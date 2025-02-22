using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;

namespace TechLibrary.Api.Services.LoggedUser
{
    public class LoggedUserService
    {
        private readonly HttpContext _httpContext;
        public LoggedUserService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public User GetLoggedUser(TechLibraryDbContext dbContext)
        {
            var authentication = _httpContext.Request.Headers.Authorization.ToString();
            var token = authentication["Bearer".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            var userId = Guid.Parse(identifier);
            
            return dbContext.Users.First(user => user.Id == userId);
        }
    }
}
