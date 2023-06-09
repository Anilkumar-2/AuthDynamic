namespace AuthDynamic.Middlewares
{
    using DataAccessLayer.Model;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public LoginMiddleware(RequestDelegate next, IConfiguration configuration, IServiceProvider serviceProvider, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;

        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SampleDBContext>();

                var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8);

                var requestBody =await reader.ReadToEndAsync();
                var requestBodyJson = JsonSerializer.Deserialize<User>(requestBody);
                var email = requestBodyJson.Email;

                var user = dbContext.User.Where(x => x.Email == email).FirstOrDefault();
                if (String.IsNullOrEmpty(user.Email))
                {
                    httpContext.Response.StatusCode = 404;
                }
                var token = GenerateJwtToken(user);
                Console.WriteLine(token);
                await httpContext.Response.WriteAsync(token);
                await _next(httpContext);
            }
        }

        public string GenerateJwtToken(User user)
        {
            try
            {
                var secretKey = _configuration.GetSection("JwtConfig").GetSection("Key").Value;
                var expMin = _configuration.GetSection("JwtConfig").GetSection("ExpMin").Value;

                var byteKey = Encoding.ASCII.GetBytes(secretKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                UserRolesEnum userRole = (UserRolesEnum)user.RoleId;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, userRole.ToString()),
                }),
                    Expires = DateTime.UtcNow.AddMinutes(double.Parse(expMin)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            catch(Exception e)
            {
                throw e;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class LogicMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseLogicMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.Map("/login", builder =>
    //        { 
    //            builder.UseMiddleware<LoginMiddleware>(); 
    //        });
    //    }
    //}
}
