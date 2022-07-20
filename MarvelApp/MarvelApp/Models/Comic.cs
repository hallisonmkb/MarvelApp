using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MarvelApp.Models
{
    public class Comic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public List<Urls> Urls { get; set; }
        public List<Prices> Prices { get; set; }

        public string Image => Thumbnail != null ? $"{Thumbnail.Path}.{Thumbnail.Extension}" : string.Empty;
        public string Url => Urls != null && Urls.Any() ? Urls.First().Url : string.Empty;
        public decimal Price => Prices != null && Prices.Any() ? Prices.First().Price : 0;
    }
}