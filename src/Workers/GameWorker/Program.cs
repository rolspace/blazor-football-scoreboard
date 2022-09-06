using System;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql;
using Football.Core.Persistence.MySql.Contexts;
using Football.Workers.GameWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddHostedService<Worker>();

    var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));
    services.AddDbContext<FootballDbContext>(options =>
        options.UseMySql(hostContext.Configuration.GetConnectionString("FootballDbContext"), mySqlServerVersion));

    services.AddScoped<IFootballDataProvider, MySqlFootballDataProvider>();
});

builder.Build().Run();
