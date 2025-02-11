using HospitalSystemTeamTask.Models;
using HospitalSystemTeamTask.Shared.Helper;

namespace HospitalSystemTeamTask.Services
{
    public interface IAuthService
    {
        JwtTokenResponse GenerateToken(User user);
        Task SaveTokenToCookie(string token);
        Task<int> GetUserIdFromToken();
    }
}