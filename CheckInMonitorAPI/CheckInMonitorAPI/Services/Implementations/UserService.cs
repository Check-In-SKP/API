﻿using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Interfaces;

namespace CheckInMonitorAPI.Services.Implementations
{
    public class UserService : GenericService<User, int>, IUserService
    {
        private readonly IGenericRepository<User, int> _repository;

        public UserService(IUnitOfWork unitOfWork, ILogger<GenericService<User, int>> logger) : base(unitOfWork, logger)
        {
            _repository = unitOfWork.GetRepository<User, int>();
        }

        // Other implementations here
    }
}