using FluentValidation;

namespace RocketBlend.Application.Queries.Projects;

/// <summary>
/// GetResourcesQueryValidator
/// </summary>
public class GetProjectsQueryValidator : AbstractValidator<GetProjectsQuery>
{
    /// <inheritdoc />
    public GetProjectsQueryValidator()
    {
        this.RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        this.RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}