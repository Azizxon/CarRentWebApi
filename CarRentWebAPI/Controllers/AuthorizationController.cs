using CarRent.Application;
using CarRent.Common;
using CarRentWebAPI.Models;
using CarRentWebAPI.Security;
using Microsoft.AspNetCore.Mvc;

namespace CarRentWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("client")]
    public class AuthorizationController : Controller
    {
         public AuthorizationController(IClientCarRental clientCarRental, IJwIssuer jwIssuer,SecuritySettings securitySettings)
         {
             _clientCarRental = clientCarRental;
            _jsIssuer = jwIssuer;
             _securitySetting = securitySettings;
         }
        [HttpPost]
        [Route("clients/token")]
        public IActionResult Login([FromBody]LoginRequest request)
        {
            var credentials = Credentials.FromRawData(request.Email, request.Password);
            if (credentials.Equals(_securitySetting.AdminCredentials))
            {
                return Ok(_jsIssuer.IssueJwt(Claims.Roles.Admin, null));
            }
            var client = _clientCarRental.FindBy(Credentials.FromRawData(request.Email, request.Password));
            if (client != null)
            {
                return Ok(_jsIssuer.IssueJwt(Claims.Roles.User, client.Id));
            }
            return Unauthorized();
        }
        private IJwIssuer _jsIssuer;
        private IClientCarRental _clientCarRental;
        private SecuritySettings _securitySetting;
    }
}