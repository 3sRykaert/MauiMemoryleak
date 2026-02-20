using MemoryToolkit.Maui;

namespace MauiMemoryleak
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
                    fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                    fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
                });
#if DEBUG
        builder.UseLeakDetection(collectionTarget =>
        {
            // This callback will run any time a leak is detected.
            var mainPage = Application.Current?.Windows[0].Page;
            mainPage.DisplayAlertAsync("💦Leak Detected💦", $"❗🧟❗{collectionTarget.Name} is a zombie!", "OK");
        });
#endif

            var app = builder.Build();

            return app;
        }
    }
}
