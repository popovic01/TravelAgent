using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelAgent.Utility;

namespace TravelAgent.Helpers
{
    public class AuthorizeAction : IAuthorizationFilter
    {
        readonly Claim _claim;
        private readonly IAuthHelper _auth;

        public AuthorizeAction(Claim claim, IAuthHelper auth)
        {
            _claim = claim;
            _auth = auth;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Session.GetString(StaticDetails.UserToken);
            if (_auth.ValidateCurrentToken(token))
            {
                var role = _auth.GetClaim(token, "Role"); //ovo se ne nalazi u claimu
                if (role == _claim.Value)
                    return;
            }
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
