namespace AuthDynamic.Controllers
{
    using AutoMapper;
    using DataAccessLayer.DTO;
    using DataAccessLayer.Model;
    using DataAccessLayer.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        [Authorize(Roles = "ClientAdmin,Admin")]
        [HttpGet("getusers")]
        public List<UserDTO> GetUsers()
        {
            var data = _userData.GetUsers();
            var mappedData = _mappper.Map<List<UserDTO>>(data);
            return mappedData;
        }
    }
}
