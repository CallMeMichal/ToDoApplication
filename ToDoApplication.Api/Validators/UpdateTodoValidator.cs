using FluentValidation;
using ToDoApplication.Common.Models.Domain.Request;

namespace ToDoApplication.Api.Validators
{
    public class UpdateTodoValidator : AbstractValidator<UpdateTodoRequest>
    {
        /// <summary>
        /// Specified validation rules for the UpdateTodoRequest model.
        /// </summary>
        public UpdateTodoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .WithErrorCode("TITLE_REQUIRED")
                .MaximumLength(100)
                .WithMessage("Title must not exceed 100 characters.")
                .WithErrorCode("TITLE_TOO_LONG")
                .MinimumLength(3)
                .WithMessage("Title must be at least 3 characters long.")
                .WithErrorCode("TITLE_TOO_SHORT");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .WithErrorCode("DESCRIPTION_REQUIRED")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.")
                .WithErrorCode("DESCRIPTION_TOO_LONG")
                .MinimumLength(10)
                .WithMessage("Description must be at least 10 characters long.")
                .WithErrorCode("DESCRIPTION_TOO_SHORT");

            RuleFor(x => x.ExpirationDate)
                .NotEmpty()
                .WithMessage("Expiration date is required.")
                .WithErrorCode("EXPIRATION_DATE_REQUIRED")
                .GreaterThan(DateTime.Now)
                .WithMessage("Expiration date must be in the future.")
                .WithErrorCode("EXPIRATION_DATE_PAST")
                .LessThan(DateTime.Now.AddYears(1))
                .WithMessage("Expiration date cannot be more than a year from now.")
                .WithErrorCode("EXPIRATION_DATE_TOO_FAR");

            RuleFor(x => x.CompletePercent)
                .NotNull()
                .WithMessage("Complete percent is required.")
                .WithErrorCode("COMPLETE_PERCENT_REQUIRED")
                .InclusiveBetween(0, 100)
                .WithMessage("Complete percent must be between 0 and 100.")
                .WithErrorCode("COMPLETE_PERCENT_OUT_OF_RANGE");

            RuleFor(x => x)
                .Must(request => !string.IsNullOrEmpty(request.Title) &&
                      !request.Title.Equals(request.Description, StringComparison.OrdinalIgnoreCase))
                .WithMessage("Title and description cannot be identical.")
                .WithErrorCode("TITLE_DESCRIPTION_IDENTICAL")
                .When(x => !string.IsNullOrEmpty(x.Title) && !string.IsNullOrEmpty(x.Description));
        }
    }
}
