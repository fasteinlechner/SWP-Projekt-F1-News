﻿using F1_News.Models;
using F1_News.Models.DB.UserRep;
using F1_News.Models.Services;
using F1_News.Models.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Controllers {
    public class UserController : Controller {

        public static string lockStr = "~/img/lock.png";
        
        private IRepositoryUser rep = new RepositoryUser();
        //private readonly IMailService mailService = new MailService();
        
        public async Task<IActionResult> Index(/*User user*/) {
            //if (user != null)
            //{
            //    HttpRequest req = HttpResponseWritingExtensions
            //}
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registration() {
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
                        /*MailRequest request = new();
                        request.ToEmail = "fsteinlechner4@gmail.com";
                        request.Subject = "F1-NEWS";
                        request.Body = "WILLKOMMEN zu unserem Newsletter!";

                        await mailService.SendEmailAsync(request);*/
                        return View("systemMessage", new systemMessage("Registration-Control", "Sie haben sich erfolgreich registriert!"));
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
