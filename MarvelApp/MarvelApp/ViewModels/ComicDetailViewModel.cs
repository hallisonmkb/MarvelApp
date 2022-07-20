using MarvelApp.Models;
using MarvelApp.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ComicDetailViewModel : BaseViewModel
    {
        private int id, pageCount;
        private string title, description, url, image;
        private decimal price;
        private bool isWishList;

        public ICommand OpenWebCommand { get; }
        public ICommand WishListCommand { get; }
        private IDataSource<WishComic> WishListService;
        private IComicService ComicService;

        public bool HasUrl => !string.IsNullOrWhiteSpace(Url);
        public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
        public string ImageWishList => IsWishList ? "checkbox_outline.png" : "plus_outline.png";

        public ComicDetailViewModel()
        {
            OpenWebCommand = new Command(async () => await Browser.OpenAsync(Url));
            WishListCommand = new Command(async () => await CheckWishListCommand());
            WishListService = DependencyService.Get<IDataSource<WishComic>>();
            ComicService = DependencyService.Get<IComicService>();
        }

        #region Properties
        public new string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set
            {
                SetProperty(ref description, value);
                OnPropertyChanged(nameof(HasDescription));
            }
        }

        public string Url
        {
            get => url;
            set
            {
                SetProperty(ref url, value);
                OnPropertyChanged(nameof(HasUrl));
            } 
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public int PageCount
        {
            get => pageCount;
            set => SetProperty(ref pageCount, value);
        }

        public bool IsWishList
        {
            get => isWishList;
            set
            {
                SetProperty(ref isWishList, value);
                OnPropertyChanged(nameof(ImageWishList));
            }
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                LoadComic(value);
                LoadWish();
            }
        }
        #endregion

        #region Methods
        public async void LoadComic(int id)
        {
            IsBusy = true;

            try
            {
                var comic = (await ComicService.GetByIdAsync(id)).FirstOrDefault();
                if (comic is null)
                    return;

                this.Title = comic.Title;
                this.Url = comic.Url;
                this.Image = comic.Image;
                this.Price = comic.Price;
                this.PageCount = comic.PageCount;
                this.Description = comic.Description;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Load Comic - {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadWish()
        {
            IsBusy = true;

            try
            {
                var wish = await WishListService.GetAsync(this.Id);
                if (wish is null)
                    return;

                IsWishList = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Load Wish - {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task CheckWishListCommand()
        {
            try
            {
                IsWishList = !IsWishList;

                if (IsWishList)
                {
                    await WishListService.AddAsync(new WishComic 
                    { 
                        ComicId = this.Id, 
                        Title = this.Title, 
                        Image = this.Image, 
                        Price = this.Price 
                    });

                    DependencyService.Get<IMessage>().LongAlert("Comic added successfully to your wishlist!");
                }
                else
                {
                    await WishListService.DeleteAsync(this.Id);

                    DependencyService.Get<IMessage>().LongAlert("Comic removed from your wishlist.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Check Wish - {ex}");
            }
        }
        #endregion
    }
}
