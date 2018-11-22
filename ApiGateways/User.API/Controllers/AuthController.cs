using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.API.Entities;
using User.API.Services;

namespace User.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Constructor
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(IUserDetails), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Authenticate([FromBody]Auth request)
        {
            var user = await _userService.AuthenticateAsync(request.Username, request.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}