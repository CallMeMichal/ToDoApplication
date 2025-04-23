using ToDoApplication.Common.Models.Database;
using ToDoApplication.Common.Models.Domain.Request;
using ToDoApplication.Common.Models.Domain.Response;

namespace ToDoApplication.Common.Interfaces
{
    public interface IToDoService
    {
        Task<ApiResponse> CreateTodo(CreateTodoRequest request);
        Task<ApiResponse> DeleteTodo(int id);
        Task<List<GetAllTodosResponse>> GetAllTodos();
        Task<List<GetIncomingTodosResponse>> GetIncomingTodos();
        Task<GetTodoByIdResponse> GetTodoById(int id);
        Task<ApiResponse> MarkDoneTodo(int id);
        Task<ApiResponse> SetTodoPercent(int id, int amount);
        Task<ApiResponse> UpdateTodo(UpdateTodoRequest request);
    }
}
