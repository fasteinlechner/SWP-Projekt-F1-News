using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Controllers {
    public class UserController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Registration() {
            return View();
        }

       // [HttpPost]
       /* public IActionResult Registration(User userDataFromForm) {

        }*/
    }
}
