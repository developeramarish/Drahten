﻿using Microsoft.AspNetCore.Mvc;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Dispatcher;
using PrivateHistoryService.Application.Queries;
using PrivateHistoryService.Application.Queries.Dispatcher;
using PrivateHistoryService.Presentation.Dtos;

namespace PrivateHistoryService.Presentation.Controllers
{
    [ApiController]
    [Route("privatehistory-service/users")]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ResponseDto _responseDto;

        public UserController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _responseDto = new ResponseDto();
        }

        [HttpGet("{UserId:guid}/commented-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetCommentedArticles([FromRoute] GetCommentedArticlesQuery getCommentedArticlesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getCommentedArticlesQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        //TODO: Change the name of the query. The query name should be GetLikedArticlesQuery, instead of GetArticleLikesQuery.
        //Reason: The current name is NOT appropriate because it suggests that the query will return likes for article,
        //but the query returns articles that are liked by user with the specified UserId.
        [HttpGet("{UserId:guid}/liked-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetLikedArticles([FromRoute] GetArticleLikesQuery getArticleLikesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleLikesQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        //TODO: Change the name of the query. The query name should be GetDislikedArticlesQuery, instead of GetArticleDislikesQuery.
        //Reason: For similar reasons as the GetArticleLikesQuery from the action method GetLikedArticles.
        [HttpGet("{UserId:guid}/disliked-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetDislikedArticles([FromRoute] GetArticleDislikesQuery getArticleDislikesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleDislikesQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        //TODO: Change the name of the query. The query name should be GetLikedArticleCommentsQuery, instead of GetArticleCommentLikesQuery.
        //Reason: For similar reasons as the GetArticleLikesQuery from the action method GetLikedArticles.
        [HttpGet("{UserId:guid}/liked-article-comments/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetLikedArticleComments([FromRoute] GetArticleCommentLikesQuery getArticleCommentLikesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleCommentLikesQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        //TODO: Change the name of the query. The query name should be GetDislikedArticleCommentsQuery, instead of GetArticleCommentDislikesQuery.
        //Reason: For similar reasons as the GetArticleLikesQuery from the action method GetLikedArticles.
        [HttpGet("{UserId:guid}/disliked-article-comments/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetDislikedArticleComments([FromRoute] GetArticleCommentDislikesQuery getArticleCommentDislikesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getArticleCommentDislikesQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        //TODO: Change the name of the query. The query name should be GetSearchedArticlesQuery, instead of GetSearchedArticlesDataQuery.
        [HttpGet("{UserId:guid}/searched-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetSearchedArticles([FromRoute] GetSearchedArticlesDataQuery getSearchedArticlesDataQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getSearchedArticlesDataQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/searched-topics/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetSearchedTopics([FromRoute] GetSearchedTopicsDataQuery getSearchedTopicsDataQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getSearchedTopicsDataQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/topic-subscriptions/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetTopicSubscriptions([FromRoute] GetTopicSubscriptionsQuery getTopicSubscriptionsQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getTopicSubscriptionsQuery);
            
            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/viewed-articles/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetViewedArticles([FromRoute] GetViewedArticlesQuery getViewedArticlesQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(getViewedArticlesQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpGet("{UserId:guid}/viewed-users/")]
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        public async Task<ActionResult> GetViewedUsers([FromRoute] GeViewedUsersQuery geViewedUsersQuery)
        {
            var result = await _queryDispatcher.DispatchAsync(geViewedUsersQuery);

            _responseDto.Result = result;

            if (result == null)
            {
                return NotFound(_responseDto);
            }

            _responseDto.IsSuccess = true;

            return Ok(_responseDto);
        }

        [HttpPost("{UserId:guid}/commented-article/{ArticleId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddCommentedArticle([FromBody] AddCommentedArticleCommand addCommentedArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(addCommentedArticleCommand);

            return Created(HttpContext.Request.Path, null);
        }

        [HttpPost("{UserId:guid}/liked-article/{ArticleId:guid}")]
        [ProducesResponseType(typeof(ResponseDto), 201)]
        public async Task<ActionResult> AddLikedArticle([FromBody] AddLikedArticleCommand addLikedArticleCommand)
        {
            await _commandDispatcher.DispatchAsync(addLikedArticleCommand);

            return Created(HttpContext.Request.Path, null);
        }
    }
}
