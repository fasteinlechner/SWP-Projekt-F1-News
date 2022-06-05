using F1_News.Models;
using F1_News.Models.DB.ArticleRep;
using F1_News.Models.DB.BestellungRep;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Controllers {
    public class ShopController : Controller {
        private IRepositoryArticle rep = new RepositoryArticle();
        private IRepositoryBestellung repBes= new RepositoryBestellung();
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
        

        [HttpGet]
        public async Task<IActionResult> ArticleDetail(int id) {
            try {
                await rep.ConnectAsync();
                Article article = await rep.GetArticleByIDAsync(id);
                return View(article);
            } catch (DbException) {
                systemMessage model = new("DB-CONTROL", "ERROR-404");
                return base.View("systemMessage", model);
            } finally {
                await rep.DisconnectAsync();
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Basket() {
            try {
                await repBes.ConnectAsync();
                await rep.ConnectAsync();
                if (HttpContext.Session.GetInt32("bNr") == null) {
                    List<String> list = new List<string>();
                    list.Add("/user/login");
                    list.Add("Zum LOGIN");
                    systemMessage model = new("Registration-Control", "Sie sind NICHT ANGEMELDET!", list);
                    return base.View("systemMessage", model);
                }
                List<Bestellung> bestellungen = await repBes.GetBestellungbyBNrAsync((int)HttpContext.Session.GetInt32("bNr"), (int)HttpContext.Session.GetInt32("uId"));
                List<Article> articles = new List<Article>();
                foreach(Bestellung b in bestellungen) {
                    articles.Add(await rep.GetArticleByIDAsync(b.ArticleID));
                }
                return View(articles);
            } catch (DbException db) {
                systemMessage model = new("DB-CONTROL", Convert.ToString(db.Message));
                return base.View("systemMessage", model);
            } finally {
                await repBes.DisconnectAsync();
                await rep.DisconnectAsync();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddToBasket (int id) {
            try {
                await repBes.ConnectAsync();
                if (HttpContext.Session.GetInt32("bNr") == null) {
                    List<String> list = new List<string>();
                    list.Add("/user/login");
                    list.Add("Zum LOGIN");
                    systemMessage model = new("Registration-Control", "Sie sind NICHT ANGEMELDET!", list);
                    return base.View("systemMessage", model);
                }
                Bestellung b = new Bestellung();
                b.ArticleID = id;
                b.UserID = (int)HttpContext.Session.GetInt32("uId");
                b.Anzahl = 1;
                b.BestellNummer = (int)HttpContext.Session.GetInt32("bNr");
                if (await repBes.InsertAsync(b)) {
                    return RedirectToAction("Basket");
                } else {
                    systemMessage model = new("DB-CONTROL", "INSERT-FAILURE");
                    return base.View("systemMessage", model);
                }
            } catch(DbException) {
                systemMessage model = new("DB-CONTROL", "ERROR-404");
                return base.View("systemMessage", model);
            } finally {
                await repBes.DisconnectAsync();
            }
        }
    }
}
