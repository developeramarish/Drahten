﻿using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class RemoveViewedUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<RemoveViewedUserCommand> _handler;

        private RemoveViewedUserCommand GetRemoveViewedUserCommand(Guid? viewerUserId = null, Guid? viewedUserId = null, DateTimeOffset? dateTime = null)
        {
            var ViewerUserId = viewerUserId ?? Guid.NewGuid();
            var ViewedUserId = viewedUserId ?? Guid.NewGuid();
            var DateTime = dateTime ?? DateTimeOffset.Now;

            var command = new RemoveViewedUserCommand(ViewerUserId: ViewerUserId, ViewedUserId: ViewedUserId, DateTime: DateTime);

            return command;
        }

        private ViewedUser GetViewedUser()
        {
            var viewedUser = new ViewedUser(viewerUserID: Guid.NewGuid(), viewedUserID: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return viewedUser;
        }

        public RemoveViewedUserHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RemoveViewedUserHandler(_userRepository);
        }

        #endregion

        private Task Act(RemoveViewedUserCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ViewerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ViewerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeViewedUserCommand = GetRemoveViewedUserCommand();

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the ViewedUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ViewedUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var removeViewedUserCommand = GetRemoveViewedUserCommand();

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewerUserId).Returns(user);

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewedUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove ViewedUser value object if the ViewerUserId and ViewedUserId from the RemoveViewedUserCommand are valid Ids for an existing Users.
        //The ViewedUser value object to be removed must have the same values as those in the RemoveViewedUserCommand.
        //Ensure the repository is called to update the User.
        [Fact]
        public async Task Given_Valid_ViewerUserId_And_ViewedUserId_Removes_ViewedUser_Instance_From_Viewer_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var viewerUser = _userConcreteFactory.Create(Guid.NewGuid());

            var viewedUser = _userConcreteFactory.Create(Guid.NewGuid());

            var viewedUserValueObject = GetViewedUser();

            var removeViewedUserCommand = GetRemoveViewedUserCommand(viewedUserValueObject.ViewerUserID, 
                viewedUserValueObject.ViewedUserID, viewedUserValueObject.DateTime);

            viewerUser.AddViewedUser(viewedUserValueObject);

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewerUserId).Returns(viewerUser);

            _userRepository.GetUserByIdAsync(removeViewedUserCommand.ViewedUserId).Returns(viewedUser);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeViewedUserCommand));

            //ASSERT
            exception.ShouldBeNull();
            
            await _userRepository.Received(1).UpdateUserAsync(viewerUser);
        }
    }
}
