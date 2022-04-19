using F1_News.Models;
using F1_News.Models.DB.ArticleRep;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Controllers {
    public class ShopController : Controller {
        private IRepositoryArticle rep = new RepositoryArticle();
        public async Task<IActionResult> Index() {
            try {
                await rep.ConnectAsync();
                List<Article> articles = new List<Article>();
                articles = await rep.GetAllArticlesAsync();
                return View(articles);
            } catch (DbException) {
                return View("systemMessage", new systemMessage("DB-CONTROL", "ERROR-404"));
            } finally {
                await rep.DisconnectAsync();
            }
        }
        public IActionResult Basket() {
            return View();
        }
    }
}
