﻿
namespace TopicArticleService.Application.Dtos
{
    public class ArticleCommentDislikeDto
    {
        public Guid ArticleCommentId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
