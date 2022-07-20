using MarvelApp.Models;
using MarvelApp.Services.Interfaces;
using MarvelApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    public class ComicsViewModel : BaseViewModel
    {
        private Comic selectedComic;
        private string searchText = string.Empty;

        public ICommand LoadComicsCommand { get; }
        public ICommand ComicTapped { get; }
        public ICommand SearchCommand { get; }
        private IComicService ComicService;

        public ComicsViewModel()
        {
            Title = "Comics";
            Comics = new ObservableCollection<Comic>();
            LoadComicsCommand = new Command(async () => await DoSearchCommand());
            ComicTapped = new Command<Comic>(OnComicSelected);
            SearchCommand = new Command(async () => await DoSearchCommand(), CanExecuteSearchCommand);
            ComicService = DependencyService.Get<IComicService>();
        }

        #region Properties
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

        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    SetProperty(ref searchText, value ?? string.Empty);
                    OnPropertyChanged(nameof(SearchText));

                    // Perform the search
                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
            }
        }
        #endregion

        #region Methods
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

        async Task DoSearchCommand()
        {
            IsBusy = true;

            try
            {
                Comics.Clear();
                if (string.IsNullOrEmpty(SearchText))
                {
                    var comics = await ComicService.GetAsync();
                    foreach (var comic in comics)
                    {
                        Comics.Add(comic);
                    }
                }
                else if (SearchText.Count() >= 3)
                {
                    var comics = await ComicService.GetByTitleAsync(SearchText);
                    foreach (var comic in comics)
                    {
                        Comics.Add(comic);
                    }
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
        private bool CanExecuteSearchCommand()
        {
            return !IsBusy;
        }
        #endregion
    }
}