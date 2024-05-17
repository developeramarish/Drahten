﻿using PrivateHistoryService.Domain.Events;
using PrivateHistoryService.Domain.Exceptions;
using PrivateHistoryService.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace PrivateHistoryService.Domain.Entities
{
    public class User : AggregateRoot<UserID>
    {
        private HashSet<ViewedArticle> _viewedArticles;
        private List<TopicSubscription> _subscribedTopics;
        private HashSet<SearchedArticleData> _searchedArticleInformation;
        private HashSet<SearchedTopicData> _searchedTopicInformation;
        private HashSet<CommentedArticle> _commentedArticles;
        private List<LikedArticle> _likedArticles;
        private List<DislikedArticle> _dislikedArticles;
        private List<LikedArticleComment> _likedArticleComments;
        private List<DislikedArticleComment> _dislikedArticleComments;
        private List<ViewedUser> _viewedUsers;

        public IReadOnlyCollection<ViewedArticle> ViewedArticles
        {
            get { return new ReadOnlyCollection<ViewedArticle>(_viewedArticles.ToList()); }
        }

        public IReadOnlyCollection<TopicSubscription> SubscribedTopics
        {
            get { return new ReadOnlyCollection<TopicSubscription>(_subscribedTopics); }
        }

        public IReadOnlyCollection<SearchedArticleData> SearchedArticleInformation
        {
            get { return new ReadOnlyCollection<SearchedArticleData>(_searchedArticleInformation.ToList()); }
        }

        public IReadOnlyCollection<SearchedTopicData> SearchedTopicInformation
        {
            get { return new ReadOnlyCollection<SearchedTopicData>(_searchedTopicInformation.ToList()); }
        }

        public IReadOnlyCollection<CommentedArticle> CommentedArticles
        {
            get { return new ReadOnlyCollection<CommentedArticle>(_commentedArticles.ToList()); }
        }

        public IReadOnlyCollection<LikedArticle> LikedArticles
        {
            get { return new ReadOnlyCollection<LikedArticle>(_likedArticles); }
        }

        public IReadOnlyCollection<DislikedArticle> DislikedArticles
        {
            get { return new ReadOnlyCollection<DislikedArticle>(_dislikedArticles); }
        }

        public IReadOnlyCollection<LikedArticleComment> LikedArticleComments
        {
            get { return new ReadOnlyCollection<LikedArticleComment>(_likedArticleComments); }
        }

        public IReadOnlyCollection<DislikedArticleComment> DislikedArticleComments
        {
            get { return new ReadOnlyCollection<DislikedArticleComment>(_dislikedArticleComments); }
        }

        public IReadOnlyCollection<ViewedUser> ViewedUsers
        {
            get { return new ReadOnlyCollection<ViewedUser>(_viewedUsers); }
        }

        private User()
        {
        }

        internal User(UserID userId)
        {
            ValidateConstructorParameters<NullUserParametersException>([userId]);

            Id = userId;

            _viewedArticles = new HashSet<ViewedArticle>();
            _subscribedTopics = new List<TopicSubscription>();
            _searchedArticleInformation = new HashSet<SearchedArticleData>();
            _searchedTopicInformation = new HashSet<SearchedTopicData>();
            _commentedArticles = new HashSet<CommentedArticle>();
            _likedArticles = new List<LikedArticle>();  
            _dislikedArticles = new List<DislikedArticle>();
            _likedArticleComments = new List<LikedArticleComment>();
            _dislikedArticleComments = new List<DislikedArticleComment>();
            _viewedUsers = new List<ViewedUser>();
        }

        public void AddViewedArticle(ViewedArticle viewedArticle)
        {
            var alreadyExists = _viewedArticles.Contains(viewedArticle);

            if (alreadyExists)
            {
                throw new ViewedArticleAlreadyExistsException(viewedArticle.ArticleID, viewedArticle.UserID, viewedArticle.DateTime);
            }

            _viewedArticles.Add(viewedArticle);

            AddEvent(new ViewedArticleAdded(this, viewedArticle));
        }

        public void AddTopicSubscription(TopicSubscription topicSubscription)
        {
            var alreadyExists = _subscribedTopics.Any(x => x.TopicID == topicSubscription.TopicID && x.UserID == topicSubscription.UserID);

            if (alreadyExists)
            {
                throw new SubscribedTopicAlreadyExistsException(topicSubscription.TopicID, topicSubscription.UserID);
            }

            _subscribedTopics.Add(topicSubscription);

            AddEvent(new TopicSubscriptionAdded(this, topicSubscription));
        }

        public void AddSearchedArticleData(SearchedArticleData searchedArticleData)
        {
            var alreadyExists = _searchedArticleInformation.Contains(searchedArticleData);

            if (alreadyExists) 
            {
                throw new SearchedArticleDataAlreadyExistsException(searchedArticleData.UserID, searchedArticleData.DateTime);
            }

            _searchedArticleInformation.Add(searchedArticleData);

            AddEvent(new SearchedArticleDataAdded(this, searchedArticleData));
        }

        public void AddSearchedTopicData(SearchedTopicData searchedTopicData)
        {
            var alreadyExists = _searchedTopicInformation.Contains(searchedTopicData);

            if (alreadyExists)
            {
                throw new SearchedTopicDataAlreadyExistsException(searchedTopicData.UserID, searchedTopicData.DateTime);
            }

            _searchedTopicInformation.Add(searchedTopicData);

            AddEvent(new SearchedTopicDataAdded(this, searchedTopicData));
        }

        public void AddCommentedArticle(CommentedArticle commentedArticle)
        {
            var alreadyExists = _commentedArticles.Contains(commentedArticle);

            if (alreadyExists)
            {
                throw new CommentedArticleAlreadyExistsException(commentedArticle.UserID, commentedArticle.DateTime);
            }

            _commentedArticles.Add(commentedArticle);

            AddEvent(new CommentedArticleAdded(this, commentedArticle));
        }

        public void AddLikedArticle(LikedArticle likedArticle)
        {
            var alreadyExists = _likedArticles.Any(x => x.ArticleID == likedArticle.ArticleID && x.UserID == likedArticle.UserID);

            if (alreadyExists)
            {
                throw new LikedArticleAlreadyExistsException(likedArticle.ArticleID, likedArticle.UserID);
            }

            _likedArticles.Add(likedArticle);

            AddEvent(new LikedArticleAdded(this, likedArticle));
        }
    }
}