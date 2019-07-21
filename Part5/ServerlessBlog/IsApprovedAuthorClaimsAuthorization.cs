using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FunctionMonkey.Abstractions;

namespace ServerlessBlog
{
    public class IsApprovedAuthorClaimsAuthorization : IClaimsPrincipalAuthorization
    {
        private static readonly string[] PreapprovedAuthors =
        {
            "WL6PsrWcW7zvG0a4otXTAIiBUMUpDT5U@clients"
        };
        
        public Task<bool> IsAuthorized(
            ClaimsPrincipal claimsPrincipal,
            string httpVerb,
            string requestUrl)
        {
            string subject = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Task.FromResult(subject != null && PreapprovedAuthors.Contains(subject));
        }
    }
}