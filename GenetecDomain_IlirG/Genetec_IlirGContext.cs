using System;
using System.Collections.Generic;
using System.Text;
using GenetecDomain_IlirG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GenetecDomain_IlirG
{

    public partial class Genetec_IlirGContext : DbContext
    {
        public Genetec_IlirGContext()
        {
        }

        public Genetec_IlirGContext(DbContextOptions<Genetec_IlirGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookEntities> BookEntities { get; set; }
        public virtual DbSet<History> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=NBGASHIIW10\\ILIRGASHI;database=Genetec_IlirG;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntities>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.Property(e => e.Title)
                    .HasMaxLength(500);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.PublishDate)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Authors)
                   .HasMaxLength(500);

            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.Property(e => e.DateChanged)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");              
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


