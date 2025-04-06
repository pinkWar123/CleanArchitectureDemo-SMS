using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Faculties;

namespace Application.Features.Faculties.Queries.GetFaculties;

public record GetFacultiesQuery : IQuery<List<Faculty>>;