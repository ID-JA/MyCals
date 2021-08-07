using MY_CALS_BACKEND.Dto.Review;
using MY_CALS_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Repositories
{
    public interface IRepoReviews
    {

        public List<Review> GetReviews();
        public void AddReview<T>(T entity) where T : class;
        public Task<bool> Save();

    }


    public class RepoReviews : IRepoReviews
    {
        private readonly ApplicationDbContext _dbContext;
        public RepoReviews(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void AddReview<T>(T entity) where T : class
        {
            await _dbContext.AddAsync(entity);
        }

        public List<Review> GetReviews()
        {
           return _dbContext.Reviews.Where(r=> r.Nbr_Stars >= 3).ToList();
        }
        /// <summary>
        /// Save Data 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Save()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }


}
