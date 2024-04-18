﻿
namespace UserService.Application.Commands
{
    public record BanUserCommand(Guid IssuerUserId, Guid ReceiverUserId, DateTimeOffset DateTime) : ICommand;
}
