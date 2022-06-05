using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class Bestellung {
        private int bestellID;
        private int userID;
        private int articleID;
        private int anzahl;
        private int bestellNummer;
        public int BestellID {
            get { return this.bestellID; }
            set {
                if (value > 0) {
                    this.bestellID = value;
                }
            }
        }
        public int UserID {
            get { return this.userID; }
            set {
                if (value > 0) {
                    this.userID = value;
                }
            }
        }
        public int ArticleID {
            get { return this.articleID; }
            set {
                if (value > 0) {
                    this.articleID = value;
                }
            }
        }
        public int Anzahl {
            get { return this.anzahl; }
            set {
                if (value > 0) {
                    this.anzahl = value;
                }
            }
        }
        public int BestellNummer {
            get { return this.bestellNummer; }
            set {
                if(value > 0) {
                    this.bestellNummer = value;
                }
            }
        }
        
        public Bestellung() : this(0, 0, 0, 1, 1) { }

        public Bestellung(int bId, int aId, int uId, int anz, int bNr) {
            this.BestellID = bId;
            this.ArticleID = aId;
            this.UserID = uId;
            this.Anzahl = anz;
            this.BestellNummer = bNr;
        }

    }
}
