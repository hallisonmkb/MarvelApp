using MarvelApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;
using static Xamarin.Forms.Button;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace MarvelApp.Views
{
    public partial class ComicDetailPage : ContentPage
    {
        ComicDetailViewModel _viewModel;

        public ComicDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ComicDetailViewModel();
        }
    }
}