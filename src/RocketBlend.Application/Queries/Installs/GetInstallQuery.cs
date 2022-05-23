using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RocketBlend.Common.Application.Exceptions;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Queries.Installs;

/// <summary>
/// The get collection query.
/// </summary>
public record GetInstallQuery(Guid Id) : IRequest<InstallDto>;

/// <summary>
/// The get collection query handler.
/// 
/// </summary>
public class GetInstallQueryHandler : IRequestHandler<GetInstallQuery, InstallDto>
{
    private readonly IInstallRepository _installRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInstallQueryHandler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper.</param>
    public GetInstallQueryHandler(IInstallRepository collectionRepository, IMapper mapper)
    {
        this._installRepository = collectionRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<InstallDto> Handle(GetInstallQuery request, CancellationToken cancellationToken)
    {
        var query = this._installRepository.Get(new InstallQueryOptions
        {
            AsNoTracking = true,
        });

        var mapped = query
            .Where(x => x.Id == request.Id)
            .ProjectTo<InstallDto>(this._mapper.ConfigurationProvider);

        var collection = await this._installRepository.FirstOrDefaultAsync(mapped, cancellationToken);

        return collection ?? throw new NotFoundException("Install", request.Id);
    }
}
