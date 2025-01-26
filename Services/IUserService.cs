﻿using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IUserService
    {
        Task AddStaff(User InputUser);
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
        User AuthenticateUser(string email, string password);
        void UpdatePassword(int uid, string currentPassword, string newPassword);
        void AddSuperAdmin(UserInputDTO InputUser);
        bool EmailExists(string email);
        int AddStaff(User InputUser);
        UserOutputDTO GetUserData(string? userName, int? uid);
        IEnumerable<UserOutputDTO> GetUserByRole(string roleName);
    }
}