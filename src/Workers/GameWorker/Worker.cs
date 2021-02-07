using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Football.Core.Models;
using System.Collections.Generic;

namespace Football.Workers.GameWorker
{
    public class Worker : IHostedService, IAsyncDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly HubConnection _hubConnection;
        private bool _isHubActive;
        private Timer _gameTimer;
        private HttpClient _httpClient;

        class GameTime
        {
            public int Current = 3600;
        } 

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger, IConfiguration config)
        {
            try
            {
                _logger = logger;

                _httpClient = new HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(1)
                };

                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(config["HubEndpoint"]).Build();
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
            if (_isHubActive)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        private async void DoWork(object state)
        {
            var gameTime = state as GameTime;
            var pastGameTime = gameTime.Current;
            Interlocked.Decrement(ref gameTime.Current);

            try
            {
                var requestUrl = $"http://localhost:2500/api/football/plays/week/1/{pastGameTime}/{gameTime.Current}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Play> plays = JsonSerializer.Deserialize<List<Play>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (plays.Count > 0)
                {
                    foreach (var play in plays)
                    {
                        _logger.LogInformation(play.ToString());
                        await _hubConnection.SendAsync("SendPlay", play);
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
