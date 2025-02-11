namespace HospitalSystemTeamTask.Shared.Helper
{
    public class JwtSettings
    {

        public string SecretKey { get; set; }

        public int ExpirationInMinutes { get; set; }
    }
}
