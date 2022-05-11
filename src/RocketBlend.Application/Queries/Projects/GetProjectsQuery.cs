using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Common.Application.Models;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Queries.Projects;

/// <summary>
/// The get projects query.
/// </summary>
public record GetProjectsQuery(string Name = "", int PageNumber = 1, int PageSize = 25) : IRequest<PaginatedList<ProjectDto>>;

/// <summary>
/// The get projects handler.
/// </summary>
public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectDto>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectsHandler"/> class.
    /// </summary>
    /// <param name="ProjectRepository">The weather forecast repository.</param>
    public GetProjectsHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        this._projectRepository = projectRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public Task<PaginatedList<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = this._projectRepository.Get(new ProjectQueryOptions
        {
            ProjectName = request.Name,
            IncludeInstall = true,
            AsNoTracking = true,
        });

        return Task.FromResult(query
            .ProjectTo<ProjectDto>(this._mapper.ConfigurationProvider)
            .ToPaginatedList(request.PageNumber, request.PageSize));
    }
}
