using Microsoft.Extensions.Logging;
using REV_UTS_72220538_Mobile.Data;
using REV_UTS_72220538_Mobile.Pages;
using System.Net.Http;
using CommunityToolkit.Maui;

namespace REV_UTS_72220538_Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
        
                .ConfigureFonts(fonts =>
                 {
                     fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                     fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                 }); 

            // Register HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Register your ccService (which depends on HttpClient)
            builder.Services.AddSingleton<ccService>();
            builder.Services.AddHttpClient<authService>();

            // Register AuthService
            builder.Services.AddTransient<authService>();

            // Register MainPage with dependency injection
            builder.Services.AddTransient<MainPage>();
            return builder.Build();
        }

    }
}

