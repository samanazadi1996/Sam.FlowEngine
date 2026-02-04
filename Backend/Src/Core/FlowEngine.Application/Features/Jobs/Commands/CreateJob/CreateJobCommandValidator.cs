using FlowEngine.Application.Interfaces;
using FluentValidation;

namespace FlowEnginex.Application.Features.Jobs.Commands.CreateJob;

public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator(ITranslator translator)
    {
        RuleFor(p => p.Name)
            .NotNull()
            .WithName(p => translator[nameof(p.Name)]);
    }
}