using System;
using Crautnot.Controllers;
using Crautnot.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace Crautnot.Quartz;

public class SyncScheduler {
    public static async void Start(IServiceProvider serviceProvider) {
        var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
        scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
        await scheduler.Start();
        var now = DateTimeOffset.Now;
        var job = JobBuilder.Create<SyncJob>()
                            .WithIdentity("SyncJob", "Synchronization")
                            .Build();

        var trigger = TriggerBuilder.Create()
                                    .WithIdentity("SynchronizationTrigger", "Synchronization")
                                    .WithCronSchedule("0/3 * * * * ?")
                                    .Build();

        await scheduler.ScheduleJob(job, trigger);
        await TelegramClientProvider.DisposeClientAsync();
    }
}