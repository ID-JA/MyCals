using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto.Review;
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
    public class ReviewController : ControllerBase
    {
        private readonly UserManager<UserAccount> _userManager;
        private readonly IRepoReviews _repoReviews;
        private readonly IMapper _mapper;
        private readonly int userId;
        public ReviewController(IRepoReviews repoReviews, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<UserAccount> userManager)
        {
            _repoReviews = repoReviews;
            _userManager = userManager;
            _mapper = mapper;
            userId = Int32.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPost("add")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReview([FromBody] ReviewForAddDTO reviewForAddDTO)
        {
            if (ModelState.IsValid)
            {
                reviewForAddDTO.Id_User = userId;
                _repoReviews.AddReview(_mapper.Map<Review>(reviewForAddDTO));
                var result = await _repoReviews.Save();

                if (result)
                {
                    return Ok("Review has been added successfully 👍.");
                }

                return BadRequest("Somthing wrong happened 😵.");
            }

            return BadRequest("Something isn't valid pelase try again !!!");
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetReviews()
        {
            var allReviews = new List<ReviewForDisplayDTO>();

            foreach (var review in _repoReviews.GetReviews())
            {
                var reviewForDisplay = _mapper.Map<ReviewForDisplayDTO>(review);
                var user = await _userManager.FindByIdAsync(review.Id_User.ToString());
                reviewForDisplay.NameOfAuthor = string.Format("{0} {1}", user.FirstName, user.LastName);
                allReviews.Add(reviewForDisplay);
            }
            return Ok(allReviews);
        }
    }
}
