using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Login.DoLogin;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult DoLogin(RequestLoginJson request)
        {
            var useCase = new DoLoginUseCase();

            var response = useCase.Exectute(request);

            return Ok(response);
        }
    }
}
