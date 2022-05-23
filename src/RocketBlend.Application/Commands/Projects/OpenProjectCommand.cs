using MediatR;
using RocketBlend.Blender.Interfaces;
using RocketBlend.Common.Application.Commands;
using RocketBlend.Common.Application.Exceptions;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Application.Commands.Projects;

public record OpenProjectCommand(Guid Id) : ICommand;

/// <summary>
/// The open project command handler.
/// </summary>
public class OpenProjectCommandHandler : ICommandHandler<OpenProjectCommand>
{
    private readonly IBlenderClient _blenderClient;
    private readonly IProjectRepository _projectRepository;
    private readonly ICrudService<Project, Guid> _projectService;

    public OpenProjectCommandHandler(
        IProjectRepository projectRepository,
        ICrudService<Project, Guid> projectService,
        IBlenderClient blenderClient)
    {
        this._projectRepository = projectRepository;
        this._projectService = projectService;
        this._blenderClient = blenderClient;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(OpenProjectCommand request, CancellationToken cancellationToken)
    {
        // var entity = await this._projectService.GetByIdAsync(request.Id, true);

        var query = this._projectRepository.Get(new ProjectQueryOptions
        {
            AsNoTracking = false,
            IncludeInstall = true,
        });

        query = query.Where(x => x.Id == request.Id);

        var entity = await this._projectRepository.FirstOrDefaultAsync(query, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("Project", request.Id);
        }

        if (entity!.IsValid && entity.Install.IsValid)
        {
            this._blenderClient.Executable = entity!.Install.ExecutablePath;
            await this._blenderClient.OpenProject(entity.ExecutablePath);
        }

        return Unit.Value;
    }
}
