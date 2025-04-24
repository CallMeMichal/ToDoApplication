using FluentValidation;

namespace ToDoApplication.Api.Validators
{
    public class GetIncomingTodosValidator : AbstractValidator<DateTime>
    {
        /// <summary>
        /// Specifies validation rules for the GetIncomingTodosRequest model.
        /// </summary>
        public GetIncomingTodosValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Date is required.")
                .WithErrorCode("DATE_REQUIRED")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Date must be today or in the future.")
                .WithErrorCode("DATE_PAST");
        }
    }
}
