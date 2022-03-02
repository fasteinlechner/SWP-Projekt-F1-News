using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class systemMessage {
        public string Header { get; set; }
        public string MessageText { get; set; }

        public systemMessage () : this("ERROR-404", "") { }
        public systemMessage(string header, string message) {
            this.Header = header;
            this.MessageText = message;
        }
    }
}
