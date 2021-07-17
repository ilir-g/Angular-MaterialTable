using GenetecDomain_IlirG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenetecDomain_IlirG.Configurations
{
     public class HistoryConfigurations : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("History");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.DateChanged)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(x => x.EntityBookId);

            builder.HasIndex(x => x.Id);
        }
    }
}
