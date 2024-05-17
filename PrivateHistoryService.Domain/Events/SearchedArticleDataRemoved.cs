﻿using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Domain.Events
{
    public record SearchedArticleDataRemoved(User User, SearchedTopicData SearchedTopicData) : IDomainEvent;
}
