using Crautnot.Models;
using Crautnot.Telegram;
using System.Net;
using Crautnot.Client;
using Crautnot.Services;
using Crautnot.Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crautnot.Installers;

public class ApplicationInstaller : IInstaller {
    public void InstallServices(IServiceCollection services, IConfiguration configuration) {
        // services
        services.AddScoped<SynchronizeService>();
        services.AddScoped<NewsService>();

        // logs
        services.AddSingleton(new Bugsnag.Client("05e1795885afb0c8fbeb36dc1e409408"));
        services.AddTransient<ErrorNotifier>();

        // configuration
        //services.Configure<ExchangeOptions>(configuration.GetSection("ExchangeOptions"));
        //services.Configure<TelegramOptions>(configuration.GetSection("TelegramOptions"));
        //services.Configure<JobOptions>(configuration.GetSection("JobOptions"));

        services.AddScoped<ExchangeOptions>();
        services.AddScoped<TelegramOptions>();
        services.AddScoped<JobOptions>();

        // client services
        services.AddScoped<BybitClientService>();
        services.AddScoped<MexcClientService>();
        services.AddScoped<GateIoClientService>();
        services.AddScoped<BinanceClientService>();
        services.AddScoped<OkxClientService>();

        // tg
        services.AddScoped<TelegramBot>();

        // quartz
        services.AddTransient<JobFactory>();
        services.AddScoped<SyncJob>();
    }
}