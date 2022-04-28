using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class systemMessage {
        public string Header { get; set; }
        public string MessageText { get; set; }
        public List<String> ButtonInfo { get; set; }

        public systemMessage () : this("ERROR-404", "",new List<string>()) { }
         
        public systemMessage(string header, string message) {
            this.Header = header;
            this.MessageText = message;
        }

        //with Button-Info
        public systemMessage(string header, string message, List<String> list) {
            this.Header = header;
            this.MessageText = message;
            this.ButtonInfo = list;
        }
    }
}
