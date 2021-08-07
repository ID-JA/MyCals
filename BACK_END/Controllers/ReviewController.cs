using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MY_CALS_BACKEND.Dto.Review;
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
    public class ReviewController : ControllerBase
    {
        private readonly IRepoReviews _repoReviews;
        private readonly IMapper _mapper;
        public ReviewController(IRepoReviews repoReviews, IMapper mapper)
        {
            _repoReviews = repoReviews;
            _mapper = mapper;
        }

        [HttpPost("add")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReview([FromBody] ReviewForAddDTO reviewForAddDTO)
        {
            if (ModelState.IsValid)
            {

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
    }
}
