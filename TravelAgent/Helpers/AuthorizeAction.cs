using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                string token = authHeader.Substring("Bearer ".Length).Trim();

                if (_auth.ValidateCurrentToken(token))
                {
                    var role = _auth.GetClaim(token, "Role");
                    if (role == _claim.Value)
                        return;
                }
            }
            context.Result = new JsonResult(new { message = "Unauthorized." }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
    }
}
