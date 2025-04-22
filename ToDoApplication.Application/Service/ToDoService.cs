using ToDoApplication.Common.Interfaces;
using ToDoApplication.Common.Models.Database;

namespace ToDoApplication.Application.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepostiory _toDoRepository;

        public ToDoService(IToDoRepostiory toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<List<TodoItem>> GetAllTodos()
        {
            return await _toDoRepository.GetAllAsync();
        }
    }
}
