using MarvelApp.Models;
using MarvelApp.Services.Interfaces;
using MarvelApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    public class WishComicsViewModel : BaseViewModel
    {
        private WishComic selectedWish;

        public ICommand LoadWishesCommand { get; }
        public ICommand WishTapped { get; }
        private IDataSource<WishComic> WishListService;

        public WishComicsViewModel()
        {
            Title = "Wish List";
            Wishes = new ObservableCollection<WishComic>();
            LoadWishesCommand = new Command(async () => await LoadWishes());
            WishTapped = new Command<WishComic>(OnWishSelected);
            WishListService = DependencyService.Get<IDataSource<WishComic>>();
        }

        #region Properties
        public ObservableCollection<WishComic> Wishes { get; set; }

        public WishComic SelectedWish
        {
            get => selectedWish;
            set
            {
                SetProperty(ref selectedWish, value);
                OnWishSelected(value);
            }
        }
        #endregion

        #region Methods
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedWish = null;
        }

        async void OnWishSelected(WishComic wish)
        {
            if (wish is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ComicDetailPage)}?{nameof(ComicDetailViewModel.Id)}={wish.ComicId}");
        }

        async Task LoadWishes()
        {
            IsBusy = true;

            try
            {
                Wishes.Clear();
                var wishes = await WishListService.GetAsync();
                foreach (var wish in wishes)
                {
                    Wishes.Add(wish);
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
        #endregion
    }
}