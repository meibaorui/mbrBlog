using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mbrBlog.DAL
{
    public class SelectArticleRequest
    {
        public string Title { get; set; }
        //public string Content { get; set; }
        public DateTime? BeginPublicationTime { get; set; }
        public DateTime? EndPublicationTime { get; set; }
        public int Page { get; set; }
        public int CountPerPage { get; set; }
    }
}