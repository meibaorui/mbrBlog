using mbrBlog.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbrBlog.Models
{
    public class ArticlePager : IPager<Article>
    {

        public List<Article> ItemList { get; set; }

        public int CurrentPage { get; set; }

        public int PerPageItemCount { get; set; }

        public int TotalItemCount { get; set; }

        public string GetLinkForPage(int pageNumber)
        {
            return null;
        }
    }
}