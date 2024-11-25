using Crautnot;
using Crautnot.Controllers;
using Crautnot.Installers;
using Crautnot.Models;
using Crautnot.Quartz;
using Crautnot.Services;
using Crautnot.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
                    //.AddJsonFile("appsettings.json", true) // добавление файла конфигурации
                    .Build();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.InstallServicesInAssembly(configuration, typeof(Program));
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crautnot", Version = "v1" });
});
builder.Services.AddSignalR();

var app = builder.Build();
//using(var scope = app.Services.CreateScope()) {
//    var scopedProvider = scope.ServiceProvider;
//    var newsService = scopedProvider.GetRequiredService<INewsRepository>();
//    await newsService.InitializeCacheAsync();
//}

using(var scope = app.Services.CreateScope()) {
    var serviceProvider = scope.ServiceProvider;

    var jobOptions = scope.ServiceProvider.GetRequiredService<IOptions<JobOptions>>().Value;
    if (jobOptions.SyncJob) {
        SyncScheduler.Start(serviceProvider);
        //SyncOkxScheduler.Start(serviceProvider);
        //SyncBybitScheduler.Start(serviceProvider);
    }
    //CleanLogsScheduler.Start(serviceProvider);
}
// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crautnot v1"));

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => {
    endpoints.MapHub<QuartzHub>("/quartzHub");
});

app.Run();
