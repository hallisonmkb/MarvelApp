using MarvelApp.Models;
using MarvelApp.ViewModels;
using MarvelApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarvelApp.Views
{
    public partial class WishComicsPage : ContentPage
    {
        WishComicsViewModel _viewModel;

        public WishComicsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new WishComicsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}