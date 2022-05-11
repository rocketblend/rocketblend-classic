using MediatR;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Commands.Builds;

/// <summary>
/// The create build command.
/// </summary>
public record CreateBuildCommand(Guid Id, string Name, string Tag, string Hash, string DownloadUrl, string FileSize) : ICommand;

/// <summary>
/// The create build command handler.
/// </summary>
public class CreateBuildCommandHandler : ICommandHandler<CreateBuildCommand>
{
    private readonly ICrudService<Build, Guid> _crudService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBuildCommandHandler"/> class.
    /// </summary>
    /// <param name="crudService">The crud service.</param>
    public CreateBuildCommandHandler(ICrudService<Build, Guid> crudService)
    {
        this._crudService = crudService;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(CreateBuildCommand request, CancellationToken cancellationToken)
    {
        var build = new Build(request.Id, request.Name, request.Tag, request.Hash, request.DownloadUrl, request.FileSize);
        await this._crudService.CreateAsync(build, cancellationToken);
        return Unit.Value;
    }
}
