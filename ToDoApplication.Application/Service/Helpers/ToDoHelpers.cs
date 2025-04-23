using System.Net;
using ToDoApplication.Common.Models.Domain.Response;

namespace ToDoApplication.Application.Service.ServiceHelpers
{
    public static class ToDoHelpers
    {
        public static ApiResponse CreateResponse(bool isSuccess, string message, HttpStatusCode statusCode)
        {
            return new ApiResponse
            {
                isSuccess = isSuccess,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}
