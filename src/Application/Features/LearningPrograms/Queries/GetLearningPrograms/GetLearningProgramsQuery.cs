using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.LearningPrograms;

namespace Application.Features.LearningPrograms.Queries.GetLearningPrograms;

public record GetLearningProgramsQuery : IQuery<List<LearningProgram>>;