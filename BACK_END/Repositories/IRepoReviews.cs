﻿using MY_CALS_BACKEND.Dto.Review;
using MY_CALS_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Repositories
{
    public interface IRepoReviews
    {
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

        public async Task<bool> Save()
        {
            return (await _dbContext.SaveChangesAsync() > 0);
        }
    }


}
