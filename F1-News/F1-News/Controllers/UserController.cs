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

        [HttpGet]
        public IActionResult Login() {
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
           
            //Username
            if(user.Username==null || user.Username.Trim().Length < 4) {
                ModelState.AddModelError("Username", "Der Username muss mind. 4 Zeichen lang sein");
            }

            //Password
            if ((user.Password == null) || (user.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens 8 Zeichen lang sein!");
            }

            //Birthdate
            if (user.Birthdate > DateTime.Now)
            {
                ModelState.AddModelError("Birthdate", "Das Geburtsdatum darf nicht in der Zukunft sein!");
            }

            //EMail
            if (user.Email != null)
            {
                if (!user.Email.Contains("@"))
                {
                    ModelState.AddModelError("EMail", "Geben Sie eine gültige E-Mail-Adresse an!");
                }
            }
            else
            {
                ModelState.AddModelError("EMail", "Geben Sie eine E-Mail-Adresse an!");
            }
        }

        [HttpPost]
        public IActionResult Login(User userDataFromForm) {

            if (userDataFromForm == null)
            {
                return RedirectToAction("Login");
            }
            ValidateLoginData(userDataFromForm);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(userDataFromForm);

        }

        private void ValidateLoginData(User user) {
            if (user == null)
            {
                return;
            }

            //Username
            if (user.Username == null || user.Username.Trim().Length < 4)
            {
                ModelState.AddModelError("Username", "Der Username muss mind. 4 Zeichen lang sein");
            }

            //Password
            if ((user.Password == null) || (user.Password.Length < 8))
            {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens 8 Zeichen lang sein!");
            }

        }
    }
}
