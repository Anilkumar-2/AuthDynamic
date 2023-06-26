namespace AuthDynamic.Controllers
{
    using AuthDynamic.Filters;
    using AutoMapper;
    using DataAccessLayer.DTO;
    using DataAccessLayer.Model;
    using DataAccessLayer.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly SampleDBContext _sampleDBContext;
        private readonly IUserData _userData;
        private readonly IMapper _mappper;
        public HomeController(SampleDBContext sampleDBContext, IUserData userData, IMapper mapper)
        {
            _sampleDBContext = sampleDBContext;
            _userData = userData;
            _mappper = mapper;
        }

        //[Authorize(Roles = "Admin")]
        [Authorize()]
        [MyAuthorizationFilter]
        [HttpGet("GetUsers")]
        public List<UserDTO> GetUsers()
        {
            var data = _userData.GetUsers();
            var mappedData = _mappper.Map<List<UserDTO>>(data);
            return mappedData;
        }

        [HttpGet("getPrime")]
        public string Prime(int num)
        {
            var data = _userData.prime(num);
            return data;

        }

        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int ID)
        {
            var data = _userData.GetUserById(ID);
            if(data !=null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest("No data found");
            }
        } 
    }
}
