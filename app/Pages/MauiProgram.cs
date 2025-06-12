using Microsoft.Extensions.Logging;
using Plugin.LocalNotification; // ✅ для локальних повідомлень
using cursiv.Services;         // ✅ наш сервіс повідомлень

namespace cursiv.Pages
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseLocalNotification() // ✅ обов'язковий виклик для ініціалізації
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Викликаємо нагадування про воду
            NotificationService.ScheduleWaterReminder();

            return app;
        }
    }
}
