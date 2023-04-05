using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelAgent.Utility;
using Microsoft.AspNetCore.Authorization;

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
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var token = context.HttpContext.Session.GetString(StaticDetails.UserToken);
            if (_auth.ValidateCurrentToken(token))
            {
                var role = _auth.GetClaim(token, "Role"); 
                if (role == _claim.Value)
                    return;
            }
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
