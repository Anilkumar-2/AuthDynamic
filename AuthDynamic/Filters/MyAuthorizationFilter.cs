namespace AuthDynamic.Filters
{
    using DataAccessLayer.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

    public class MyAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private SampleDBContext sampleDBContext;

        public MyAuthorizationFilter()
        {
            sampleDBContext = new SampleDBContext();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string s = context.HttpContext.Request.Path;

            string[] ApiArray = s.Split('/');
            string tokenBearer = context.HttpContext.Request.Headers["Authorization"];
            var token= tokenBearer.Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Access the claims in the decoded token
            var claims = jwtToken.Claims;

            // Retrieve specific claims by their names
            string userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            var userRoleDB = sampleDBContext.ApiAccessRoles.Where(x => x.ControllerName.ToLower() == ApiArray[2] && x.ApiName.ToLower() == ApiArray[3]).Select(x => x.RolesAccess).ToList();

            if (userRole!=userRoleDB[0])
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
