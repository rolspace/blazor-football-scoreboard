using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Games.Entities;
using Games.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Games.Worker
{
    public class Worker : IHostedService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private IAsyncRepository<Play> _playRepository;
        private HubConnection _hubConnection;
        private Timer _gameTimer;

        class GameTime
        {
            public int Current = 3600;
        } 

        public Worker(ILogger<Worker> logger, IAsyncRepository<Play> playRepository)
        {
            _logger = logger;
            _playRepository = playRepository;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5001/gamehub").Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var gameTime = new GameTime();
            _hubConnection.StartAsync().Wait();

            _gameTimer = new Timer(new TimerCallback(DoWork), gameTime, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _hubConnection.StopAsync().Wait();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _hubConnection.DisposeAsync().Wait();
        }

        private async void DoWork(object state)
        {
            var gameState = state as GameTime;
            var pastGameTime = gameState.Current;
            Interlocked.Decrement(ref gameState.Current);

            var plays = await _playRepository.ListAsync(p => p.GameSecondsRemaining < pastGameTime && p.GameSecondsRemaining >= gameState.Current);

            foreach (var play in plays)
            {
                _logger.LogInformation(play.Desc);
                await _hubConnection.SendAsync("SendPlay", play.Desc);
            }
        }
    }
}
