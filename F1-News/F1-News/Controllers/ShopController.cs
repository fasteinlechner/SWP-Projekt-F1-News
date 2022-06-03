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
                List<Article> a;
                

                await rep.ConnectAsync();
                List<Article> articles = await rep.GetAllArticlesAsync();
                return View(articles);
            } catch (DbException) {
                systemMessage model = new("DB-CONTROL", "ERROR-404");
                return base.View("systemMessage", model);
            } finally {
                await rep.DisconnectAsync();
            }
        }
        public IActionResult Basket() {
            return View();
        }

        [HttpGet]
        public IActionResult ArticleDetail(Article a) {
            return View(a);
        }
    }
}
