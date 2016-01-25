using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mbrBlog.Models
{
    public class SubmitBlogEditorRequest
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string LabelString { get; set; }
    }
}