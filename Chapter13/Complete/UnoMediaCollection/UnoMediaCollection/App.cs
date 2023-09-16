using UnoMediaCollection.Interfaces;
using UnoMediaCollection.Services;
using UnoMediaCollection.ViewModels;

namespace UnoMediaCollection
{
    public class App : Application
    {
        protected Window? MainWindow { get; private set; }
        internal static IHost? HostContainer { get; private set; }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var navigationService = new NavigationService(new Frame());
            navigationService.Configure(nameof(MainPage), typeof(MainPage));
            navigationService.Configure(nameof(ItemDetailsPage), typeof(ItemDetailsPage));

            var builder = this.CreateBuilder(args)
                .Configure(host => host
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif
                    .UseLogging(configure: (context, logBuilder) =>
                    {
                        // Configure log levels for different categories of logging
                        logBuilder
                            .SetMinimumLevel(
                                context.HostingEnvironment.IsDevelopment() ?
                                    LogLevel.Information :
                                    LogLevel.Warning)

                            // Default filters for core Uno Platform namespaces
                            .CoreLogLevel(LogLevel.Warning);

                        // Uno Platform namespace filter groups
                        // Uncomment individual methods to see more detailed logging
                        //// Generic Xaml events
                        //logBuilder.XamlLogLevel(LogLevel.Debug);
                        //// Layout specific messages
                        //logBuilder.XamlLayoutLogLevel(LogLevel.Debug);
                        //// Storage messages
                        //logBuilder.StorageLogLevel(LogLevel.Debug);
                        //// Binding related messages
                        //logBuilder.XamlBindingLogLevel(LogLevel.Debug);
                        //// Binder memory references tracking
                        //logBuilder.BinderMemoryReferenceLogLevel(LogLevel.Debug);
                        //// RemoteControl and HotReload related
                        //logBuilder.HotReloadCoreLogLevel(LogLevel.Information);
                        //// Debug JS interop
                        //logBuilder.WebAssemblyLogLevel(LogLevel.Debug);

                    }, enableUnoLogging: true)
                    .UseConfiguration(configure: configBuilder =>
                        configBuilder
                            .EmbeddedSource<App>()
                            .Section<AppConfig>()
                    )
                    // Register Json serializers (ISerializer and ISerializer)
                    .UseSerialization((context, services) => services
                        .AddContentSerializer(context)
                        .AddJsonTypeInfo(WeatherForecastContext.Default.IImmutableListWeatherForecast))
                    .UseHttp((context, services) => services
                        // Register HttpClient
#if DEBUG
					// DelegatingHandler will be automatically injected into Refit Client
					.AddTransient<DelegatingHandler, DebugHttpHandler>()
#endif
                        .AddSingleton<IWeatherCache, WeatherCache>()
                        .AddRefitClient<IApiClient>(context))
                    .ConfigureServices((context, services) =>
                    {
                        services.AddSingleton<INavigationService>(navigationService);
                        services.AddSingleton<IDataService, DataService>();
                        services.AddTransient<MainViewModel>();
                        services.AddTransient<ItemDetailsViewModel>();
                    })
                );
            MainWindow = builder.Window;

            HostContainer = builder.Build();

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (MainWindow.Content is not Frame rootFrame)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // Place the frame in the current Window
                MainWindow.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), args.Arguments);
            }

            NavigationService.AppFrame = rootFrame;

            // Ensure the current window is active
            MainWindow.Activate();
        }
    }
}