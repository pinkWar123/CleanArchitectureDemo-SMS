using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Faculties;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Faculties.Queries.GetFaculties
{
    public class GetFacultiesQueryHandler : IQueryHandler<GetFacultiesQuery, List<Faculty>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetFacultiesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<Faculty>>> Handle(GetFacultiesQuery request, CancellationToken cancellationToken)
        {
            var faculties = await _unitOfWork.Faculties.GetAll();
            return Result.Success(faculties.ToList());
        }
    }
}