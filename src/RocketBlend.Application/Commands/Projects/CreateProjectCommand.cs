using MediatR;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Commands.Projects;

/// <summary>
/// The create project command.
/// </summary>
public record CreateProjectCommand(Guid Id, string Name, string Path) : ICommand;

/// <summary>
/// The create project command handler.
/// </summary>
public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly ICrudService<Project, Guid> _crudService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="crudService">The crud service.</param>
    public CreateProjectCommandHandler(ICrudService<Project, Guid> crudService)
    {
        this._crudService = crudService;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Id, request.Name, request.Path);
        await this._crudService.CreateAsync(project, cancellationToken);
        return Unit.Value;
    }
}
