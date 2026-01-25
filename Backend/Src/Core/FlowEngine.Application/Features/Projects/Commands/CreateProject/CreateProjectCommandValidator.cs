using FlowEngine.Application.Interfaces;
using FluentValidation;

namespace FlowEngine.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.ProjectName)
            .NotNull()
            .WithName(p => translator[nameof(p.ProjectName)]);
    }
}