using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Statuses;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Statuses.Queries.GetStatuses
{
    public class GetStatusesQueryHandler : IQueryHandler<GetStatusesQuery, List<Status>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetStatusesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<Status>>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _unitOfWork.Statuses.GetAll();
            return Result.Success(statuses.ToList());
        }
    }
}