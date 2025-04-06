
using Application.Abstractions.Messaging;
using Domain.Entities.Statuses;

namespace Application.Features.Statuses.Queries.GetStatuses;

public record GetStatusesQuery : IQuery<List<Status>>;
