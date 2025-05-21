using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPIApp
{
    public class Article
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string source { get; set; }
        public bool isFavorite { get; set; }

        public Article(string title, string description, string url, string urlToImage, DateTime publishedAt, bool isFavorite)
        {
            this.title = title;
            this.description = description;
            this.urlToImage = urlToImage;
            this.url = url;
            this.publishedAt = publishedAt;
            this.isFavorite = isFavorite;
        }


    }
}