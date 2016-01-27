using mbrBlog.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mbrBlog.DAL
{
    public static class ArticleDal
    {
        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static int UpdateArticle(Article article)
        {
            using (BlogEntities blogEntities = new BlogEntities())
            {
                var changeArticle = blogEntities.Article.First(a => a.Id == article.Id);

                //changeArticle.PublicationTime = article.PublicationTime;
                changeArticle.Title = article.Title;
                changeArticle.Content = article.Content;
                var ArticleLabelList = article.ArticleLabel;
                var newLabelList =new List<ArticleLabel>();
                var oldLabelList = new List<ArticleLabel>(changeArticle.ArticleLabel);
                foreach (var label in ArticleLabelList)
                {
                    var labelFromDb = (from l in blogEntities.ArticleLabel where l.Id == label.Id select l).FirstOrDefault();
                    newLabelList.Add(labelFromDb);
                }

                foreach (var label in newLabelList)
                {
                    if (!changeArticle.ArticleLabel.Contains(label))
                    {
                        changeArticle.ArticleLabel.Add(label);
                    }
                }

                foreach (var label in oldLabelList)
                {
                    if (!newLabelList.Contains(label))
                    {
                        changeArticle.ArticleLabel.Remove(label);
                    }
                }
                blogEntities.SaveChanges();
            }
            return article.Id;
        }

        public static int ArticlePvAdd(int id)
        {
            using (var blogEntities = new BlogEntities())
            {
                var article = blogEntities.Article.First(a => a.Id == id);
                article.PvCount += 1;
                blogEntities.SaveChanges();
                return article.PvCount.Value;
            }
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public static int AddArticle(Article article)
        {
            var articleTemp = article;
            using (BlogEntities blogEntities = new BlogEntities())
            {
                var ArticleLabelList = article.ArticleLabel;
                article.ArticleLabel = null;
                var newlabelList = new List<ArticleLabel>();
                foreach (var label in ArticleLabelList)
                {
                    var labelFromDb = (from l in blogEntities.ArticleLabel where l.Id == label.Id select l).FirstOrDefault();

                    newlabelList.Add(labelFromDb);
                }
                article.ArticleLabel = newlabelList;
                blogEntities.Article.Add(article);
                blogEntities.SaveChanges();
                articleTemp = article;
            }
            return articleTemp.Id;
        }
        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteArticle(int id)
        {
            var article = GetArticle(id);
            if (article == null)
                return;

            using (BlogEntities blogEntities = new BlogEntities())
            {
                blogEntities.Set<Article>().Remove(article);
                blogEntities.SaveChanges();
            }
        }
        /// <summary>
        /// 获取博客
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static Article GetArticle(int articleId)
        {
            Article article = null;
            using (BlogEntities blogEntities = new BlogEntities())
            {
                article = (from a in blogEntities.Article.Include("ArticleLabel") where a.Id == articleId select a).SingleOrDefault();
            }
            return article;
        }
        /// <summary>
        /// 按条件检索博客
        /// </summary>
        /// <param name="selectArticleRequest"></param>
        /// <returns></returns>
        public static List<Article> SelectArticleList(SelectArticleRequest selectArticleRequest)
        {
            List<Article> list = null;
            using (BlogEntities blogEntities = new BlogEntities())
            {
                if (String.IsNullOrEmpty(selectArticleRequest.Title))
                {
                    selectArticleRequest.Title = "";
                }
                if (selectArticleRequest.BeginPublicationTime == null)
                {
                    selectArticleRequest.BeginPublicationTime = DateTime.MinValue;
                }
                if (selectArticleRequest.EndPublicationTime == null)
                {
                    selectArticleRequest.EndPublicationTime = DateTime.MaxValue;
                }
                if (selectArticleRequest.Page == 0)
                {
                    selectArticleRequest.Page = 1;
                }
                if (selectArticleRequest.CountPerPage == 0)
                {
                    selectArticleRequest.CountPerPage = int.MaxValue;
                }
                var articles = (from article in blogEntities.Article.Include("ArticleLabel")
                                where article.Title.Contains(selectArticleRequest.Title)
                                && article.PublicationTime >= selectArticleRequest.BeginPublicationTime
                                && article.PublicationTime <= selectArticleRequest.EndPublicationTime
                                orderby article.PublicationTime descending
                                select article)
                               .Skip((selectArticleRequest.Page - 1) * selectArticleRequest.CountPerPage)
                               .Take(selectArticleRequest.CountPerPage);
                list = new List<Article>(articles.ToArray());
            }
            return list;
        }
        /// <summary>
        /// 根据标签名获取标签
        /// </summary>
        /// <param name="labelTitle"></param>
        /// <returns></returns>
        public static ArticleLabel GetLabelByTitle(string labelTitle)
        {
            ArticleLabel articleLabel = null;
            using (BlogEntities blogEntities = new BlogEntities())
            {
                articleLabel = (from label in blogEntities.ArticleLabel.Include("Article") where label.Title == labelTitle select label).FirstOrDefault();
            }
            return articleLabel;
        }
        /// <summary>
        /// 获取所有标签
        /// </summary>
        /// <returns></returns>
        public static List<ArticleLabel> GetAllLabel()
        {
            List<ArticleLabel> list = null;
            using (BlogEntities blogEntities = new BlogEntities())
            {
                var labelList = (from label in blogEntities.ArticleLabel select label);
                list = new List<ArticleLabel>(labelList.ToList<ArticleLabel>());

            }
            return list;
        }

        /// <summary>
        /// 获取所有标签和标签中的所有博文
        /// </summary>
        /// <returns></returns>
        public static List<ArticleLabel> GetAllLabelAndArticle()
        {
            List<ArticleLabel> list = null;
            using (BlogEntities blogEntities = new BlogEntities())
            {
                var labelList = (from label in blogEntities.ArticleLabel.Include("Article") select label);
                list = new List<ArticleLabel>(labelList.ToList<ArticleLabel>());

            }
            return list;
        }

        //private static ICollection<ArticleLabel> LabelItemsGetId(ICollection<ArticleLabel> labelList) 
        //{
        //    var newlabelList
        //    foreach (var label in articleList)
        //    {
        //        var labelFromCheck = GetLabelByTitle(label.Title);
        //        if (labelFromCheck != null)
        //        {
        //            label.Id = labelFromCheck.Id;
        //        }
        //    }
        //    return articleList;
        //}
    }
}