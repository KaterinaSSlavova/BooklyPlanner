using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlannerMaui.ViewModels;
using Infrastructure;

namespace PlannerMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=PlannerDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(connectionString));

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddAutoMapper(typeof(MauiProgram).Assembly);
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IReadingTaskRepository, ReadingTaskRepository>();
            builder.Services.AddTransient<IUserService, UserServices>();
            builder.Services.AddTransient<IReadingTaskService, ReadingTaskServices>();
            builder.Services.AddTransient<ActiveTaskPage>();
            builder.Services.AddTransient<ActiveTaskViewModel>();
            builder.Services.AddTransient<CompletedTaskPage>();
            builder.Services.AddTransient<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
