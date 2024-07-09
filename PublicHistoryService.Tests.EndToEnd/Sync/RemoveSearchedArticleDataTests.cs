﻿using Newtonsoft.Json;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Presentation.Dtos;
using PublicHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PublicHistoryService.Tests.EndToEnd.Sync
{
    public class RemoveSearchedArticleDataTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddSearchedArticleDataCommand> PrepareAddSearchedArticleDataCommandAsync()
        {
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            await Post(command, $"/publichistory-service/users/{userId}");

            var addSearchedArticleDataCommand = new AddSearchedArticleDataCommand(ArticleId: Guid.NewGuid(), UserId: userId,
                SearchedData: "...", DateTime: DateTimeOffset.Now);

            return addSearchedArticleDataCommand;
        }

        private async Task<SearchedArticleDataDto> GetSearchedArticleDataDtoAsync(Guid userId)
        {
            var searchedArticlesResponse = await Get($"/publichistory-service/users/{userId}/searched-articles/");

            var responseSerializedContent = await searchedArticlesResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            var responseResult = JsonConvert.DeserializeObject<List<SearchedArticleDataDto>>(Convert.ToString(responseDto.Result));

            return responseResult.FirstOrDefault();
        }

        private async Task<RemoveSearchedArticleDataCommand> PrepareRemoveSearchedArticleDataCommandAsync()
        {
            var command = await PrepareAddSearchedArticleDataCommandAsync();

            await Post(command, $"/publichistory-service/users/{command.UserId}/searched-articles/{command.ArticleId}");

            var searchedArticleDataDto = await GetSearchedArticleDataDtoAsync(command.UserId);

            var removeSearchedArticleDataCommand = new RemoveSearchedArticleDataCommand(UserId: command.UserId,
                SearchedArticleDataId: searchedArticleDataDto.SearchedArticleDataId);

            return removeSearchedArticleDataCommand;
        }

        public RemoveSearchedArticleDataTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if SearchedArticleData value object that has values like the ones that are
        //specified in RemoveSearchedArticleDataCommand is deleted from the database.
        [Fact]
        public async Task Remove_Searched_ArticleData_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var command = await PrepareRemoveSearchedArticleDataCommandAsync();

            //ACT
            var response = await Delete($"/publichistory-service/users/{command.UserId}/searched-articles/{command.SearchedArticleDataId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted SearchedArticleData value object is not found in the database.
        [Fact]
        public async Task Remove_Searched_ArticleData_Endpoint_Should_Remove_SearchedArticleData_From_The_Database()
        {
            //ARRANGE
            var command = await PrepareRemoveSearchedArticleDataCommandAsync();

            await Delete($"/publichistory-service/users/{command.UserId}/searched-articles/{command.SearchedArticleDataId}");

            //ACT
            var response = await Get($"/publichistory-service/users/{command.UserId}/searched-articles/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
