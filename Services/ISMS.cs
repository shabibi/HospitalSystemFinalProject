
namespace HospitalSystemTeamTask.Services
{
    public interface ISMS
    {
        Task SendSmsAsync(string toPhoneNumber, string message);
    }
}