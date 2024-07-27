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
        private readonly ILoginUserServies _loginUserServies;
        public UserController(IRegisterUserServies registerUserServies,
            ILoginUserServies loginUserServies)
        {
            _registerServies = registerUserServies ?? throw new ArgumentException(nameof(registerUserServies));
            _loginUserServies = loginUserServies ?? throw new AbandonedMutexException(nameof(loginUserServies));
        }

        #region Register User

        [HttpPost("RGU(V1)")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO register)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var resgiterUser = await _registerServies.RegisterUser(register);

            if (register == null)
                return BadRequest(resgiterUser.Message);

            if (!resgiterUser.ISuucess)
                return BadRequest(resgiterUser.Message);

            return Ok(resgiterUser);

        }

        #endregion

        #region Login User

        [HttpPost("LGU(V1)")]
        public async Task<IActionResult> Loginuser(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginUserStatuce = await _loginUserServies.LoginUser(loginUser);

            if (!loginUserStatuce.ISuucess)
                return BadRequest(loginUserStatuce.Message);

            return Ok(loginUserStatuce.Message);
        }

        #endregion
    }
}
