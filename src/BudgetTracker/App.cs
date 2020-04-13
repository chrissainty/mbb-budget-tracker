using Microsoft.MobileBlazorBindings;
using Xamarin.Forms;
using BudgetTracker.Features.Home;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BudgetTracker
{
    public class App : Application
    {
        public App()
        {
            var database = new BudgetTrackerDb(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "budgettracker.db3"));
            
            var host = MobileBlazorBindingsHost.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    services.AddSingleton<BudgetTrackerDb>(database);
                    services.AddSingleton<AppState>();
                })
                .Build();

            MainPage = new ContentPage();
            host.AddComponent<HomePage>(parent: MainPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
