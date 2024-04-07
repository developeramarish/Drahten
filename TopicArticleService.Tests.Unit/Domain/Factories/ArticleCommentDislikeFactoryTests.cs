﻿using NSubstitute;
using Shouldly;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Factories
{
    public class ArticleCommentDislikeFactoryTests
    {
        #region GLOBAL ARRANGE

        private ArticleCommentID _articleCommentId;
        private UserID _userId;
        private string _dateTime;
        private readonly IArticleCommentDislikeFactory _articleCommentDislikeConcreteFactory;
        private readonly IArticleCommentDislikeFactory _articleCommentDislikeMockFactory;

        public ArticleCommentDislikeFactoryTests()
        {
            _articleCommentId = new ArticleCommentID(Guid.NewGuid());
            _userId = new UserID(Guid.NewGuid());
            _dateTime = DateTime.Now.ToString();
            
            _articleCommentDislikeConcreteFactory = new ArticleCommentDislikeFactory();
            _articleCommentDislikeMockFactory = Substitute.For<IArticleCommentDislikeFactory>();
        }

        #endregion

        //Should create ArticleCommentDislike instance from concrete factory implementation.
        //The created ArticleCommentDislike instance should have the same values as the values provided to the concrete factory. 
        [Fact]
        public void Given_Valid_ArticleCommentDislike_Parameters_Should_Create_ArticleCommentDislike_Instance_From_Concrete_Factory()
        {
            //ACT
            var articleCommentDislike = _articleCommentDislikeConcreteFactory.Create(_articleCommentId, _userId, DateTime.Now.ToString());

            //ASSERT
            articleCommentDislike.ShouldNotBeNull();

            articleCommentDislike.ArticleCommentID.ShouldBeSameAs(_articleCommentId);

            articleCommentDislike.UserID.ShouldBeSameAs(_userId);
        }

        //Should create two ArticleCommentDislike instances with equal values when equal values are given to the concrete factory
        //(_articleCommentDislikeConcreteFactory) and to the mock factory (_articleCommentDislikeMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent ArticleCommentDislike instances when provided with the same
        //input parameters, thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that ArticleCommentDislike instance should be created. 
        //This is needed becouse the concrete factory may not create ArticleCommentDislike instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_ArticleCommentDislike_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var articleCommentDislike = _articleCommentDislikeConcreteFactory.Create(_articleCommentId, _userId, DateTime.Now.ToString());

            _articleCommentDislikeMockFactory.Create(_articleCommentId, _userId, DateTime.Now.ToString()).Returns(
                callInfo =>
                {
                    var articleCommentDislike = new ArticleCommentDislike(_articleCommentId, _userId, DateTime.Now.ToString());

                    return articleCommentDislike;
                });

            var articleCommentDislikeFromMockFactory = _articleCommentDislikeMockFactory
                .Create(_articleCommentId, _userId, DateTime.Now.ToString());

            //ASSERT
            articleCommentDislikeFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the articleCommentDislike object that is created by _articleCommentDislikeConcreteFactory
            //with the values of the articleCommentDislikeFromMockFactory object that is created by _articleCommentDislikeMockFactory. 

            articleCommentDislike.ArticleCommentID.ShouldBe(articleCommentDislikeFromMockFactory.ArticleCommentID);

            articleCommentDislike.UserID.ShouldBe(articleCommentDislikeFromMockFactory.UserID);

            articleCommentDislike.DateTime.ShouldBe(articleCommentDislikeFromMockFactory.DateTime);
        }
    }
}
