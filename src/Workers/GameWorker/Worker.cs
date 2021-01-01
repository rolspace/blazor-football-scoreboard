using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Football.Workers.GameWorker
{
    public class Worker : IHostedService, IAsyncDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
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
                _scopeFactory = scopeFactory;
                _httpClient = new HttpClient();
                _httpClient.Timeout = TimeSpan.FromSeconds(1);

                string hubEndpoint = config["HubEndpoint"];
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubEndpoint).Build();
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

            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:11517/api/games/week/1");

            /*using (var scope = _scopeFactory.CreateScope())
            {
                IRepository<Play> playRepository = scope.ServiceProvider.GetRequiredService<IRepository<Play>>();

                var plays = await playRepository
                    .ListAsync(p => p.GameSecondsRemaining < pastGameTime && p.GameSecondsRemaining >= gameTime.Current);

                //TODO: change this to tasks?
                foreach (var play in plays)
                {
                    _logger.LogInformation($"{play.HomeTeam}-{play.AwayTeam}-{play.Desc}");

                    if (_isHubActive)
                    {
                        await _hubConnection.SendAsync("SendPlay", play);
                    }
                }*/

                //TODO: send to stats processing
            // }
        }
    }
}
