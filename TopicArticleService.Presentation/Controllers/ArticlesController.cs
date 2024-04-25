﻿using Microsoft.AspNetCore.Mvc;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Dispatcher;
using TopicArticleService.Application.Queries;
using TopicArticleService.Application.Queries.Dispatcher;
using TopicArticleService.Presentation.Dtos;

namespace TopicArticleService.Presentation.Controllers
{
    [ApiController]
    [Route("topic-article-service/articles")]
    public class ArticlesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ResponseDto _responseDto;

        public ArticlesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _responseDto = new ResponseDto();
        }

        [HttpGet("{ArticleId:guid}", Name = "GetArticle")]
        public async Task<ActionResult> GetArticle([FromRoute] GetArticleQuery getArticleQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{ArticleId:guid}/likes/")]
        public async Task<ActionResult> GetArticleLikes([FromRoute] GetArticleLikesQuery getArticleLikesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleLikesQuery);

            _responseDto.Result = result;

            if (result.Count == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{ArticleId:guid}/dislikes/")]
        public async Task<ActionResult> GetArticleDislikes([FromRoute] GetArticleDislikesQuery getArticleDislikesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleDislikesQuery);

            _responseDto.Result = result;

            if (result.Count == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess= true;

            return Ok(_responseDto);
        }

        [HttpGet("{ArticleId:guid}/comments/", Name = "GetArticleComments")]
        public async Task<ActionResult> GetArticleComments([FromRoute] GetArticleCommentsQuery getArticleCommentsQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleCommentsQuery);

            _responseDto.Result = result;

            if (result.Count == 0)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterArticle([FromBody] CreateArticleCommand createArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(createArticleCommand);

            return CreatedAtAction(actionName: nameof(GetArticle), routeValues: new { ArticleId = createArticleCommand .ArticleId}, null);
        }

        [HttpPost("{ArticleId:guid}/likes/")]
        public async Task<ActionResult> RegisterArticleLike([FromBody] AddArticleLikeCommand addArticleLikeCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleLikeCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{ArticleId:guid}/dislikes/")]
        public async Task<ActionResult> RegisterArticleDislike([FromBody] AddArticleDislikeCommand addArticleDislikeCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleDislikeCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{ArticleId:guid}/comments/")]
        public async Task<ActionResult> RegisterArticleComment([FromBody] AddArticleCommentCommand addArticleCommentCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleCommentCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("comments/{ArticleCommentId:guid}/likes")]
        public async Task<ActionResult> RegisterArticleCommentLike([FromBody] AddArticleCommentLikeCommand addArticleCommentLikeCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleCommentLikeCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("comments/{ArticleCommentId:guid}/dislikes")]
        public async Task<ActionResult> RegisterArticleCommentDislike([FromBody] AddArticleCommentDislikeCommand addArticleCommentDislikeCommand)
        {
            await _commandDispatcher.DispatchAsync(addArticleCommentDislikeCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpDelete("{ArticleId:guid}/comments/{ArticleCommentId:guid}")]
        public async Task<ActionResult> RemoveArticleComment([FromRoute] RemoveArticleCommentCommand removeArticleCommentCommand)
        {
            await _commandDispatcher.DispatchAsync(removeArticleCommentCommand);

            return NoContent();
        }
    }
}
