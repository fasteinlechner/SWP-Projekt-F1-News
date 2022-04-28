using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class MailRequest {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailRequest(string email, string sub, string bod) {
            ToEmail = email;
            Subject = sub;
            Body = bod;
        }

        public MailRequest() {
        }
    }
}
