﻿using Microsoft.EntityFrameworkCore;
using ThwartAPI.Domain.Aggregates.UserAggregate;
using ThwartAPI.Domain.Interfaces.Repositories;
using ThwartAPI.Infrastructure.Data;

namespace ThwartAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
               _context = context;
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
