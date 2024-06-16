﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PrivateHistoryService.Infrastructure.EntityFramework.Contexts;

#nullable disable

namespace PrivateHistoryService.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(ReadDbContext))]
    [Migration("20240616072105_Added SearchedDataAnswer property of type string in SearchedArticleDataReadModel")]
    partial class AddedSearchedDataAnswerpropertyoftypestringinSearchedArticleDataReadModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("private-history-service")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.CommentedArticleReadModel", b =>
                {
                    b.Property<Guid>("CommentedArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ArticleComment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CommentedArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentedArticle", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.DislikedArticleCommentReadModel", b =>
                {
                    b.Property<Guid>("ArticleCommentId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleCommentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("DislikedArticleComment", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.DislikedArticleReadModel", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("DislikedArticle", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.LikedArticleCommentReadModel", b =>
                {
                    b.Property<Guid>("ArticleCommentId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleCommentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("LikedArticleComment", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.LikedArticleReadModel", b =>
                {
                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("LikedArticle", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.SearchedArticleDataReadModel", b =>
                {
                    b.Property<Guid>("SearchedArticleDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SearchedData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SearchedDataAnswer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SearchedArticleDataId");

                    b.HasIndex("UserId");

                    b.ToTable("SearchedArticleData", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.SearchedTopicDataReadModel", b =>
                {
                    b.Property<Guid>("SearchedTopicDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SearchedData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SearchedTopicDataId");

                    b.HasIndex("UserId");

                    b.ToTable("SearchedTopicData", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.TopicSubscriptionReadModel", b =>
                {
                    b.Property<Guid>("TopicId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TopicId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TopicSubscription", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RetentionUntil")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.ToTable("User", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.ViewedArticleReadModel", b =>
                {
                    b.Property<Guid>("ViewedArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ArticleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ViewedArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ViewedArticle", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.ViewedUserReadModel", b =>
                {
                    b.Property<Guid>("ViewedUserReadModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ViewedUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ViewerUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ViewedUserReadModelId");

                    b.HasIndex("ViewerUserId");

                    b.ToTable("ViewedUser", "private-history-service");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.CommentedArticleReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("CommentedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_CommentedArticles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.DislikedArticleCommentReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("DislikedArticleComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_DislikedArticleComments");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.DislikedArticleReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("DislikedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_DislikedArticles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.LikedArticleCommentReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("LikedArticleComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_LikedArticleComments");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.LikedArticleReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("LikedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_LikedArticles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.SearchedArticleDataReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("SearchedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_SearchedArticles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.SearchedTopicDataReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("SearchedTopics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_SearchedTopics");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.TopicSubscriptionReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("TopicSubscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_TopicSubscriptions");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.ViewedArticleReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "User")
                        .WithMany("ViewedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_ViewedArticles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.ViewedUserReadModel", b =>
                {
                    b.HasOne("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", "ViewedUser")
                        .WithMany("ViewedUsers")
                        .HasForeignKey("ViewerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_User_ViewedUsers");

                    b.Navigation("ViewedUser");
                });

            modelBuilder.Entity("PrivateHistoryService.Infrastructure.EntityFramework.Models.UserReadModel", b =>
                {
                    b.Navigation("CommentedArticles");

                    b.Navigation("DislikedArticleComments");

                    b.Navigation("DislikedArticles");

                    b.Navigation("LikedArticleComments");

                    b.Navigation("LikedArticles");

                    b.Navigation("SearchedArticles");

                    b.Navigation("SearchedTopics");

                    b.Navigation("TopicSubscriptions");

                    b.Navigation("ViewedArticles");

                    b.Navigation("ViewedUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
