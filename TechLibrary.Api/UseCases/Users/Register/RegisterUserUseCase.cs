using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Securiry.Cryptography;
using TechLibrary.Api.Infraestructure.Securiry.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisterUserJson Execute(RequestUserJson request)
        {
            var dbContext = new TechLibraryDbContext();

            Validate(request, dbContext);

            var crypt = new BCryptAlgorithm();

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = crypt.HashPassword(request.Password)
            };

            
            dbContext.Add(entity);
            dbContext.SaveChanges();

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisterUserJson
            {
                Name = entity.Name,
                AccessToken = tokenGenerator.Generate(entity)
            };
        }

        private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email)); 

            if (existUserWithEmail)
            {
                result.Errors.Add(new ValidationFailure("E-mail", "E-mail já registrado na plataforma!"));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
