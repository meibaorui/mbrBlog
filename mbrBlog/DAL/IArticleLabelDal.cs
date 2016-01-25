using mbrBlog.DAL.Entity;
using mbrBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mbrBlog.DAL
{
    interface IArticleLabelDal
    {
        int AddArticleLabel(ArticleLabel label);
        int DeleteArticleLabel(int labelId);
        List<ArticleLabel> GetArticleLabelList(ArticleLabel label);
    }
}
