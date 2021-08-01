using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto.UserAccount;
using MY_CALS_BACKEND.Models;
using MY_CALS_BACKEND.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "Manager,Admin")]
    [ApiController]
    public class UsersAccountsController : ControllerBase
    {
        private readonly IRepoUserAccount _repoUserAccount;
        private readonly IMapper _mapper;
        private readonly UserManager<UserAccount> _userManager;
        private int userId;

        //private readonly RoleManager<Role> _roleManager;
        public UsersAccountsController(IRepoUserAccount repoUserAccount, IMapper mapper, UserManager<UserAccount> userManager
       /*, RoleManager<Role> roleManager*/, IHttpContextAccessor httpContextAccessor)
        {
            _repoUserAccount = repoUserAccount;
            _mapper = mapper;
            _userManager = userManager;
            //_roleManager = roleManager;
            userId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _repoUserAccount.GetUsersAccounts();
            var usersForDisplayDTO = new List<UserForDisplayDTO>();
            foreach (var user in users)
            {
                usersForDisplayDTO.Add(_mapper.Map<UserForDisplayDTO>(user));
            }
            return Ok(usersForDisplayDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repoUserAccount.GetUserById(id);
            var userForDisplayDTO = _mapper.Map<UserForDisplayDTO>(user);
            return Ok(userForDisplayDTO);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _repoUserAccount.GetUserById(id);

            if (user != null)
            {
                if (user.Id != userId)
                {
                    await _repoUserAccount.DeleteUserAccount(id);
                    var result = await _repoUserAccount.Save();

                    if (result)
                    {
                        return Ok("This user has been delete successfully 🙌");
                    }
                    else
                    {
                        return NotFound("Something wrong happened 😵 !!!");
                    }
                }

                return BadRequest("Bad Action 😑 !!!");
            }
            return NotFound("This user doesn't existe 💢!!!");
        }
    }
}
