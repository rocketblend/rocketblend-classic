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
    private readonly IProjectRepository _collectionRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectQueryHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public GetProjectQueryHandler(IProjectRepository collectionRepository, IMapper mapper)
    {
        this._collectionRepository = collectionRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var query = this._collectionRepository.Get(new ProjectQueryOptions
        {
            AsNoTracking = true,
        });

        var mapped = query
            .Where(x => x.Id == request.Id)
            .ProjectTo<ProjectDto>(this._mapper.ConfigurationProvider);

        var collection = await this._collectionRepository.FirstOrDefaultAsync(mapped, cancellationToken);

        return collection ?? throw new NotFoundException("Project", request.Id);
    }
}
