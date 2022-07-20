using System;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Xamarin.UITest.Queries;

namespace MarvelApp.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                .InstalledApp("com.hallisonmkb.marvelapp")
                .StartApp(AppDataMode.Clear);
            }

            return ConfigureApp.iOS
                .InstalledApp("com.hallisonmkb.marvelapp")
                .StartApp(AppDataMode.Clear);
        }
    }
}