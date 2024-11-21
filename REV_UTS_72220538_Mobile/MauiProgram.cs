using Microsoft.Extensions.Logging;
using REV_UTS_72220538_Mobile.Data;
using REV_UTS_72220538_Mobile.Pages;
using System.Net.Http;

namespace REV_UTS_72220538_Mobile
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
                });

            // Register HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Register your ccService (which depends on HttpClient)
            builder.Services.AddSingleton<ccService>();

            return builder.Build();
        }

    }
}
