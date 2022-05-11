using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RocketBlend.Common.Application.Exceptions;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Queries.Builds;

/// <summary>
/// The get collection query.
/// </summary>
public record GetBuildQuery(Guid Id) : IRequest<BuildDto>;

/// <summary>
/// The get collection query handler.
/// 
/// </summary>
public class GetBuildQueryHandler : IRequestHandler<GetBuildQuery, BuildDto>
{
    private readonly IBuildRepository _collectionRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBuildQueryHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public GetBuildQueryHandler(IBuildRepository collectionRepository, IMapper mapper)
    {
        this._collectionRepository = collectionRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<BuildDto> Handle(GetBuildQuery request, CancellationToken cancellationToken)
    {
        var query = this._collectionRepository.Get(new BuildQueryOptions
        {
            AsNoTracking = true,
        });

        var mapped = query
            .Where(x => x.Id == request.Id)
            .ProjectTo<BuildDto>(this._mapper.ConfigurationProvider);

        var collection = await this._collectionRepository.FirstOrDefaultAsync(mapped, cancellationToken);

        return collection ?? throw new NotFoundException("Build", request.Id);
    }
}
