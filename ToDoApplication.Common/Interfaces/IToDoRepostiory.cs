using ToDoApplication.Common.Models.Database;
using ToDoApplication.Common.Models.DTO;

namespace ToDoApplication.Common.Interfaces
{
    public interface IToDoRepostiory
    {
        Task<List<CreateTodoDTO>> GetAllAsync();
        Task<TodoItem> GetTodoById(int id);
        Task<List<TodoItem>> GetIncomingTodos();
        Task<bool> CreateTodo(CreateTodoDTO createTodoDTO);
        Task<bool> UpdateTodo(TodoItem todo);
        Task<bool> SetTodoPercent(int id, int amount);
        Task<bool> DeleteTodo(int id);
        Task<bool> MarkTodoAsCompleted(int id);
    }
}
