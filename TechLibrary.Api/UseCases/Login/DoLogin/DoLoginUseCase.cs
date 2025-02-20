using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Securiry.Cryptography;
using TechLibrary.Api.Infraestructure.Securiry.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Login.DoLogin
{
    public class DoLoginUseCase
    {
        public ResponseRegisterUserJson Exectute(RequestLoginJson request)
        {
            var dbContext = new TechLibraryDbContext();

            var entity = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
            if (entity is null)
            {
                throw new InvalidLoginException();
            }

            var crypt = new BCryptAlgorithm();
            var passwordIsValid = crypt.Verify(request.Password, entity);
            if (passwordIsValid == false)
            {
                throw new InvalidLoginException();
            }

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisterUserJson
            {
                Name = entity.Name,
                AccessToken = tokenGenerator.Generate(entity)
            };
        }
    }
}
