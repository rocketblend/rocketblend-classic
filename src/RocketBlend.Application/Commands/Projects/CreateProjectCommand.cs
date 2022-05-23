using MediatR;
using RocketBlend.Blender.Interfaces;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Exceptions;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Commands.Projects;

/// <summary>
/// The create project command.
/// </summary>
public record CreateProjectCommand(Guid Id, string FileName, string FilePath, Guid? InstallId = null, string ? Name = null) : ICommand;

/// <summary>
/// The create project command handler.
/// </summary>
public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand>
{
    private readonly ICrudService<Project, Guid> _projectService;
    private readonly IInstallRepository _installRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="projectService">The project service.</param>
    /// <param name="installRepository">The install repository.</param>
    public CreateProjectCommandHandler(
        ICrudService<Project, Guid> projectService,
        IInstallRepository installRepository)
    {
        this._projectService = projectService;
        this._installRepository = installRepository;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var query = this._installRepository.Get(new InstallQueryOptions
        {
            AsNoTracking = false,
        });

        if(request.InstallId is not null)
        {
            query = query.Where(x => x.Id == request.InstallId);
        }

        var install = await this._installRepository.FirstOrDefaultAsync(query, cancellationToken);

        if(install is null)
        {
            throw new NotFoundException("Install");
        }

        string projectName = request.Name is not null ? request.Name : Path.GetFileNameWithoutExtension(request.FileName);
        var project = new Project(request.Id, projectName, request.FileName, request.FilePath, install.Id);

        await this._projectService.CreateAsync(project, cancellationToken);
        return Unit.Value;
    }
}
