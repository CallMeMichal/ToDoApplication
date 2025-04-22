using ToDoApplication.Common.Models.Database;

namespace ToDoApplication.Common.Interfaces
{
    public interface IToDoRepostiory
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<List<TodoItem>> GetIncomingTodos();
        Task<TodoItem> GetTodoById(int id);
    }
}
