using F1_News.Models;
using F1_News.Models.DB.UserRep;
using F1_News.Models.Helpers;
using F1_News.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace F1_News.Controllers {
    public class UserController : Controller {
        public const string L = "~/img/lock.png";
        public static string lockStr = L;

        private readonly IMailService mailService;
        public UserController(IMailService mailServ) {
            mailService = mailServ;
        }

        
        private IRepositoryUser rep = new RepositoryUser();
        
        
#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        public async Task<IActionResult> Index() {
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
            return View();
        }

        [HttpGet]
#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        public async Task<IActionResult> Registration() {
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User userDataFromForm) {
           
            if (userDataFromForm == null) {
                return RedirectToAction("Registration");
            }
            ValidateRegistrationData(userDataFromForm);
            if (ModelState.IsValid) {
                try {
                    await rep.ConnectAsync();
                    if (await rep.InsertAsync(userDataFromForm)) {
                        PersonalizedMail request = new();
                        request.ToEmail = userDataFromForm.Email;
                        request.Firstname = userDataFromForm.Firstname;
                        request.Username = userDataFromForm.Username;

                        await mailService.SendWelcomeEmailAsync(request);
                        List<String> list = new List<string>();
                        list.Add("/user/login");
                        list.Add("Zum LOGIN");
                        return View("systemMessage", new systemMessage("Registration-Control", "Sie haben sich erfolgreich registriert!", list));
                    } else {
                        return View("systemMessage", new systemMessage("Registration-Control", "Etwas ist schiefgelaufen!"));
                    }
                } catch (DbException) {
                    return View("systemMessage", new systemMessage());
                } finally {
                    await rep.DisconnectAsync();
                }
            }
            return View(userDataFromForm);
            
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User userDataFromForm) {
            try {
               await rep .ConnectAsync();
               if(await rep .LoginAsync(userDataFromForm.Username, userDataFromForm.Password)) {
                    HttpContext.Session.SetString("uname", userDataFromForm.Username);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", userDataFromForm);
                    if (userDataFromForm.Username.Equals("adminF1")) {
                        return RedirectToAction("AdminView");
                    }
                    Console.WriteLine("INFO: "+HttpContext.Session.GetString("uname"));
                    //Schloss-Icon auswechseln (=> entsperrt)
                    lockStr = "~/img/lock_open.png";
                    return RedirectToAction("index", "user", userDataFromForm);
               } else {
                   return View("systemMessage", new systemMessage("LOGIN-Control", "Benutzer oder Passwort falsch"));
               }
            } catch (DbException) {
                return View("systemMessage", new systemMessage());
            } finally {
                await rep .DisconnectAsync();
            }
        }
        [HttpGet]
        public async Task<IActionResult> AdminView() {
            try {
                await rep.ConnectAsync();
                List<User> users= await rep.GetAllUsersAsync();
                return View(users);
            } catch (DbException) {
                return View("systemMessage", new systemMessage());
            } finally {
                await rep.DisconnectAsync();
            }
            
        }

        [HttpGet]

        public IActionResult Logout() {
            HttpContext.Session.Remove("uname");
            return View("systemMessage", new systemMessage("LOGOUT-Control","Erfolgreich ausgeloggt"));
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
        
        private void changeLock() {

        }


    }
}
