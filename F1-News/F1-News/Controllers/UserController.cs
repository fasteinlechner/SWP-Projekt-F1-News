using F1_News.Models;
using F1_News.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Controllers {
    public class UserController : Controller {

        private IRepositoryUser rep = new RepositoryUser();
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
                try {
                    rep.Connect();
                    if (rep.Insert(userDataFromForm)) {
                        return View("systemMessage", new systemMessage("Registration-Control", "Sie haben sich erfolgreich registriert!"));
                    } else {
                        return View("systemMessage", new systemMessage("Registration-Control", "Etwas ist schiefgelaufen!"));
                    }
                } catch (DbException) {
                    return View("systemMessage", new systemMessage());
                } finally {
                    rep.Disconnect();
                }
            }
            return View(userDataFromForm);
            
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User userDataFromForm) {
            try {
               rep.Connect();
               if(rep.Login(userDataFromForm.Username, userDataFromForm.Password)) {
                   return View("systemMessage", new systemMessage("LOGIN-Control", "Sie haben sich erfolgreich angemeldet"));
               } else {
                   return View("systemMessage", new systemMessage("LOGIN-Control", "Benutzer oder Passwort falsch"));
               }
            } catch (DbException) {
                return View("systemMessage", new systemMessage());
            } finally {
                rep.Disconnect();
            }
        }
        
        
        private void ValidateRegistrationData(User user) {
            if (user == null) {
                return;
            }

            //Username
            if (user.Username == null || user.Username.Trim().Length < 4) {
                ModelState.AddModelError("Username", "Der Username muss mind. 4 Zeichen lang sein");
            }

            //Password
            if ((user.Password == null) || (user.Password.Length < 8)) {
                ModelState.AddModelError("Password", "Das Passwort muss mindestens 8 Zeichen lang sein!");
            }

            //Birthdate
            if (user.Birthdate > DateTime.Now) {
                ModelState.AddModelError("Birthdate", "Das Geburtsdatum darf nicht in der Zukunft sein!");
            }

            //EMail
            if (user.Email != null) {
                if (!user.Email.Contains("@")) {
                    ModelState.AddModelError("EMail", "Geben Sie eine gültige E-Mail-Adresse an!");
                }
            } else {
                ModelState.AddModelError("EMail", "Geben Sie eine E-Mail-Adresse an!");
            }
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
