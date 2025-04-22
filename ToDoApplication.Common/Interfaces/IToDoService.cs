using ToDoApplication.Common.Models.Database;

namespace ToDoApplication.Common.Interfaces
{
    public interface IToDoService
    {
        Task<List<TodoItem>> GetAllTodos();
    }
}
