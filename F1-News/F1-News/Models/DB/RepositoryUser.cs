using MySql.Data.MySqlClient;
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
            throw new NotImplementedException();
        }
        public bool Update(User newUser) {
            throw new NotImplementedException();
        }
        public bool Delete(int userId) {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password) {
            throw new NotImplementedException();
        }

       
    }
}
