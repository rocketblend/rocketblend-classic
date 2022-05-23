using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RocketBlend.Common.Application.Exceptions;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Queries.Projects;

/// <summary>
/// The get collection query.
/// </summary>
public record GetProjectQuery(Guid Id) : IRequest<ProjectDto>;

/// <summary>
/// The get collection query handler.
/// 
/// </summary>
public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDto>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectQueryHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public GetProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        this._projectRepository = projectRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var query = this._projectRepository.Get(new ProjectQueryOptions
        {
            AsNoTracking = true,
        });

        var mapped = query
            .Where(x => x.Id == request.Id)
            .ProjectTo<ProjectDto>(this._mapper.ConfigurationProvider);

        var project = await this._projectRepository.FirstOrDefaultAsync(mapped, cancellationToken);

        return project ?? throw new NotFoundException("Project", request.Id);
    }
}
