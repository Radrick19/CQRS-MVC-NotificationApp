using FastSchedule.Application.AutoMapper;
using FastSchedule.Application.Dto;
using FastSchedule.Application.Services.PasswordService;
using FastSchedule.Application.Services.ScheduleMaker;
using FastSchedule.Domain;
using FastSchedule.Domain.Infrastucture;
using FastSchedule.Domain.Interfaces;
using FastSchedule.Domain.Models;
using FastSchedule.Domain.Models.Tasks;
using FastSchedule.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc().AddRazorRuntimeCompilation();

builder.Services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(ApplicationMediatREntrypoint).Assembly));

//builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddAuthentication("Cookies")
    .AddCookie(option =>
    {
        option.LoginPath = "/login";
    });

builder.Services.AddAuthorization();

builder.Logging.ClearProviders();

builder.Host.UseNLog();

builder.Services.AddDbContext<FastScheduleContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

builder.Services.AddTransient<IRepository<ScheduleTask>, TaskRepository>();

builder.Services.AddTransient<IScheduleMaker, ScheduleMaker>();

builder.Services.AddTransient<IPasswordService, PasswordService>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

//app.UseSwagger();

//app.UseSwaggerUI(config =>
//{
//    config.RoutePrefix = string.Empty;
//    config.SwaggerEndpoint("swagger/v1/swagger.json", "Schedule API");
//});

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endponints =>
{
    endponints.MapControllerRoute("Default", "{controller=home}/{action=index}");
});

app.UseStaticFiles();

app.Run();
