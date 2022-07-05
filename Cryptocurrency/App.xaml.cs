using Cryptocurrency.Helper;
using Cryptocurrency.Services.Implementation;
using Cryptocurrency.Services.Interfaces;
using Cryptocurrency.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Windows;

namespace Cryptocurrency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"),
                    optional: true,
                    reloadOnChange: false
                 ).Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IRetrieveDataService, RetrieveDataService>(client =>
            {
                client.BaseAddress = new Uri(Configuration["docsCoincap:BaseAddress"]);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddTransient<MainViewModel>();
            services.AddTransient<StartViewModel>();
            services.AddSingleton<PageService>();

            services.AddSingleton(typeof(MainWindow));
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ViewModelLocator.Init(ServiceProvider);
            var mainWindow = ServiceProvider!.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
