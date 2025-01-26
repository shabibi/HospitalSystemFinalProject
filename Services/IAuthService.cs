using HospitalSystemTeamTask.Helper;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(User user);
        Task SaveTokenToCookie(string token);
    }
}