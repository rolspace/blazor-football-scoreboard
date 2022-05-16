using Football.Core.Models;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql.Contexts;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Workers.GameWorker
{
    public class Worker : IHostedService, IAsyncDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly HubConnection _hubConnection;

        private bool _isHubActive;
        private Timer _gameTimer;

        internal class GameTime
        {
            public int Counter = 3600;
        }

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger, IConfiguration config)
        {
            try
            {
                _logger = logger;
                _scopeFactory = scopeFactory;

                var hubUri = new Uri(config["HubEndpoint"]);

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUri).Build();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred initializing the GameWorker service");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var gameTime = new GameTime();
            _gameTimer = new Timer(new TimerCallback(DoWork), gameTime, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            try
            {
                await _hubConnection.StartAsync();
                _isHubActive = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred starting the SignalR connection hub");
                _isHubActive = false;
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_isHubActive)
            {
                await _hubConnection.StopAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            _gameTimer.Dispose();

            if (_isHubActive)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        private async void DoWork(object state)
        {
            var gameTime = state as GameTime;
            int previousTime = gameTime.Counter;

            Interlocked.Decrement(ref gameTime.Counter);
            int currentTime = gameTime.Counter;

            try
            {
                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    IFootballDataProvider dataProvider = scope.ServiceProvider.GetRequiredService<IFootballDataProvider>();
                    IReadOnlyCollection<Play> plays = await dataProvider.GetPlaysByWeekAndGameTime(1, previousTime, currentTime);

                    foreach (Play play in plays)
                    {
                        _logger.LogInformation(play.ToString());

                        await dataProvider.SaveStat(play.Game.Id, play.Game.HomeTeam, play.HomePlayLog);
                        await dataProvider.SaveStat(play.Game.Id, play.Game.AwayTeam, play.AwayPlayLog);

                        if (_isHubActive)
                        {
                            await _hubConnection.SendAsync("SendPlay", play);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
