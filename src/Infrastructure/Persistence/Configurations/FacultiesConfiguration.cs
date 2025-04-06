using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using Domain.Entities.Faculties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Configurations
{
    public class FacultiesConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("Faculties");
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