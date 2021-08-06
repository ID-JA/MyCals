using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto;
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
        private readonly RoleManager<Role> _roleManager;
        private int userId;


        public AdminController(IRepoUserAccount repoUserAccount, IMapper mapper, UserManager<UserAccount> userManager, RoleManager<Role> roleManager, IHttpContextAccessor httpContextAccessor, IRepoMeals repoMeals)
        {
            _repoUserAccount = repoUserAccount;
            _repoMeals = repoMeals;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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

        [HttpPost("addmanager")]
        public async Task<IActionResult> AddManger(UserRegisterDTO userRegisterDTO)
        {
            var checkedEmail = await _userManager.FindByEmailAsync(userRegisterDTO.Email);

            var user = _mapper.Map<UserAccount>(userRegisterDTO);

            if (checkedEmail != null)
            {
                return BadRequest("This email already exist try again");
            }
            else
            {
                user.UserName = userRegisterDTO.Email.Split("@")[0] + userRegisterDTO.Email.Split("@")[1];
            }

            if (await _roleManager.RoleExistsAsync("Admin") == false && await _roleManager.RoleExistsAsync("Manager") == false && await _roleManager.RoleExistsAsync("User") == false)
            {
                var roles = new List<Role>{
              new Role{Name="Admin"},
              new Role{Name="Manager"},
              new Role{Name="User"},

            };

                foreach (var role in roles)
                {
                    await _roleManager.CreateAsync(role);
                }
            }

            var Result = await _userManager.CreateAsync(user, userRegisterDTO.Password);

            if (Result.Succeeded)
            {
                //var confirmaEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.AddToRoleAsync(user, userRegisterDTO.Role);
                return Ok("User Created Successfully");
            }
            return BadRequest(Result.Errors);
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
                    var meals = _repoMeals.GetMealsOfUser(id);

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

        [HttpGet("users/{id}/meals/{id_meal}")]
        public async Task<IActionResult> GetUserMeal(int id, int id_meal)
        {
            var user = await _repoUserAccount.GetUserById(id);
            if (user != null)
            {
                if (user.Id != userId)
                {
                    var mealOfUser = _repoMeals.GetMealOfUser(id, id_meal);
                    if (mealOfUser != null && mealOfUser.Id_User == user.Id)
                    {
                        var mealOfUserDTO = _mapper.Map<MealForDisplayDTO>(mealOfUser);
                        return Ok(mealOfUserDTO);
                    }
                    return NotFound("This meal dosen't existe 💢!!!");
                }
                return BadRequest("Bad Action 😑 !!!");
            }
            return NotFound("This user dosen't existe 💢 ");
        }

        [HttpDelete("users/{id}/meals/{id_meal}/delete")]
        public async Task<IActionResult> DeleteUserMeal(int id, int id_meal)
        {
            var user = await _repoUserAccount.GetUserById(id);
            if (user != null)
            {
                if (user.Id != userId)
                {
                    await _repoMeals.DeleteMeal(id_meal);
                    var result = await _repoMeals.SaveMeal();
                    if (result)
                    {
                        return Ok("This Meal has been deleted successfully 👍 !!!");
                    }
                    return NotFound("This meal dosen't existe 💢!!!");
                }
                return BadRequest("Bad Action 😑 !!!");
            }
            return NotFound("This user dosen't existe 💢 ");
        }

        [HttpPut("users/{id}/meals/{id_meal}/edit")]
        public async Task<IActionResult> EditUserMeal(int id, int id_meal, MealForEditDTO mealForEditDTO)
        {
            var user = await _repoUserAccount.GetUserById(id);
            if (user != null)
            {
                if (user.Id != userId)
                {
                    await _repoMeals.EditMeal(id_meal, mealForEditDTO);
                    var result = await _repoMeals.SaveMeal();
                    return result ? Ok("This Meal has been  updated successfully 👍 !!!") : (IActionResult)NotFound("This meal dosen't existe 💢!!!");
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
