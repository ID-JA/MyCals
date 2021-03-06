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
    [Route("api/manager")]
    [Authorize(Roles = "Manager")]
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

        [HttpGet("users/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _repoUserAccount.GetUsersAccounts();
            var usersForDisplayDTO = new List<UserForDisplayDTO>();
            foreach (var user in users)
            {
                var roleOfUser = await _userManager.GetRolesAsync(user);
                var userForDisplayDTO = _mapper.Map<UserForDisplayDTO>(user);
                userForDisplayDTO.Role = roleOfUser[0];
                if (userForDisplayDTO.Role == "User")
                {
                    usersForDisplayDTO.Add(userForDisplayDTO);
                }
            }
            return Ok(usersForDisplayDTO);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repoUserAccount.GetUserById(id);
            var userForDisplayDTO = _mapper.Map<UserForDisplayDTO>(user);
            return Ok(userForDisplayDTO);
        }

        [HttpDelete("users/delete/{id}")]
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
