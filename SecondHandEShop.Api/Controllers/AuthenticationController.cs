using Domain.CustomExceptions;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondHandEShop.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(ShopApplicationUser user)
        {
            try
            {
                var result = await _userService.SignUp(user);
                return Created("", result);
            }
            catch (EmailAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(ShopApplicationUser user)
        {
            try
            {
                var result = await _userService.SignIn(user);
                return Ok(result);
            }
            catch (InvalidEmailPasswordException e)
            {
                return StatusCode(409, e.Message);
            }
        }
    }

}
