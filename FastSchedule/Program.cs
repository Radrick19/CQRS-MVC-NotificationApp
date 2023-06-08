using FastSchedule.Application.AutoMapper;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddMvc().AddRazorRuntimeCompilation();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Logging.ClearProviders();

builder.Host.UseNLog();

app.UseEndpoints(endpoints =>
    endpoints.MapControllerRoute("Default", "{controller}/{action}/{id?}")
);

app.Run();
