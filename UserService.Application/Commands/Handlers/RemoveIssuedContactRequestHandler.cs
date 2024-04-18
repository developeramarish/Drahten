﻿using UserService.Application.Exceptions;
using UserService.Application.Services.ReadServices;
using UserService.Domain.Repositories;

namespace UserService.Application.Commands.Handlers
{
    internal sealed class RemoveIssuedContactRequestHandler : ICommandHandler<RemoveIssuedContactRequestCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;

        public RemoveIssuedContactRequestHandler(IUserRepository userRepository, IUserReadService userReadService)
        {
            _userRepository = userRepository;
            _userReadService = userReadService;
        }
        
        public async Task HandleAsync(RemoveIssuedContactRequestCommand command)
        {
            var issuer = await _userRepository.GetUserByIdAsync(command.IssuerUserId);

            if (issuer == null)
            {
                throw new UserNotFoundException(command.IssuerUserId);
            }

            var receiverExists = await _userReadService.ExistsByIdAsync(command.ReceiverUserId);

            if (receiverExists == false)
            {
                throw new UserNotFoundException(command.ReceiverUserId);
            }

            issuer.RemoveIssuedContactRequest(command.ReceiverUserId);

            await _userRepository.UpdateUserAsync(issuer);
        }
    }
}
