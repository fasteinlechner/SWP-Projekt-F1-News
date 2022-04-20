using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.Services {
    public interface IMailService {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
