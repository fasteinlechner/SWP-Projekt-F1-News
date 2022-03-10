﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB {
    public class RepositoryUser : IRepositoryUser {
        private String connectionString = "Server=localhost;database=f1DB;user=root;password=hallo123!";
        private DbConnection conn;
        public void Connect() {
            if(this.conn == null) {
                this.conn = new MySqlConnection(this.connectionString);
            }
            if(this.conn.State != ConnectionState.Open) {
                this.conn.Open();
            }
        }
        public void Disconnect() {
            if (this.conn !=null && this.conn.State == ConnectionState.Open) {
                this.conn.Close();
            }
        }
        public List<User> GetAllUsers() {
            List<User> users = new List<User>();
            if (this.conn?.State == ConnectionState.Open) {
                DbCommand cmdSelectAll = this.conn.CreateCommand();
                cmdSelectAll.CommandText = "Select * from user";
                using(DbDataReader reader = cmdSelectAll.ExecuteReader()) {
                    while (reader.Read()) {
                        users.Add(new User() {
                            UserId = Convert.ToInt32(reader["user_id"]),
                            Username = Convert.ToString(reader["Username"]),
                            Password = Convert.ToString(reader["Password"]),
                            Firstname = Convert.ToString(reader["Firstname"]),
                            Lastname = Convert.ToString(reader["Lastname"]),
                            Email = Convert.ToString(reader["Email"]),
                            Birthdate = Convert.ToDateTime(reader["Birthdate"]),
                            Gender = (Gender)Convert.ToInt32(reader["Gender"])
                        });
                    }
                }
            }
            return users;
        }
        public User GetUserById(int id) {
            User user = new User();
            if (this.conn?.State == ConnectionState.Open) {
                DbCommand cmdSelectOne = this.conn.CreateCommand();
                cmdSelectOne.CommandText = "select * from user where user_id = @id";
                DbParameter paramId = cmdSelectOne.CreateParameter();
                paramId.ParameterName = "id";
                paramId.DbType = DbType.Int32;
                paramId.Value = id;
                cmdSelectOne.Parameters.Add(paramId);
                using(DbDataReader reader = cmdSelectOne.ExecuteReader()) {
                    if (reader.Read()) {
                        user.UserId = Convert.ToInt32(reader["user_id"]);
                        user.Username = Convert.ToString(reader["username"]);
                        user.Password = Convert.ToString(reader["password"]);
                        user.Firstname = Convert.ToString(reader["firstname"]);
                        user.Lastname = Convert.ToString(reader["lastname"]);
                        user.Email = Convert.ToString(reader["email"]);
                        user.Birthdate = Convert.ToDateTime(reader["birthdate"]);
                        user.Gender = (Gender)Convert.ToInt32(reader["gender"]);
                    }
                }
            }
            return user;
        }
        public bool Insert(User user) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand cmdInsert = this.conn.CreateCommand();
                cmdInsert.CommandText = "insert into user values(null, @username, sha2(@password,512), @firstname, @lastname, @email, @birthdate, @gender);";

                DbParameter username = cmdInsert.CreateParameter();
                username.ParameterName = "username";
                username.DbType = DbType.String;
                username.Value = user.Username;

                DbParameter password = cmdInsert.CreateParameter();
                password.ParameterName = "password";
                password.DbType = DbType.String;
                password.Value = user.Password;

                DbParameter firstname = cmdInsert.CreateParameter();
                firstname.ParameterName = "firstname";
                firstname.DbType = DbType.String;
                firstname.Value = user.Firstname;

                DbParameter lastname = cmdInsert.CreateParameter();
                lastname.ParameterName = "lastname";
                lastname.DbType = DbType.String;
                lastname.Value = user.Lastname;

                DbParameter email = cmdInsert.CreateParameter();
                email.ParameterName = "email";
                email.DbType = DbType.String;
                email.Value = user.Email;

                DbParameter birthdate = cmdInsert.CreateParameter();
                birthdate.ParameterName = "birthdate";
                birthdate.DbType = DbType.DateTime;
                birthdate.Value = user.Birthdate;

                DbParameter gender = cmdInsert.CreateParameter();
                gender.ParameterName = "gender";
                gender.DbType = DbType.Int32;
                gender.Value = user.Gender;

                cmdInsert.Parameters.Add(username);
                cmdInsert.Parameters.Add(password);
                cmdInsert.Parameters.Add(firstname);
                cmdInsert.Parameters.Add(lastname);
                cmdInsert.Parameters.Add(email);
                cmdInsert.Parameters.Add(birthdate);
                cmdInsert.Parameters.Add(gender);

                return cmdInsert.ExecuteNonQuery() == 1;
            }
            return false;
        }
        public bool Update(User newUser) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand cmdUpdate = this.conn.CreateCommand();
                cmdUpdate.CommandText = "update user set username = @username, password = sha2(@password,512), firstname = @firstname, lastname = @lastname, email = @email, birthdate = @birthdate, gender = @gender where user_id = @id";

                DbParameter username = cmdUpdate.CreateParameter();
                username.ParameterName = "username";
                username.DbType = DbType.String;
                username.Value = newUser.Username;

                DbParameter password = cmdUpdate.CreateParameter();
                password.ParameterName = "password";
                password.DbType = DbType.String;
                password.Value = newUser.Password;

                DbParameter firstname = cmdUpdate.CreateParameter();
                firstname.ParameterName = "firstname";
                firstname.DbType = DbType.String;
                firstname.Value = newUser.Firstname;

                DbParameter lastname = cmdUpdate.CreateParameter();
                lastname.ParameterName = "lastname";
                lastname.DbType = DbType.String;
                lastname.Value = newUser.Lastname;

                DbParameter email = cmdUpdate.CreateParameter();
                email.ParameterName = "email";
                email.DbType = DbType.String;
                email.Value = newUser.Email;

                DbParameter birthdate = cmdUpdate.CreateParameter();
                birthdate.ParameterName = "birthdate";
                birthdate.DbType = DbType.DateTime;
                birthdate.Value = newUser.Birthdate;

                DbParameter gender = cmdUpdate.CreateParameter();
                gender.ParameterName = "gender";
                gender.DbType = DbType.Int32;
                gender.Value = newUser.Gender;

                DbParameter id = cmdUpdate.CreateParameter();
                id.ParameterName = "id";
                id.DbType = DbType.Int32;
                id.Value = newUser.UserId;

                cmdUpdate.Parameters.Add(username);
                cmdUpdate.Parameters.Add(password);
                cmdUpdate.Parameters.Add(firstname);
                cmdUpdate.Parameters.Add(lastname);
                cmdUpdate.Parameters.Add(email);
                cmdUpdate.Parameters.Add(birthdate);
                cmdUpdate.Parameters.Add(gender);
                cmdUpdate.Parameters.Add(id);

                return cmdUpdate.ExecuteNonQuery() == 1;
            }
            return false;
        }
        public bool Delete(int userId) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand cmdDelete = this.conn.CreateCommand();
                cmdDelete.CommandText = "delete from user where user_id = @id";

                DbParameter id = cmdDelete.CreateParameter();
                id.ParameterName = "id";
                id.DbType = DbType.Int32;
                id.Value = userId;
                cmdDelete.Parameters.Add(id);

                return cmdDelete.ExecuteNonQuery() == 1;
            }
            return false;
        }
        public bool Login(string username, string password) {
            if(this.conn?.State == ConnectionState.Open) {
                DbCommand cmdLogin = this.conn.CreateCommand();
                cmdLogin.CommandText = "select * from user where username = @username and password = sha2(@password, 512)";

                DbParameter user = cmdLogin.CreateParameter();
                user.ParameterName = "username";
                user.DbType = DbType.String;
                user.Value = username;

                DbParameter passw = cmdLogin.CreateParameter();
                passw.ParameterName = "password";
                passw.DbType = DbType.String;
                passw.Value = password;

                cmdLogin.Parameters.Add(user);
                cmdLogin.Parameters.Add(passw);

                using (DbDataReader reader = cmdLogin.ExecuteReader()) {
                    return reader.Read();
                }
            }
            return false;
        }

       
    }
}
