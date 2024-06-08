using AutoMapper;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Plays;
using Football.Application.Features.Plays.Models;
using Football.Application.Features.Stats;
using Football.Application.Interfaces;
using Football.Application.Services;
using Football.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace Football.Worker;

public class PlayLogBackgroundService : BackgroundService, IAsyncDisposable
{
    private readonly ScoreboardOptions _scoreboardOptions;

    private readonly IHubProvider _hubProvider;

    private readonly IMapper _mapper;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<PlayLogBackgroundService> _logger;

    private readonly IGameTimeManager _gameTimeManager;

    public PlayLogBackgroundService(IHubProvider hubProvider, IMapper mapper,
        IServiceScopeFactory scopeFactory, ILogger<PlayLogBackgroundService> logger,
        IOptions<ScoreboardOptions> scoreboardOptions)
    {
        _hubProvider = hubProvider;
        _mapper = mapper;
        _scopeFactory = scopeFactory;
        _logger = logger;
        _scoreboardOptions = scoreboardOptions.Value;

        _gameTimeManager = new GameTimeManager();
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                var gamesQuery = new GetGamesQuery()
                {
                    Week = _scoreboardOptions.Week
                };

                ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                IEnumerable<GameDto> gameDtos = await mediator.Send(gamesQuery, cancellationToken);

                _gameTimeManager.GamesScheduled = gameDtos.Count();
            }

            await _hubProvider.StartAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting the background service");
        }
        finally
        {
            await base.StartAsync(cancellationToken);
            _logger.LogInformation("Background service started");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: finish loop if all the games are finished
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    int quarter = _gameTimeManager.GetQuarter();
                    int quarterSecondsRemaining = _gameTimeManager.GetQuarterSecondsRemaining();
                    var query = new GetPlaysQuery()
                    {
                        Week = _scoreboardOptions.Week,
                        Quarter = quarter,
                        QuarterSecondsRemaining = quarterSecondsRemaining
                    };

                    ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                    IEnumerable<PlayDto> playDtos = await mediator.Send(query, stoppingToken);

                    int gameOverCount = playDtos.Count(p => p.GameOver);
                    if (gameOverCount > 0) _gameTimeManager.IncrementGamesFinished(gameOverCount);

                    foreach (PlayDto playDto in playDtos)
                    {
                        SaveGameStatsCommand saveGameStatsCommand = _mapper.Map<SaveGameStatsCommand>(playDto);
                        await mediator.Send(saveGameStatsCommand, stoppingToken);

                        await _hubProvider.SendAsync<PlayDto>("SendPlay", playDto, stoppingToken);

                        _logger.LogInformation("{quarter}/{quarterSecondsRemaining} - {playDto}", quarter, quarterSecondsRemaining, playDto);
                    }

                    _gameTimeManager.SetTime();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred trying to retrieve play log data.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }

        await _hubProvider.DisposeAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubProvider.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);

        _logger.LogInformation("Background service stopped");
    }

    public async ValueTask DisposeAsync()
    {
        await _hubProvider.DisposeAsync();
        base.Dispose();

        _logger.LogInformation("Background service disposed");
    }
}
