﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto.Meals;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IRepoUserAccount _repoUserAccount;
        private readonly IRepoMeals _repoMeals;
        private readonly IMapper _mapper;
        private readonly UserManager<UserAccount> _userManager;
        private int userId;


        public AdminController(IRepoUserAccount repoUserAccount, IMapper mapper, UserManager<UserAccount> userManager, IHttpContextAccessor httpContextAccessor, IRepoMeals repoMeals)
        {
            _repoUserAccount = repoUserAccount;
            _repoMeals = repoMeals;
            _mapper = mapper;
            _userManager = userManager;
            userId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }


        [HttpGet("users/all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _repoUserAccount.GetUsersAccounts();
            var usersForDisplayDTO = new List<UserForDisplayDTO>();
            foreach (var user in users)
            {
                var roleOfUser = await _userManager.GetRolesAsync(user);
                var userForDisplayDTO = _mapper.Map<UserForDisplayDTO>(user);
                userForDisplayDTO.Role = roleOfUser[0];

                if (userForDisplayDTO.Role != "Admin")
                {
                    usersForDisplayDTO.Add(userForDisplayDTO);
                }
            }
            return Ok(usersForDisplayDTO);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var user = await _repoUserAccount.GetUserById(id);
            if (user != null)
            {
                if (user.Id != userId)
                {
                    var roleOfUser = await _userManager.GetRolesAsync(user);
                    var userForDisplayDTO = _mapper.Map<UserForDisplayDTO>(user);
                    userForDisplayDTO.Role = roleOfUser[0];
                    return Ok(userForDisplayDTO);
                }
                return BadRequest("Bad Action 😑 !!!");
            }
            return NotFound("This user dosen't existe 💢 ");
        }


        [HttpGet("users/{id}/meals")]
        public async Task<IActionResult> GetUserMeals(int id)
        {
            var user = await _repoUserAccount.GetUserById(id);
            if (user != null)
            {
                if (user.Id != userId)
                {
                    var mealsOfUser = new List<MealForDisplayDTO>();
                    var meals =  _repoMeals.GetMealsOfUser(id);

                    foreach (var meal in meals)
                    {
                        mealsOfUser.Add(_mapper.Map<MealForDisplayDTO>(meal));
                    }
                    return Ok(mealsOfUser);
                }
                return BadRequest("Bad Action 😑 !!!");
            }
            return NotFound("This user dosen't existe 💢 ");
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
