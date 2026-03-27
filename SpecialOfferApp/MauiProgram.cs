using Microsoft.Extensions.Logging;
using SpecialOfferApp.Popups;
using SpecialOfferApp.Services;
using SpecialOfferApp.ViewModels;

namespace SpecialOfferApp;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// DI registrations (prod-style)
		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<OfferPopupPage>();

		// ViewModels
		builder.Services.AddTransient<MainViewModel>();

		// Services
		builder.Services.AddSingleton<IOfferDialogService, OfferDialogService>();

		return builder.Build();
	}
}
