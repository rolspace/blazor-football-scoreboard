using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Worker.Game
{
    public class Worker : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Worker> _logger;
        private readonly HubConnection _hubConnection;
        private bool _isHubActive;
        private Timer _gameTimer;

        class GameTime
        {
            public int Current = 3600;
        } 

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger, IConfiguration config)
        {
            try
            {
                _logger = logger;
                _scopeFactory = scopeFactory;

                string hubEndpoint = config["HubEndpoint"];
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubEndpoint).Build();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred initializing the worker service");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var gameTime = new GameTime();
            _gameTimer = new Timer(new TimerCallback(DoWork), gameTime, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            try
            {
                _hubConnection.StartAsync().Wait();
                _isHubActive = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred starting the SignalR Hub");
                _isHubActive = false;
            }
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_isHubActive)
            {
                _hubConnection.StopAsync().Wait();
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_isHubActive)
            {
                _hubConnection.DisposeAsync().Wait();
            }
        }

        private async void DoWork(object state)
        {
            var gameTime = state as GameTime;
            var pastGameTime = gameTime.Current;
            Interlocked.Decrement(ref gameTime.Current);

            using (var scope = _scopeFactory.CreateScope())
            {
                IAsyncRepository<Play> playRepository = scope.ServiceProvider.GetRequiredService<IAsyncRepository<Play>>();

                var plays = await playRepository
                    .ListAsync(p => p.GameSecondsRemaining < pastGameTime && p.GameSecondsRemaining >= gameTime.Current);

                foreach (var play in plays)
                {
                    var game = new Core.Entities.Game
                    {
                        Id = play.GameId,
                        Week = play.Week,
                        HomeTeam = play.HomeTeam,
                        AwayTeam = play.AwayTeam
                    };

                    var gameState = new GameState(game, play.Qtr, (int)play.QuarterSecondsRemaining,
                        (int)play.TotalHomeScore, (int)play.TotalAwayScore, play.Desc);

                    _logger.LogInformation(gameState.ToString());

                    if (_isHubActive)
                    {
                        await _hubConnection.SendAsync("SendPlay", gameState);
                    }
                }
            }
        }
    }
}
