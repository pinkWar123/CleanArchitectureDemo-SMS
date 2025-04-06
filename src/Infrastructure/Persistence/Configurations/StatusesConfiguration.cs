using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using Domain.Entities.Statuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Configurations
{
    public class StatusesConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Statuses");

            builder.HasKey(s => s.Id);

            var nameConverter = new ValueConverter<Name, string>(
                v => v.Value,
                v => Name.Create(v).Value
            );
            builder.Property(s => s.Name)
                .HasConversion(nameConverter)
                .IsRequired();
        }
    }
}