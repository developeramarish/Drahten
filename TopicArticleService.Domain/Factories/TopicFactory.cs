﻿using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Factories
{
    public sealed class TopicFactory : ITopicFactory
    {
        public Topic Create(TopicId topicId, TopicName topicName, TopicId parentTopicId = null)
            => new Topic(topicId, topicName, parentTopicId);
    }
}
