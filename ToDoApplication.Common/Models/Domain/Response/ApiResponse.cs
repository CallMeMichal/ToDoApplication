using System.Net;

namespace ToDoApplication.Common.Models.Domain.Response
{
    public class ApiResponse
    {
        public bool isSuccess { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object? Errors { get; set; }
    }
}
