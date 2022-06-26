using System.Reactive;
using ReactiveUI;
using RocketBlend.Presentation.Interfaces.Main.Operations;
using RocketBlend.Presentation.Services.Interfaces;
using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Presentation.ViewModels.Main.Operations;

/// <summary>
/// The operations view model.
/// </summary>
public class OperationsViewModel : ViewModelBase, IOperationsViewModel
{
    private readonly IOperationsService _operationsService;
    private readonly IDialogService _dialogService;
    private readonly IDirectoryService _directoryService;

    //private readonly IFilesOperationsMediator _filesOperationsMediator;
    //private readonly INodesSelectionService _nodesSelectionService;
    //private readonly ITrashCanService _trashCanService;

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> TestCommand { get; }

    /// <inheritdoc />
    public ReactiveCommand<Unit, Unit> OpenInDefaultCommand { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationsViewModel"/> class.
    /// </summary>
    /// <param name="operationsService">The operations service.</param>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="directoryService">The directory service.</param>
    public OperationsViewModel(
        IOperationsService operationsService,
        IDialogService dialogService,
        IDirectoryService directoryService)
    {
        this._operationsService = operationsService;
        this._dialogService = dialogService;
        this._directoryService = directoryService;

        this.TestCommand = ReactiveCommand.Create(this.CreateDirectory);
        this.OpenInDefaultCommand = ReactiveCommand.Create(this.OpenInDefaultEditor);
    }

    /// <summary>
    /// Opens the in default editor.
    /// </summary>
    private void OpenInDefaultEditor() => this._operationsService.OpenFiles(this.GetSelectedNodes());

    private void CreateDirectory() => this._operationsService.CreateDirectory("",".temp/");

    private void Download() => this._operationsService.DownloadAsync(new Uri("https://download.blender.org/release/Blender3.0/blender-3.0.0-windows-x64.zip"), "E:\\RocketBlendTesting\\Folder2");

    private void Move() => Execute(() => this._operationsService.MoveAsync(this.GetSelectedNodes(), "E:\\RocketBlendTesting\\Folder2"));

    /// <summary>
    /// Gets the selected nodes.
    /// </summary>
    /// <returns>A list of string.</returns>
    private IReadOnlyList<string> GetSelectedNodes() => new List<string>()
    {
        ".temp/",
    };

    /// <summary>
    /// Executes the.
    /// </summary>
    /// <param name="action">The action.</param>
    private static void Execute(Action action) => Task.Factory.StartNew(action);
}
