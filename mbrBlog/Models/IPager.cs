using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbrBlog.Models
{
    interface IPager<T>
    {
        List<T> ItemList { get; set; }
        int CurrentPage { get; set; }
        int PerPageItemCount { get; set; }
        int TotalItemCount { get; set; }
        string GetLinkForPage(int pageNumber);
    }
}
