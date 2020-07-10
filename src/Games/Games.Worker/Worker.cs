using System;
using System.Threading;
using System.Threading.Tasks;
using Games.Entities;
using Games.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Games.Worker
{
    public class Worker : IHostedService, IDisposable
    {
        private readonly IAsyncRepository<Play> _playRepository;
        private readonly ILogger<Worker> _logger;
        private readonly HubConnection _hubConnection;
        private bool _isHubActive;
        private Timer _gameTimer;

        class GameTime
        {
            public int Current = 3600;
        } 

        public Worker(IAsyncRepository<Play> playRepository, ILogger<Worker> logger, IConfiguration config)
        {
            try
            {
                _logger = logger;
                _playRepository = playRepository;

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
            var gameState = state as GameTime;
            var pastGameTime = gameState.Current;
            Interlocked.Decrement(ref gameState.Current);

            var plays = await _playRepository
                .ListAsync(p => p.GameSecondsRemaining < pastGameTime && p.GameSecondsRemaining >= gameState.Current);

            foreach (var play in plays)
            {
                _logger.LogInformation(play.Desc);

                if (_isHubActive)
                {
                    await _hubConnection.SendAsync("SendPlay", play.Desc);
                }
            }
        }
    }
}
