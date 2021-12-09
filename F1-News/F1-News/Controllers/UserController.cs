using F1_News.Models;
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

       [HttpPost]
        public IActionResult Registration(User userDataFromForm) {
           
            if (userDataFromForm == null) {
                return RedirectToAction("Registration");
            }
            ValidateRegistrationData(userDataFromForm);
            if (ModelState.IsValid) {
                return RedirectToAction("Index");
            }
            return View(userDataFromForm);
            
        }

        private void ValidateRegistrationData(User user) {
            if (user== null) {
                return;
            }
           
            if(user.Username==null || user.Username.Trim().Length < 4) {
                ModelState.AddModelError("Username", "Der Username muss mind. 4 Zeichen lang sein");
            }

        }
    }
}
