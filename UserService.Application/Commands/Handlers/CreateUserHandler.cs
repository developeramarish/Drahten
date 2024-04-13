﻿using UserService.Application.Services.ReadServices;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class CreateUserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly IUserReadService _userReadService;

        public CreateUserHandler(IUserRepository userRepository, IUserFactory userFactory, IUserReadService userReadService)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
            _userReadService = userReadService;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var alreadyExists = await _userReadService.ExistsByIdAsync(command.UserId);

            if (alreadyExists)
            {
                //TODO: Throw exception
            }

            var user = _userFactory.Create(command.UserId, command.UserFullName, command.UserNickName, command.UserEmailAddress);

            await _userRepository.AddUserAsync(user);
        }
    }
}
