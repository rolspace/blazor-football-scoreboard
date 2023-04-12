using AutoMapper;
using Football.Application.Features.Plays;
using Football.Application.Features.Stats;
using Football.Application.Interfaces;
using Football.Application.Models;
using Football.Application.Services;
using MediatR;

namespace Football.Worker;

public class PlayLogBackgroundService : BackgroundService, IAsyncDisposable
{
    private readonly IGameTimeManager _gameTimeManager;

    private readonly IHubManager _hubManager;

    private readonly IMapper _mapper;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<PlayLogBackgroundService> _logger;

    public PlayLogBackgroundService(IHubManager hubManager, IMapper mapper,
        IServiceScopeFactory scopeFactory, ILogger<PlayLogBackgroundService> logger)
    {
        _hubManager = hubManager;
        _mapper = mapper;
        _scopeFactory = scopeFactory;
        _logger = logger;

        _gameTimeManager = new GameTimeManager();
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
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
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

                    int quarter = _gameTimeManager.GetQuarter();
                    int quarterSecondsRemaining = _gameTimeManager.GetQuarterSecondsRemaining();
                    var query = new GetPlaysQuery()
                    {
                        Week = 1,
                        Quarter = quarter,
                        QuarterSecondsRemaining = quarterSecondsRemaining
                    };

                    IEnumerable<PlayDto> playDtos = await mediator.Send(query);

                    int gameOverCount = playDtos.Count(p => p.GameOver);
                    if (gameOverCount > 0) _gameTimeManager.IncrementGamesOver(gameOverCount);

                    foreach (PlayDto playDto in playDtos)
                    {
                        var saveStatsCommand = _mapper.Map<SaveStatsCommand>(playDto);
                        await mediator.Send(saveStatsCommand);

                        await _hubManager.SendAsync<PlayDto>("SendPlay", playDto, stoppingToken);
                        _logger.LogInformation($"{quarter}/{quarterSecondsRemaining} - {playDto.ToString()}");
                    }

                    _gameTimeManager.SetTime();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error ocurred trying to retrieve play log data.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
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
