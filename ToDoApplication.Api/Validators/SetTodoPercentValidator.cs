using FluentValidation;
using ToDoApplication.Common.Models.Domain.Request;

namespace ToDoApplication.Api.Validators
{
    public class SetTodoPercentValidator : AbstractValidator<int>
    {
        public SetTodoPercentValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("Complete percent value is required.")
                .WithErrorCode("PERCENT_REQUIRED")
                .InclusiveBetween(0, 100)
                .WithMessage("Complete percent must be between 0 and 100.")
                .WithErrorCode("PERCENT_OUT_OF_RANGE");
        }
    }
}
