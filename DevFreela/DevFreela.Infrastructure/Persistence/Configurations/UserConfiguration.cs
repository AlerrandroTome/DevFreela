﻿using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(u => u.Skills)
                   .WithOne()
                   .HasForeignKey(fk => fk.IdSkill)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Role)
                   .HasConversion(v => v.ToLower(), 
                                    v => v);
        }
    }
}
