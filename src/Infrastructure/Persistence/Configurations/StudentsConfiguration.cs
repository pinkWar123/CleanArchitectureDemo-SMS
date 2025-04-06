using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using Domain.Entities.Students;
using Domain.Entities.Students.ValueObjects;
using Domain.Entities.Students.ValueObjects.Email;
using Domain.Entities.Students.ValueObjects.Phones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Configurations
{
    public class StudentsConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");

            // Assume BaseEntity defines Id as the primary key.
            builder.HasKey(s => s.Id);

            // Configure the StudentId value object as a string.
            var studentIdConverter = new ValueConverter<StudentId, string>(
                v => v.Value,         // How to convert StudentId to string
                v => StudentId.Create(v).Value // How to convert string to StudentId (simplistic conversion)
            );
            var studentIdComparer = new ValueComparer<StudentId>(
                (a, b) => a.Value == b.Value,               // Equality check.
                v => v.Value.GetHashCode(),                 // Hash code generation.
                v => StudentId.Create(v.Value).Value         // Deep copy: reconstruct from the stored string.
            );
            builder.Property(s => s.StudentId)
                .HasConversion(studentIdConverter)
                .IsRequired();
                
            builder.Property(s => s.StudentId).Metadata.SetValueComparer(studentIdComparer);

            // Configure the Name value object as a string.
            var nameConverter = new ValueConverter<Name, string>(
                v => v.Value,
                v => Name.Create(v).Value
            );
            builder.Property(s => s.Name)
                .HasConversion(nameConverter)
                .IsRequired();

            // Configure DateOfBirth as required.
            builder.Property(s => s.DateOfBirth)
                .IsRequired();

            // Configure Gender. Here we store the enum as a string (alternatively, store as int).
            builder.Property(s => s.Gender)
                .HasConversion<string>()
                .IsRequired();

            // Configure Faculty relationship (optional).
            builder.HasOne(s => s.Faculty)
                .WithMany() // Modify with .WithMany(f => f.Students) if Faculty has a collection.
                .HasForeignKey("FacultyId")
                .IsRequired(false);

            // Configure Address as an owned entity if provided.
            builder.OwnsOne(s => s.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .IsRequired(false);
                address.Property(a => a.City)
                    .HasColumnName("City")
                    .IsRequired(false);
                address.Property(a => a.StateOrProvince)
                    .HasColumnName("StateOrProvince")
                    .IsRequired(false);
                address.Property(a => a.Country)
                    .HasColumnName("Country")
                    .IsRequired(false);
            });

            // Configure Email value object as a string (optional).
            var emailConverter = new ValueConverter<Email, string>(
                v => v.Value,
                v => Email.Create(v).Value
            );
            builder.Property(s => s.Email)
                .HasConversion(emailConverter);

            // Configure PhoneNumber value object as a string (optional).
            var phoneConverter = new ValueConverter<PhoneNumber, string>(
                v => v.Value,
                v => PhoneNumber.Create(v).Value
            );
            builder.Property(s => s.PhoneNumber)
                .HasConversion(phoneConverter);

            // Configure LearningProgram relationship (optional).
            builder.HasOne(s => s.LearningProgram)
                .WithMany() // Modify with .WithMany(lp => lp.Students) if applicable.
                .HasForeignKey("LearningProgramId")
                .IsRequired(false);

            // Configure Status relationship (optional).
            builder.HasOne(s => s.Status)
                .WithMany() // Modify with .WithMany(s => s.Students) if applicable.
                .HasForeignKey("StatusId")
                .IsRequired(false);
        }
    }
}