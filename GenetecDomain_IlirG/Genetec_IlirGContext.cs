using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GenetecDomain_IlirG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenetecDomain_IlirG
{

    public partial class Genetec_IlirGContext : DbContext
    {      
        public Genetec_IlirGContext(DbContextOptions<Genetec_IlirGContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Genetec_IlirGContext).Assembly);
            modelBuilder.Entity<History>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();
            modelBuilder.Entity<BookEntities>()
                       .Property(f => f.Id)
                       .ValueGeneratedOnAdd();
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToLocalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Local));

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToLocalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Local) : v);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.IsKeyless)
                    continue;

                foreach (var property in entityType.GetProperties())
                    if (property.ClrType == typeof(DateTime))
                        property.SetValueConverter(dateTimeConverter);
                    else if (property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(nullableDateTimeConverter);
            }
        }
     
    }
}


