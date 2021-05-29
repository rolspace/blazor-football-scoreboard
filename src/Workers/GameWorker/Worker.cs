using Football.Core.Converters;
using Football.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Football.Workers.GameWorker
{
    public class Worker : IHostedService, IAsyncDisposable
    {
        private readonly ILogger<Worker> _logger;
        private readonly HubConnection _hubConnection;
        private bool _isHubActive;
        private Timer _gameTimer;
        private HttpClient _httpClient;

        private static JsonSerializerOptions jsonSerializerOptions = new()
        {
            Converters =
            {
                new GameConverter()
            },
            PropertyNameCaseInsensitive = true
        };

        internal class GameTime
        {
            public int Counter = 3600;
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
            _gameTimer.Dispose();

            if (_isHubActive)
            {
                await _hubConnection.DisposeAsync();
            }
        }

        private async void DoWork(object state)
        {
            var gameTime = state as GameTime;
            int pastGameTime = gameTime.Counter;
            Interlocked.Decrement(ref gameTime.Counter);

            try
            {
                string requestUrl = $"http://localhost:2500/api/football/plays/1/{pastGameTime}/{gameTime.Counter}";
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                string jsonResponse = await response.Content.ReadAsStringAsync();

                List<Play> plays = JsonSerializer.Deserialize<List<Play>>(jsonResponse, new JsonSerializerOptions(jsonSerializerOptions));

                foreach (Play play in plays)
                {
                    _logger.LogInformation(play.ToString());

                    await _httpClient.PutAsJsonAsync($"http://localhost:2500/api/football/stat/{play.Game.Id}/{play.Game.HomeTeam}", play.HomePlayLog);
                    await _httpClient.PutAsJsonAsync($"http://localhost:2500/api/football/stat/{play.Game.Id}/{play.Game.AwayTeam}", play.AwayPlayLog);

                    await _hubConnection.SendAsync("SendPlay", play);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
