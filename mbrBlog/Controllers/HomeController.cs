using mbrBlog.DAL;
using mbrBlog.DAL.Entity;
using mbrBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mbrBlog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new HomeIndexVM();
            var ArticleList = ArticleDal.SelectArticleList(new SelectArticleRequest());
            var ArticleLabelList = ArticleDal.GetAllLabelAndArticle();
            model.ArticleList = ArticleList;
            model.ArticleLabelList = ArticleLabelList;
            return View(model);
        }

        const int noArticleId = -1;
        /// <summary>
        /// 博客编辑页
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult BlogEditor(int id = noArticleId)
        {
            var labelList = ArticleDal.GetAllLabel();
            if (id == noArticleId)
            {
                return View(new BlogEditorVM { SelectLabelList = labelList });
            }
            else 
            {
                var article = ArticleDal.GetArticle(id);

                return View(new BlogEditorVM { Article = article, SelectLabelList = labelList });
            }
        }

        /// <summary>
        /// 提交博客编辑结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult SubmitBlogEditor(SubmitBlogEditorRequest request)
        {
            Article article = new Article();


            if (request.Id != null)
                article.Id = request.Id.Value;

            article.ArticleLabel = AnalyzeLabelString(request.LabelString);
            article.Title = request.Title.Trim();
            article.Content = request.Content;
            article.PublicationTime = DateTime.Now;
            if (request.Id == null)
            {
                ArticleDal.AddArticle(article);
            }
            else
            {
                ArticleDal.UpdateArticle(article);
            }
            return Redirect("Index");
        }
        /// <summary>
        /// 博客浏览页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Blog(int id)
        {
            var article = ArticleDal.GetArticle(id);
            return View(new BlogVM { Article = article });
        }

        private List<ArticleLabel> AnalyzeLabelString(string LabelString)
        {
            var list = new List<ArticleLabel>();
            if (String.IsNullOrEmpty(LabelString)) return list;
            foreach (var labelstringItem in LabelString.Split(new char[] { ';' }))
            {
                if (String.IsNullOrEmpty(labelstringItem))
                    break;
                var id = labelstringItem.Split(new char[] { ',' })[0];
                var title = labelstringItem.Split(new char[] { ',' })[1];
                list.Add(new ArticleLabel() { Id = Int32.Parse(id), Title = title });
            }
            return list;
        }
    }
}
