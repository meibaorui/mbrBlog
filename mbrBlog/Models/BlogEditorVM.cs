using mbrBlog.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbrBlog.Models
{
    public class BlogEditorVM
    {
        public Article Article { get; set; }
        public List<ArticleLabel> SelectLabelList { get; set; }
    }
}