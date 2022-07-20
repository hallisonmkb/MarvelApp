using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarvelApp.Models
{
    [Table(nameof(WishComic))]
    public class WishComic
    {
        [PrimaryKey]
        public int ComicId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
