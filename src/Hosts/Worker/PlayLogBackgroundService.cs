using AutoMapper;
using Football.Application.Features.Games;
using Football.Application.Features.Games.Models;
using Football.Application.Features.Plays;
using Football.Application.Features.Plays.Models;
using Football.Application.Features.Stats;
using Football.Application.Interfaces;
using Football.Application.Services;
using MediatR;

namespace Football.Worker;

public class PlayLogBackgroundService : BackgroundService, IAsyncDisposable
{
    private const int DEFAULT_WEEK = 1;

    private const string WeekKey = "Week";

    private readonly int _week;

    private readonly IGameTimeManager _gameTimeManager;

    private readonly IHubManager _hubManager;

    private readonly IMapper _mapper;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<PlayLogBackgroundService> _logger;

    public PlayLogBackgroundService(IHubManager hubManager, IMapper mapper,
        IServiceScopeFactory scopeFactory, ILogger<PlayLogBackgroundService> logger, IConfiguration configuration)
    {
        _hubManager = hubManager;
        _mapper = mapper;
        _scopeFactory = scopeFactory;
        _logger = logger;

        _gameTimeManager = new GameTimeManager();

        if (configuration is not null)
        {
            _week = configuration.GetValue(WeekKey, DEFAULT_WEEK);
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                var gamesQuery = new GetGamesQuery()
                {
                    Week = _week
                };

                ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                IEnumerable<GameDto> gameDtos = await mediator.Send(gamesQuery, cancellationToken);

                _gameTimeManager.GamesScheduled = gameDtos.Count();
            }

            await _hubManager.StartAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting the Hub manager");
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
                        Week = _week,
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

                        await _hubManager.SendAsync<PlayDto>("SendPlay", playDto, stoppingToken);

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

        await _hubManager.DisposeAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _hubManager.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);

        _logger.LogInformation("Background service stopped");
    }

    public async ValueTask DisposeAsync()
    {
        await _hubManager.DisposeAsync();
        base.Dispose();

        _logger.LogInformation("Background service disposed");
    }
}
