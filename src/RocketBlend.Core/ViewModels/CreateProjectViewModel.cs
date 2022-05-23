using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Application.Commands.Projects;
using RocketBlend.Core.Utils;
using RocketBlend.Core.ViewModels.Projects.Create;
using Splat;

namespace RocketBlend.Core.ViewModels;

public class CreateProjectViewModel : ReactiveObject, IActivatableViewModel, IScreen
{
    private MediatR.ISender _mediator = null!;

    private readonly CreateProjectSelectBuildViewModel _selectBuildView;

    private readonly CreateProjectConfigureViewModel _configureProjectView;

    protected MediatR.ISender Mediator => this._mediator ??= Locator.Current.GetRequiredService<MediatR.ISender>();

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    ///<inheritdoc/>
    public RoutingState Router { get; } = new RoutingState();

    [Reactive] public int StepIndex { get; set; }

    private IReadOnlyList<IRoutableViewModel> Steps { get; }

    public ReactiveCommand<Unit, Unit> GoNext { get; }
    public ReactiveCommand<Unit, Unit> GoBack { get; }
    public ReactiveCommand<Unit, string?> CreateProjectCommand { get; }


    public CreateProjectViewModel()
    {
        this._selectBuildView = new(this);
        this._configureProjectView = new(this);

        this.Steps = new List<IRoutableViewModel>()
        {
            this._selectBuildView,
            this._configureProjectView,
        };

        this.GoNext = ReactiveCommand.Create(this.NavigateForward);
        this.GoBack = ReactiveCommand.Create(this.NavigateBack);

        this.CreateProjectCommand = ReactiveCommand.CreateFromTask(this.CreateProject);

        this.WhenAnyValue(x => x.StepIndex).Subscribe(i => this.NavigateToTab(i));

        // this.Router.NavigationStack.Add(new CreateProjectSelectBuildViewModel(this));
        //this.Router.Navigate.Execute(new CreateProjectSelectBuildViewModel(this));
    }

    private void NavigateForward()
    {
        if (this.StepIndex < this.Steps.Count - 1)
        {
            this.StepIndex += 1;
        }
    }

    private void NavigateBack()
    {
        if (this.StepIndex > 0)
        {
            this.StepIndex -= 1;
        }
    }

    private void NavigateToTab(int tabIndex)
    {
        if (tabIndex < this.Steps.Count)
        {
            this.Router.Navigate.Execute(this.Steps[tabIndex]);
        }
    }

    private async Task<string?> CreateProject()
    {
        Guid id = Guid.NewGuid();
        string name = this._configureProjectView.ProjectName;
        string filePath = this._configureProjectView.ProjectLocation;
        Guid installId = this._selectBuildView.SelectedInstall!.Id;

        await this.Mediator.Send(new CreateProjectCommand(id, $"{name}.blend", filePath, installId, name));

        return id.ToString();
    }
}
