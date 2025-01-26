using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace HospitalSystemTeamTask.Services
{
    public class SMS : ISMS
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromPhoneNumber;

        public SMS(IConfiguration configuration)
        {
            var twilioConfig = configuration.GetSection("Twilio");
            _accountSid = twilioConfig["AccountSid"];
            _authToken = twilioConfig["AuthToken"];
            _fromPhoneNumber = twilioConfig["FromPhoneNumber"];

            // Initialize Twilio client
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendSmsAsync(string toPhoneNumber, string message)
        {
            try
            {
                await MessageResource.CreateAsync(
                    to: new PhoneNumber(toPhoneNumber),
                    from: new PhoneNumber(_fromPhoneNumber),
                    body: message
                );
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error sending SMS: {ex.Message}");
                throw; // Re-throw the exception to be handled by calling code
            }
        }
    }
}
