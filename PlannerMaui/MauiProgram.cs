using Application.Interfaces;
using Infrastructure.ApiClients;
using Microsoft.Extensions.Logging;
using PlannerMaui.ViewModels;

namespace PlannerMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient("TaskInternalApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:7166/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddScoped<IApiTaskClient, ApiTaskClient>();
            builder.Services.AddAutoMapper(typeof(MauiProgram).Assembly);
            builder.Services.AddTransient<ActiveTaskPage>();
            builder.Services.AddTransient<ActiveTaskViewModel>();
            builder.Services.AddTransient<CompletedTaskPage>();
            builder.Services.AddTransient<CompletedTaskViewModel>();
            builder.Services.AddTransient<CreateNewTaskPage>();
            builder.Services.AddTransient<CreateReadingTaskViewModel>();
            builder.Services.AddTransient<MainPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
