using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class Article {
        private decimal price;

        public decimal Price {
            get { return this.price; }
            set {
                if(value > 0)
                {
                    this.price = value;
                }
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Items { get; set; }
    }
}
