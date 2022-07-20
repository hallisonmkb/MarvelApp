using MarvelApp.Models;
using MarvelApp.Services;
using MarvelApp.Views;
using MonkeyCache.FileStore;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarvelApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Barrel.ApplicationId = AppInfo.PackageName;

            var image = new Image { Source = "hamburger" }.Source;
            image.AutomationId = "HamburgerShell";

            MainPage = new AppShell { FlyoutIcon = image };
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
