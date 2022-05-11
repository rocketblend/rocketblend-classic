using MediatR;
using RocketBlend.WebScraper.Blender.Core.Enums;
using RocketBlend.WebScraper.Blender.Core.Interfaces;

namespace RocketBlend.Application.Queries.Builds.External;

public record GetExternalBuildsQuery(BuildPlatform BuildPlatform) : IRequest<IReadOnlyList<ExternalBuildDto>>;

/// <summary>
/// The get external builds handler.
/// </summary>
public class GetExternalBuildsHandler : IRequestHandler<GetExternalBuildsQuery, IReadOnlyList<ExternalBuildDto>>
{
    private readonly IBlenderBuildScraperService _blenderBuildScraper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExternalBuildsHandler"/> class.
    /// </summary>
    /// <param name="blenderBuildScraper">The blender build scraper.</param>
    public GetExternalBuildsHandler(IBlenderBuildScraperService blenderBuildScraper)
    {
        this._blenderBuildScraper = blenderBuildScraper;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ExternalBuildDto>> Handle(GetExternalBuildsQuery request, CancellationToken cancellationToken)
    {
        var result = await this._blenderBuildScraper.ScrapeStableReleaseBuilds(request.BuildPlatform);

        return result.Select(x => new ExternalBuildDto(x.Name, x.Tag, x.DownloadUrl, x.Hash, x.Filesize)).ToList();
    }
}
