﻿
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveSearchedArticleDataCommand(Guid UserId, Guid SearchedArticleDataId) : ICommand;
}
