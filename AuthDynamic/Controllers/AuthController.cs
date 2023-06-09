namespace AuthDynamic.Controllers
{
    using DataAccessLayer.Model;
    using DataAccessLayer.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly SampleDBContext _sampleDBContext;
        private readonly IUserData _userData;
        public AuthController(SampleDBContext sampleDBContext, IUserData userData)
        {
            _sampleDBContext = sampleDBContext;
            _userData = userData;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(User user)
        {
            var response = await _userData.RegisterUser(user);
            
            return Ok(response);
        }
    }
}
