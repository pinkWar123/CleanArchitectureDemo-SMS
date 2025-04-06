using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Students;

namespace Application.Features.Students.Queries.FindStudents;

public record FindStudentsQuery : IQuery<List<Student>>; 