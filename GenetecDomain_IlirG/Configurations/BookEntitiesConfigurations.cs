using GenetecDomain_IlirG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenetecDomain_IlirG.Configurations
{
      public class BookEntitiesConfigurations : IEntityTypeConfiguration<BookEntities>
    {
        public void Configure(EntityTypeBuilder<BookEntities> builder)
        {
            builder.ToTable("BookEntities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.PublishDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(x => x.Authors);

            builder.HasIndex(x => x.Id);
          
        }
    }
}
