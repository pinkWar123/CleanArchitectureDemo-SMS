using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using Domain.Entities;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Domain.Entities.Students;
using Domain.Entities.Students.Enums;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.Mongodb
{
    
    public static class MongoMappings
    {
        public static void RegisterMappings()
        {
            // Optionally register a convention pack for camelCase, enum representation, etc.
            var conventionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(BsonType.String)
            };
            ConventionRegistry.Register("CustomConventions", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Name)))
            {
                BsonClassMap.RegisterClassMap<Name>(cm =>
                {
                    cm.SetIgnoreExtraElements(true);
                    cm.MapMember(n => n.Value).SetElementName("value");
                });
            }


            if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            {
                BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
                {
                    cm.MapIdMember(b => b.Id).SetSerializer(new GuidSerializer(BsonType.String));
                });
            }

            RegisterFacultyMapping();
            RegisterLearningProgramMapping();
            RegisterStatusMapping();
            RegisterStudentMapping();
        }

        private static void RegisterFacultyMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Faculty)))
            {
                BsonClassMap.RegisterClassMap<Faculty>(cm =>
                {
                    cm.SetIgnoreExtraElements(true);
                    // cm.MapIdMember(f => f.Id);
                    // Map Name value object as a string.
                    cm.MapIdMember(f => f.Id)
              .SetSerializer(new GuidSerializer(BsonType.String));
                    cm.UnmapMember(f => f.Name);
                    cm.MapMember(f => f.Name)
                    .SetSerializer(new NameSerializer())
                    .SetElementName("Name");
                });
            }
        }

        private static void RegisterLearningProgramMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(LearningProgram)))
            {
                BsonClassMap.RegisterClassMap<LearningProgram>(cm =>
                {
                    cm.SetIgnoreExtraElements(true);
                    // cm.MapIdMember(lp => lp.Id);
                    cm.MapMember(lp => lp.Name)
                    .SetSerializer(new NameSerializer())
                    .SetElementName("Name");
                });
            }
        }

        private static void RegisterStatusMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Status)))
            {
                BsonClassMap.RegisterClassMap<Status>(cm =>
                {
                    cm.SetIgnoreExtraElements(true);
                    // cm.MapIdMember(s => s.Id);
                    cm.MapMember(s => s.Name)
                    .SetSerializer(new NameSerializer())
                    .SetElementName("Name");
                });
            }
        }

        private static void RegisterStudentMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Student)))
            {
                BsonClassMap.RegisterClassMap<Student>(cm =>
                {
                    cm.SetIgnoreExtraElements(true);
                    // cm.MapIdMember(s => s.Id);

                    // Map StudentId value object to a string.
                    cm.MapMember(s => s.StudentId)
                    .SetSerializer(new StudentIdSerializer())
                    .SetElementName("StudentId");

                    // Map Name value object to a string.
                    cm.MapMember(s => s.Name)
                    .SetSerializer(new NameSerializer())
                    .SetElementName("Name");

                    // Map DateOfBirth normally.
                    cm.MapMember(s => s.DateOfBirth)
                    .SetElementName("DateOfBirth")
                    .SetSerializer(new DateTimeSerializer(DateTimeKind.Utc));

                    // Map Gender as string (using EnumRepresentationConvention or explicitly).
                    cm.MapMember(s => s.Gender)
                    .SetSerializer(new EnumSerializer<Gender>(BsonType.String))
                    .SetElementName("Gender");

                    // For relationships, store the related entity's Id.
                    // You may choose to embed them instead if required.
                    cm.MapMember(s => s.Faculty)
                    .SetIgnoreIfNull(true)
                    .SetElementName("FacultyId");

                    cm.MapMember(s => s.LearningProgram)
                    .SetIgnoreIfNull(true)
                    .SetElementName("LearningProgramId");

                    cm.MapMember(s => s.Status)
                    .SetIgnoreIfNull(true)
                    .SetElementName("StatusId");

                    // Map optional value objects Email and PhoneNumber as strings.
                    cm.MapMember(s => s.Email)
                    .SetIgnoreIfNull(true)
                    .SetSerializer(new EmailSerializer())
                    .SetElementName("Email");

                    cm.MapMember(s => s.PhoneNumber)
                    .SetIgnoreIfNull(true)
                    .SetSerializer(new PhoneSerializer())
                    .SetElementName("PhoneNumber");

                    // Map Address as an embedded document.
                    cm.MapMember(s => s.Address)
                    .SetIgnoreIfNull(true)
                    .SetSerializer(new AddressSerializer())
                    .SetElementName("Address");
                });
            }
        }
    }
}