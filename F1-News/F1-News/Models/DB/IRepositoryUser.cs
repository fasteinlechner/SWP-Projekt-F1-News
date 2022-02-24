using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB {
    public interface IRepositoryUser {
        void Connect();
        void Disconnect();
        User GetUserById(int id);
        List<User> GetAllUsers();
        bool Insert(User user);
        bool Update(User newUser);
        bool Delete(int userId);
        bool Login(string username, string password);

    }
}
