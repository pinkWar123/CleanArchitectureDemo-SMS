using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.LearningPrograms;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.LearningPrograms.Queries.GetLearningPrograms
{
    public class GetLearningProgramsQueryHandler : IQueryHandler<GetLearningProgramsQuery, List<LearningProgram>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetLearningProgramsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<LearningProgram>>> Handle(GetLearningProgramsQuery request, CancellationToken cancellationToken)
        {
            var programs = await _unitOfWork.LearningPrograms.GetAll();
            return Result.Success(programs.ToList());
        }
    }
}