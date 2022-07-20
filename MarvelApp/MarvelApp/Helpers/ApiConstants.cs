using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelApp.Helpers
{
    public class ApiConstants
    {
        public static readonly string BASE_URL = "https://gateway.marvel.com:443/v1/public/";

        public static readonly string COMICS = "comics";
        public static readonly string COMICS_TITLE = $"{COMICS}?titleStartsWith=";
        public static readonly string COMICS_ID = $"{COMICS}/";
        public static readonly string NEW_COMICS_ON_SALE = $"{COMICS}?limit={10}&orderBy=-onsaleDate&startYear={DateTime.Today.Year}";

        public static readonly string ORDER_BY_ONSALEDATE = "-onsaleDate";
        public static readonly string ORDER_BY_FOCDATE = "-focDate";
        public static readonly string ORDER_BY_TITLE = "title";

        public static readonly int LIMIT_RECORD_BY_20 = 20;
        public static readonly int LIMIT_RECORD_BY_40 = 40;
        public static readonly int LIMIT_RECORD_BY_60 = 60;

        public static readonly string PRIVATE_KEY = "a978353a3f955c7a32af0ce69726c35db9710ee4";
        public static readonly string PUBLIC_KEY = "8d383ca3fcf79107c2c8cb6ac52988d4";
    }
}
