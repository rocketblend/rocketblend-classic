using MediatR;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Commands.Projects;

/// <summary>
/// The update project command.
/// </summary>
public record UpdateProjectCommand(Guid Id, string Name, string Path) : ICommand;

/// <summary>
/// The update project command handler.
/// </summary>
public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
{
    private readonly ICrudService<Project, Guid> _crudService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="crudService">The crud service.</param>
    public UpdateProjectCommandHandler(ICrudService<Project, Guid> crudService)
    {
        this._crudService = crudService;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await this._crudService.GetByIdAsync(request.Id, true);

        entity!.Update(request.Name, request.Path);

        await this._crudService.UpdateAsync(entity, cancellationToken);
        return Unit.Value;
    }
}
