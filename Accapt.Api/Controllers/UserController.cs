using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/ManageUsers(V1)")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserServies _registerServies;
        public UserController(IRegisterUserServies registerUserServies)
        {
            _registerServies = registerUserServies ?? throw new ArgumentException(nameof(registerUserServies));
        }

        [HttpPost("RGU(V1)")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO register)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(register == null)
                return BadRequest("Null Exeption");

            var resgiterUser = await _registerServies.RegisterUser(register);

            if (!resgiterUser.ISuucess)
                return BadRequest(resgiterUser.Message);


            return Ok(resgiterUser.Data);

        }
    }
}
