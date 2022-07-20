using MarvelApp.Helpers;
using Xamarin.Essentials;

namespace MarvelApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Properties
        public bool IsOrderByTitle
        {
            get => Preferences.Get(SettingConstants.ORDER_BY_TITLE, true);
            set
            {
                Preferences.Set(SettingConstants.ORDER_BY_TITLE, value);
                OnPropertyChanged(nameof(IsOrderByTitle));
            }
        }

        public bool IsOrderByOnSaleDate
        {
            get => Preferences.Get(SettingConstants.ORDER_BY_ONSALEDATE, false);
            set
            {
                Preferences.Set(SettingConstants.ORDER_BY_ONSALEDATE, value);
                OnPropertyChanged(nameof(IsOrderByOnSaleDate));
            }
        }

        public bool IsOrderByFocDate
        {
            get => Preferences.Get(SettingConstants.ORDER_BY_FOCDATE, false);
            set
            {
                Preferences.Set(SettingConstants.ORDER_BY_FOCDATE, value);
                OnPropertyChanged(nameof(IsOrderByFocDate));
            }
        }

        public bool IsLimitRecordBy20
        {
            get => Preferences.Get(SettingConstants.LIMIT_RECORD_BY_20, true);
            set
            {
                Preferences.Set(SettingConstants.LIMIT_RECORD_BY_20, value);
                OnPropertyChanged(nameof(IsLimitRecordBy20));
            }
        }

        public bool IsLimitRecordBy40
        {
            get => Preferences.Get(SettingConstants.LIMIT_RECORD_BY_40, false);
            set
            {
                Preferences.Set(SettingConstants.LIMIT_RECORD_BY_40, value);
                OnPropertyChanged(nameof(IsLimitRecordBy40));
            }
        }

        public bool IsLimitRecordBy60
        {
            get => Preferences.Get(SettingConstants.LIMIT_RECORD_BY_60, false);
            set
            {
                Preferences.Set(SettingConstants.LIMIT_RECORD_BY_60, value);
                OnPropertyChanged(nameof(IsLimitRecordBy60));
            }
        }
        #endregion
    }
}
