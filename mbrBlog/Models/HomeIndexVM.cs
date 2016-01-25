using mbrBlog.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbrBlog.Models
{
    public class HomeIndexVM
    {
        public List<Article> ArticleList { get; set; }
        public List<ArticleLabel> ArticleLabelList { get; set; }
    }
}