using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Common.Application.Models;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Queries.Installs;

/// <summary>
/// The get projects query.
/// </summary>
public record GetInstallsQuery(int PageNumber = 1, int PageSize = 25) : IRequest<PaginatedList<InstallDto>>;

/// <summary>
/// The get projects handler.
/// </summary>
public class GetInstallsHandler : IRequestHandler<GetInstallsQuery, PaginatedList<InstallDto>>
{
    private readonly IInstallRepository _projectRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInstallsHandler"/> class.
    /// </summary>
    /// <param name="InstallRepository">The weather forecast repository.</param>
    public GetInstallsHandler(IInstallRepository projectRepository, IMapper mapper)
    {
        this._projectRepository = projectRepository;
        this._mapper = mapper;
    }

    /// <inheritdoc />
    public Task<PaginatedList<InstallDto>> Handle(GetInstallsQuery request, CancellationToken cancellationToken)
    {
        var query = this._projectRepository.Get(new InstallQueryOptions
        {
            AsNoTracking = true,
        });

        return Task.FromResult(query
            .ProjectTo<InstallDto>(this._mapper.ConfigurationProvider)
            .ToPaginatedList(request.PageNumber, request.PageSize));
    }
}
