using CheckInSKP.Application.Common.Interfaces;
using CheckInSKP.Domain.Enums;
using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Application.User.Commands.CreateUser
{
    public record CreateUserWithStaffCommand : IRequest<int>
    {
        // User details
        public required string Name { get; init; }
        public required string Username { get; init; }
        public required string Password { get; init; }

        // Staff details
        public required string PhoneNumber { get; init; }
        public required string CardNumber { get; init; }
        public required bool PhoneNotification { get; init; }
    }
    public class CreateUserWithStaffCommandHandler : IRequestHandler<CreateUserWithStaffCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserFactory _userFactory;
        private readonly StaffFactory _staffFactory;
        public CreateUserWithStaffCommandHandler(IUserRepository userRepository, IStaffRepository staffRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, UserFactory userFactory, StaffFactory staffFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _staffRepository = staffRepository ?? throw new ArgumentNullException(nameof(staffRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _staffFactory = staffFactory ?? throw new ArgumentNullException(nameof(staffFactory));
        }
        public async Task<int> Handle(CreateUserWithStaffCommand request, CancellationToken cancellationToken)
        {
            int? userId = 0;

            try
            {
                // Checks if user already exists
                if (await _userRepository.GetByUsernameAsync(request.Username) != null)
                {
                    throw new Exception($"User with username {request.Username} already exists");
                }

                // Checks if role exists
                Domain.Entities.Role role = await _roleRepository.GetByIdAsync((int)RoleEnum.Staff) ?? throw new Exception($"Role not found");

                if (role.Name != "Staff")
                {
                    throw new Exception($"Role with ID 3 is not the default role");
                }

                // Hashing password
                string passwordHash = _passwordHasher.HashPassword(request.Password);

                // Create User
                var user = _userFactory.CreateNewUser(request.Name, request.Username, passwordHash, role.Id);
                user = await _userRepository.AddAsync(user) ?? throw new InvalidOperationException("Could not add user");

                // Complete the UnitOfWork to generate the ID
                await _unitOfWork.CompleteAsync(cancellationToken);

                var addedUser = await _userRepository.GetByUsernameAsync(request.Username) ?? throw new Exception($"User with username {request.Username} not found");
                userId = addedUser.Id;

                // Create Staff
                var staff = _staffFactory.CreateNewStaff((int)userId, request.PhoneNumber, request.CardNumber, request.PhoneNotification);
                staff = await _staffRepository.AddAsync(staff) ?? throw new InvalidOperationException("Could not add staff");

                // Complete the UnitOfWork to generate the ID
                await _unitOfWork.CompleteAsync(cancellationToken);

                return userId ?? throw new Exception($"Weird error");
            }
            catch (Exception)
            {
                if(userId !> 0)
                {
                    await _userRepository.RemoveAsync((int)userId);
                }

                throw;
            }
        }
    }
}
