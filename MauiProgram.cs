using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using MyMauiAppAndroid.Platforms.Android;
using NewRelic.MAUI.Plugin;


namespace MyMauiAppAndroid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<VideoPlayerView, VideoPlayerHandler>();
			});
		// .ConfigureFonts(fonts =>
		// {
		// 	fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		// 	fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
		// });

		builder.ConfigureLifecycleEvents(AppLifecycle =>
		{
			#if ANDROID
			AppLifecycle.AddAndroid(android => android
			  .OnCreate((activity, savedInstanceState) => StartNewRelic()));
			#endif

			#if IOS
      AppLifecycle.AddiOS(iOS => iOS.WillFinishLaunching((_,__) => {
        StartNewRelic();
        return false;
      }));
#endif
		});

#if DEBUG
		builder.Logging.AddDebug();
#endif


		return builder.Build();
	}
	private static void StartNewRelic()
  {
    CrossNewRelic.Current.HandleUncaughtException();

    // Set optional agent configuration
    // Options are: crashReportingEnabled, loggingEnabled, logLevel, collectorAddress, 
    // crashCollectorAddress, analyticsEventEnabled, networkErrorRequestEnabled, 
    // networkRequestEnabled, interactionTracingEnabled, webViewInstrumentation, 
    // fedRampEnabled, offlineStorageEnabled, newEventSystemEnabled, backgroundReportingEnabled
    // AgentStartConfiguration agentConfig = new AgentStartConfiguration(crashReportingEnabled:false);

    if (DeviceInfo.Current.Platform == DevicePlatform.Android)
    {
      CrossNewRelic.Current.Start("AA226eb767226c8145b89f1406d6d73cb33c275433-NRMA");
      // Start with optional agent configuration
      // CrossNewRelic.Current.Start("APP_TOKEN_HERE", agentConfig);
    } 
    else if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
    {
      CrossNewRelic.Current.Start("APP_TOKEN_HERE");
      // Start with optional agent configuration
      // CrossNewRelic.Current.Start("APP_TOKEN_HERE", agentConfig);
    }
  }
}
