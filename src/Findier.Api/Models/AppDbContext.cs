﻿using System.Data.Entity;
using Findier.Api.Infrastructure;
using Findier.Api.Models.Identity;

namespace Findier.Api.Models
{
    public class AppDbContext : AppIdentityDbContext<User, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppDbContext() : base("AppDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<CommentVote> CommentVotes { get; set; }

        public DbSet<Finboard> Finboards { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostThread> PostThreads { get; set; }

        public DbSet<PostVote> PostVotes { get; set; }

        public DbSet<ThreadMessage> ThreadMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(p => p.SentMessages)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(p => p.StartedThreads)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(p => p.Finboards)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(p => p.Posts)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(p => p.Comments)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(p => p.PostVotes)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<User>()
                .HasMany(p => p.CommentVotes)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            builder.Entity<Finboard>()
                .HasMany(p => p.Posts)
                .WithRequired(p => p.Finboard);

            builder.Entity<Post>()
                .HasMany(p => p.Threads)
                .WithRequired(p => p.Post);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithRequired(p => p.Post);

            builder.Entity<Post>()
                .HasMany(p => p.Votes)
                .WithRequired(p => p.Post);

            builder.Entity<Comment>()
                .HasMany(p => p.Votes)
                .WithRequired(p => p.Comment);
        }
    }
}