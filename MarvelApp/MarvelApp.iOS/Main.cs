using MarvelApp.Services.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace MarvelApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            DependencyService.Register<IMessage, MessageIOS>();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
