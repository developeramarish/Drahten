﻿
namespace PrivateHistoryService.Application.Commands
{
    public record RemoveViewedArticleCommand(Guid ArticleId, Guid UserId, DateTimeOffset DateTime) : ICommand;
}
