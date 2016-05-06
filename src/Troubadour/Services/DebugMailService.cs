using System.Diagnostics;

namespace Troubadour.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail to {to} from {from}");
            return true;
        }
    }
}
