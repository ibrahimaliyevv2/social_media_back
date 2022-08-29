using System;
using ChaosBackend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChaosBackend.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(25).IsRequired(true);
            builder.Property(x => x.PublisheTime).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

