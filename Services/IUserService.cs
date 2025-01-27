using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IUserService
    {
        int AddStaff(User InputUser);
        void AddSuperAdmin(UserInputDTO InputUser);
        void AddUser(User user);
        User AuthenticateUser(string email, string password);
        void DeactivateUser(int uid);
        bool EmailExists(string email);
        User GetUserById(int uid);
        User GetUserByName(string userName);
        string GetUserName(int userId);
        void UpdatePassword(int uid, string currentPassword, string newPassword);
        void UpdateUser(User user);
        UserOutputDTO GetUserData(string? userName, int? uid);
        IEnumerable<UserOutputDTO> GetUserByRole(string roleName);
    }
}