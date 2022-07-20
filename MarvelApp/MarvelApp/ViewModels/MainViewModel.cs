using MarvelApp.Models;
using MarvelApp.Services.Interfaces;
using MarvelApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Comic selectedComic;

        public ICommand OpenWebCommand { get; }
        public ICommand ComicTapped { get; }
        public ICommand LoadNewComicsCommand { get; }
        private IComicService ComicService;

        public MainViewModel()
        {
            Title = "Featured";
            Comics = new ObservableCollection<Comic>();
            LoadNewComicsCommand = new Command(async () => await LoadNewComics());
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.marvel.com/"));
            ComicTapped = new Command<Comic>(OnComicSelected);
            ComicService = DependencyService.Get<IComicService>();
        }

        public ObservableCollection<Comic> Comics { get; set; }

        public Comic SelectedComic
        {
            get => selectedComic;
            set
            {
                SetProperty(ref selectedComic, value);
                OnComicSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedComic = null;
        }

        async void OnComicSelected(Comic comic)
        {
            if (comic is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ComicDetailPage)}?{nameof(ComicDetailViewModel.Id)}={comic.Id}");
        }

        async Task LoadNewComics()
        {
            IsBusy = true;

            try
            {
                Comics.Clear();
                var comics = await ComicService.GetNewOnSaleAsync();
                foreach (var comic in comics)
                {
                    Comics.Add(comic);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}