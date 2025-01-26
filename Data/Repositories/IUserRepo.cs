using HospitalSystemTeamTask.Models;
using System.Collections.Generic;

namespace HospitalSystemTeamTask.Repositories
{
    public interface IUserRepo
    {
        
        User GetUserById(int uid);

        
        int AddUser(User user);

        User GetUserByEmail(string email);

        bool IsValidRole(string roleName);
        void UpdateUser(User user);
        bool EmailExists(string email);
        void UpdatePassword(int uid, string newPassword);
        bool ValidateCurrentPassword(int uid, string currentPassword);
        IQueryable<User> GetUserByRole(string roleName);
        User GetUserByName(string userName);
        string GetUserName(int uid);
    }
}
