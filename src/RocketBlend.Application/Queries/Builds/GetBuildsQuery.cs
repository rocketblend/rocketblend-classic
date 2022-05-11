using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Common.Application.Models;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Queries.Builds;

/// <summary>
/// The get projects query.
/// </summary>
public record GetBuildsQuery(int PageNumber = 1, int PageSize = 25) : IRequest<PaginatedList<BuildDto>>;

/// <summary>
/// The get projects handler.
/// </summary>
public class GetBuildsHandler : IRequestHandler<GetBuildsQuery, PaginatedList<BuildDto>>
{
    private readonly IBuildRepository _projectRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBuildsHandler"/> class.
    /// </summary>
    /// <param name="BuildRepository">The weather forecast repository.</param>
    public GetBuildsHandler(IBuildRepository projectRepository, IMapper mapper)
    {
        this._projectRepository = projectRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public Task<PaginatedList<BuildDto>> Handle(GetBuildsQuery request, CancellationToken cancellationToken)
    {
        var query = this._projectRepository.Get(new BuildQueryOptions
        {
            AsNoTracking = true,
        });

        return Task.FromResult(query
            .ProjectTo<BuildDto>(this._mapper.ConfigurationProvider)
            .ToPaginatedList(request.PageNumber, request.PageSize));
    }
}
