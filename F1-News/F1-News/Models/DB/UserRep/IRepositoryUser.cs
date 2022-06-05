using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB.UserRep {
    public interface IRepositoryUser {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> InsertAsync(User user);
        Task<bool> UpdateAsync(User newUser);
        Task<bool> DeleteAsync(int userId);
        Task<bool> LoginAsync(string username, string password);
        Task<int> GetIDByUserPW(string username, string password);


    }
}
