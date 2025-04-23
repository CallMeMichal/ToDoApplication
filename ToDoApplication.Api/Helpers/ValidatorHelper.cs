using FluentValidation;
using System.Net;
using ToDoApplication.Common.Models.Domain.Response;

namespace ToDoApplication.Api.Helpers
{
    public static class ValidatorHelper
    {
        /// <summary>
        /// Validates the request using the specified validator and returns API response if validation fails.
        /// </summary>
        /// <typeparam name="T">Type of request to validate</typeparam>
        /// <param name="request">Request to validate</param>
        /// <param name="validator">Validator to use</param>
        /// <returns>API response with validation errors if validation failed, null otherwise</returns>
        public static ApiResponse ValidateRequest<T>(T request, IValidator<T> validator)
        {
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorDetails = validationResult.Errors.Select(e => new
                {
                    Property = e.PropertyName,
                    Message = e.ErrorMessage,
                    Code = e.ErrorCode
                }).ToList();

                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    isSuccess = false,
                    Message = "Validation failed",
                    Errors = errorDetails
                };
            }

            return null;
        }
    }
}
