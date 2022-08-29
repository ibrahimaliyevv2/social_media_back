using System;
using ChaosBackend.Data.Configurations;
using ChaosBackend.Data.Entities;
using ChaosBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChaosBackend.DAL
{
	public class AppDbContext:IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<PostComment> Comments { get; set; }
		public DbSet<PostImage> PostImages { get; set; }
		public DbSet<UserImage> UserImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfiguration(new PostConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

