using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MY_CALS_BACKEND.Dto;
using MY_CALS_BACKEND.Dto.UserAccount;
using MY_CALS_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<UserAccount> _signInManager;
        private readonly IConfiguration _iConfiguration;


        public AuthController(UserManager<UserAccount> userManager,
        RoleManager<Role> roleManager,
        SignInManager<UserAccount> signInManager,
        IConfiguration iConfiguration, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _iConfiguration = iConfiguration;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDTO userForRegisterDto)
        {
            var checkedEmail = await _userManager.FindByEmailAsync(userForRegisterDto.Email);

            var user = _mapper.Map<UserAccount>(userForRegisterDto);


            if (checkedEmail != null)
            {
                return BadRequest("This email already exist try again");
            }
            else
            {
                user.UserName = userForRegisterDto.Email.Split("@")[0] + userForRegisterDto.Email.Split("@")[1];
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

            var Result = await _userManager.CreateAsync(user, userForRegisterDto.Password);

            if (Result.Succeeded)
            {
                //var confirmaEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.AddToRoleAsync(user, userForRegisterDto.Role);
                return Ok("User Created Successfully");
            }
            return BadRequest(Result.Errors);

        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user != null)
            {
                var result = _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, true);
                if (result.IsCompletedSuccessfully)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userDisplay = _mapper.Map<UserForDisplayDTO>(user);


                    userDisplay.Token = GenerateJwtToken(user, roles).Result;
                    userDisplay.Role = roles[0];
                    

                    return Ok(
                       new
                       {
                           userDisplay
                       }
                        );
                }
            }
            return BadRequest("Wrong Email or Password try again");

        }

        private async Task<string> GenerateJwtToken(UserAccount user, IList<string> roles)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_iConfiguration.GetSection("AppSettings:Token").Value));

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
