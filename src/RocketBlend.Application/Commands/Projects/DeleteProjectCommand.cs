using MediatR;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Commands.Projects;

/// <summary>
/// The delete project command.
/// </summary>
public record DeleteProjectCommand(Guid Id) : ICommand;

/// <summary>
/// The delete product command handler.
/// </summary>
public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
{
    private readonly ICrudService<Project, Guid> _crudService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="crudService">The crud service.</param>
    public DeleteProjectCommandHandler(ICrudService<Project, Guid> crudService)
    {
        this._crudService = crudService;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await this._crudService.GetByIdAsync(request.Id, true);
        await this._crudService.DeleteAsync(entity!, cancellationToken);
        return Unit.Value;
    }
}
