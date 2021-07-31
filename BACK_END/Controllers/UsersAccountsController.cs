using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto.UserAccount;
using MY_CALS_BACKEND.Models;
using MY_CALS_BACKEND.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAccountsController : ControllerBase
    {
        private readonly IRepoUserAccount _repoUserAccount;
        private readonly IMapper _mapper;
        public UsersAccountsController(IRepoUserAccount repoUserAccount, IMapper mapper)
        {
            _repoUserAccount = repoUserAccount;
            _mapper = mapper;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _repoUserAccount.GetUsersAccounts();
            var userForDisplayDTO = new List<UserForDisplayDTO>();
            foreach (var user in users)
            {
                userForDisplayDTO.Add(_mapper.Map<UserForDisplayDTO>(user));
            }
            return Ok(userForDisplayDTO);
        }


    }
}
