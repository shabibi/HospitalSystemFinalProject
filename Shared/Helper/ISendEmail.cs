namespace HospitalSystemTeamTask.Shared.Helper
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}