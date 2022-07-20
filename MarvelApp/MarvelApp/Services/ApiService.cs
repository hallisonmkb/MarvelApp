using MarvelApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MarvelApp.Services
{
    public abstract class ApiService
    {
        public virtual string GetEndpoint(string key, bool settings = false)
        {
            return $"{ApiConstants.BASE_URL}{key}{(key.Contains("?") ? "&" : "?")}{(settings ? GetSettings() : string.Empty)}";
        }

        public virtual string GetSettings()
        {
            var settings = string.Empty;

            var orderBy = ApiConstants.ORDER_BY_TITLE;
            if (Preferences.Get(SettingConstants.ORDER_BY_ONSALEDATE, false))
                orderBy = ApiConstants.ORDER_BY_ONSALEDATE;
            else if (Preferences.Get(SettingConstants.ORDER_BY_FOCDATE, false))
                orderBy = ApiConstants.ORDER_BY_FOCDATE;

            settings += $"{nameof(orderBy)}={orderBy}";

            var limit = ApiConstants.LIMIT_RECORD_BY_20;
            if (Preferences.Get(SettingConstants.LIMIT_RECORD_BY_40, false))
                limit = ApiConstants.LIMIT_RECORD_BY_40;
            else if (Preferences.Get(SettingConstants.LIMIT_RECORD_BY_60, false))
                limit = ApiConstants.LIMIT_RECORD_BY_60;

            settings += $"&{nameof(limit)}={limit}";

            return settings;
        }
    }
}
