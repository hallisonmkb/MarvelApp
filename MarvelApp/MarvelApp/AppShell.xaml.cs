using MarvelApp.ViewModels;
using MarvelApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MarvelApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ComicDetailPage), typeof(ComicDetailPage));
        }
    }
}
