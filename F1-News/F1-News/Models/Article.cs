using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class Article {
        private int articleID;
        private decimal preis;

        public int ArticleID {
            get { return this.articleID;}
            set {
                if (value > 0) {
                    this.articleID = value;
                }
            }
        }
        public decimal Preis {
            get { return this.preis; }
            set {
                if(value > 0)
                {
                    this.preis = value;
                }
            }
        }
        public string Bezeichnung { get; set; }
        public string Beschreibung { get; set; }
        public string ImageLink { get; set; }
        public int Elemente { get; set; }
        public Article() : this(0, "Article", "das ist ein Artikel", 0, 0,"") { }
        public Article(int articleId, string bez, string bes, int preis , int elemente, string image) {
            this.ArticleID = articleId;
            this.Bezeichnung= bez;
            this.Beschreibung = bes;
            this.Preis = preis;
            this.Elemente = elemente;
            this.ImageLink = image;
        }
        
    }
}
