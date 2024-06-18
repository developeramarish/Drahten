﻿using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Dtos.PrivateHistoryService;
using TopicArticleService.Infrastructure.Exceptions;

namespace TopicArticleService.Infrastructure.AsyncDataServices
{
    internal sealed class MessageBusPublisher : IMessageBusPublisher
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly Random _random = new Random();

        public MessageBusPublisher(IConfiguration configuration)
        {
            _configuration = configuration;

            // Define the retry policy
            _retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(30),
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(2)
            },
            (exception, timeSpan, retryCount, context) =>
            {
                // TODO: Log the retry attempt
                Console.WriteLine($"TopicArticleService --> Retry {retryCount} encountered an error: {exception.Message}. " +
                             $">>> The message broker is down, and the message can't be published! <<< " +
                             $"Waiting {timeSpan} before next retry.");
            });

            // Initialize RabbitMQ connection with retry policy
            InitializeRabbitMqWithRetry();
        }

        private async void InitializeRabbitMqWithRetry()
        {
            try
            {
                await _retryPolicy.ExecuteAsync(() =>
                {
                    InitializeRabbitMq();
                    return Task.CompletedTask;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TopicArticleService --> Failed to initialize RabbitMQ connection after retries: {ex.Message}.");
            }
        }


        private void InitializeRabbitMq()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQHost"],
                    Port = int.Parse(_configuration["RabbitMQPort"])
                };

                _connection = factory.CreateConnection();

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "topic_article_service", type: ExchangeType.Direct);

                _connection.ConnectionShutdown += RabbitMqConnectionShutDown;

                Console.WriteLine("TopicArticleService --> RabbitMQ connection established.");
                // Log the initialization
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize RabbitMQ connection: {ex.Message}");

                // Rethrow the exception to trigger the retry policy.
                throw new RabbitMqInitializationException();
            }
        }

        private void RabbitMqConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            Console.WriteLine("TopicArticleService --> RabbitMQ connection shutdown.");
            //TODO: Log the message.
            InitializeRabbitMqWithRetry();
        }

        private async Task SendMessageAsync(MessageDescriptor messageDescriptor)
        {
            try
            {
                var messageBody = Encoding.UTF8.GetBytes(messageDescriptor.Message);

                await _retryPolicy.ExecuteAsync(async () =>
                {
                    // Ensure the connection is open before attempting to publish
                    if (!_connection.IsOpen)
                    {
                        InitializeRabbitMqWithRetry();
                    }

                    // *** IMPORTANT!!!
                    // The commented section is intented to simulate transient errors during message publishing when the connection is up.
                    // Expected behavior: Triggers the _retryPolicy defined in the constructor of this class.
                    // Note: Remove this in production.

                    // Simulate transient errors with a random error generator
                    //if (_random.Next(0, 2) == 0) // 50% chance to simulate an error
                    //{
                    //    throw new Exception("TopicArticleService --> Simulated transient error during message publishing.");
                    //}

                    _channel.BasicPublish(exchange: messageDescriptor.Exchange,
                                          routingKey: messageDescriptor.RoutingKey,
                                          body: messageBody);

                    Console.WriteLine($"TopicArticleService --> Message was send to RabbitMQ.");

                    await Task.CompletedTask;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message sending {ex}.");

                throw;
            }
        }

        public void PublishViewedArticle(ViewedArticleDto viewedArticleDto)
        {

        }

        public async Task PublishViewedArticleAsync(ViewedArticleDto viewedArticleDto)
        {
            try
            {
                var message = JsonSerializer.Serialize(viewedArticleDto);

                //TODO: Log a message.

                var messageDescriptor = new MessageDescriptor(message,
                    exchange: "topic_article_service", routingKey: "topic_article_service.viewed-article");

                await SendMessageAsync(messageDescriptor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception during message publishing {ex}.");
                throw;
            }
        }

        public void PublishLikedArticle(LikedArticleDto likedArticleDto)
        {
            var message = JsonSerializer.Serialize(likedArticleDto);

            //if (_connection.IsOpen)
            //{
            //    //TODO: Log a message.

            //    var messageDescriptor = new MessageDescriptor(message,
            //        exchange: "topic_article_service", routingKey: "topic_article_service.liked-article");

            //    SendMessage(messageDescriptor);
            //}
            //else
            //{
            //    //TODO: Retrying if the connection is not available.

            //    //TODO: Log the message.

            //    Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            //}
        }

        public void PublishDislikedArticle(DislikedArticleDto dislikedArticleDto)
        {
            var message = JsonSerializer.Serialize(dislikedArticleDto);

            //if (_connection.IsOpen)
            //{
            //    //TODO: Log a message.

            //    var messageDescriptor = new MessageDescriptor(message,
            //        exchange: "topic_article_service", routingKey: "topic_article_service.disliked-article");

            //    SendMessage(messageDescriptor);
            //}
            //else
            //{
            //    //TODO: Retrying if the connection is not available.

            //    //TODO: Log the message.

            //    Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            //}
        }

        public void PublishCommentedArticle(CommentedArticleDto commentedArticleDto)
        {
            var message = JsonSerializer.Serialize(commentedArticleDto);

            //if (_connection.IsOpen)
            //{
            //    //TODO: Log a message.

            //    var messageDescriptor = new MessageDescriptor(message,
            //        exchange: "topic_article_service", routingKey: "topic_article_service.commented-article");

            //    SendMessage(messageDescriptor);
            //}
            //else
            //{
            //    //TODO: Retrying if the connection is not available.

            //    //TODO: Log the message.

            //    Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            //}
        }

        public void PublishLikedArticleComment(LikedArticleCommentDto likedArticleCommentDto)
        {
            var message = JsonSerializer.Serialize(likedArticleCommentDto);

            //if (_connection.IsOpen)
            //{
            //    //TODO: Log a message.

            //    var messageDescriptor = new MessageDescriptor(message,
            //        exchange: "topic_article_service", routingKey: "topic_article_service.liked-article-comment");

            //    SendMessage(messageDescriptor);
            //}
            //else
            //{
            //    //TODO: Retrying if the connection is not available.

            //    //TODO: Log the message.

            //    Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            //}
        }

        public void PublishDislikedArticleComment(DislikedArticleCommentDto dislikedArticleCommentDto)
        {
            var message = JsonSerializer.Serialize(dislikedArticleCommentDto);

            //if (_connection.IsOpen)
            //{
            //    //TODO: Log a message.

            //    var messageDescriptor = new MessageDescriptor(message,
            //        exchange: "topic_article_service", routingKey: "topic_article_service.disliked-article-comment");

            //    SendMessage(messageDescriptor);
            //}
            //else
            //{
            //    //TODO: Retrying if the connection is not available.

            //    //TODO: Log the message.

            //    Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            //}
        }

        public void PublishTopicSubscription(TopicSubscriptionDto topicSubscriptionDto)
        {
            var message = JsonSerializer.Serialize(topicSubscriptionDto);

            //if (_connection.IsOpen)
            //{
            //    //TODO: Log a message.

            //    var messageDescriptor = new MessageDescriptor(message,
            //        exchange: "topic_article_service", routingKey: "topic_article_service.topic-subscription");

            //    SendMessage(messageDescriptor);
            //}
            //else
            //{
            //    //TODO: Retrying if the connection is not available.

            //    //TODO: Log the message.

            //    Console.WriteLine("--> TopicArticleService RabbitMQ connection is CLOSED!");
            //}
        }

        public void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
