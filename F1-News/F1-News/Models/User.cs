using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models {
    public class User {
        private int _userId;
        private DateTime _birthdate;

        public int UserId {
            get { return this._userId; }
            set {
                if (value > 0) {
                    this._userId = value;
                }
            }
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate {
            get { return this._birthdate; }
            set {
                if(value< DateTime.Now) {
                    this._birthdate = value;
                }
            }

        }
        public Gender Gender { get; set; }


        public User() : this(0, "defaultUser", "12345678", "", "", "", DateTime.MinValue, Gender.notSpeciefied) { }
        public User (int userId, string username, string password, string firstname, string lastname, string email, DateTime birth, Gender gender) {
            this.UserId = userId;
            this.Username = username;
            this.Password = password;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Birthdate = birth;
            this.Gender = gender;
        }
    }
}
