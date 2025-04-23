using Microsoft.EntityFrameworkCore;
using ToDoApplication.Common.Interfaces;
using ToDoApplication.Common.Models.Database;
using ToDoApplication.Common.Models.DTO;
using ToDoApplication.Infrastructure.Context;

namespace ToDoApplication.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CreateTodoDTO>> GetAllAsync()
        {
            var todos = await _context.TodoItem.ToListAsync();
            var todosDto = new List<CreateTodoDTO>();
            foreach (var todo in todos)
            {
                var createTodoDTO = new CreateTodoDTO
                {
                    Title = todo.Title,
                    Description = todo.Description,
                    ExpirationDate = todo.ExpirationDate,
                    CompletePercent = todo.CompletePercent,
                };
                todosDto.Add(createTodoDTO);
            }

            return todosDto;
        }
        public async Task<GetTodoById?> GetTodoById(int id)
        {
            var todo = await _context.TodoItem.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return null;

            return new GetTodoById
            {
                CompletePercent = todo.CompletePercent,
                Description = todo.Description,
                ExpirationDate = todo.ExpirationDate,
                Title = todo.Title
            };
        }


        public async Task<List<GetIncomingTodosDTO>> GetIncomingTodos()
        {
            var todoList = await _context.TodoItem.Where(x => x.ExpirationDate > DateTime.Now).ToListAsync();
            var incomingTodos = new List<GetIncomingTodosDTO>();

            foreach(var todo in todoList)
            {
                var incomingTodo = new GetIncomingTodosDTO
                {
                    Title = todo.Title,
                    Description = todo.Description,
                    ExpirationDate = todo.ExpirationDate,
                    CompletePercent = todo.CompletePercent
                };
                incomingTodos.Add(incomingTodo);
            }

            return incomingTodos;
        }

        public async Task<bool> CreateTodo(CreateTodoDTO createTodoDTO)
        {
            var todo = new TodoItem
            {
                Title = createTodoDTO.Title,
                Description = createTodoDTO.Description,
                ExpirationDate = createTodoDTO.ExpirationDate,
                CompletePercent = createTodoDTO.CompletePercent,
            };

            try
            {
                await _context.TodoItem.AddAsync(todo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateTodo(TodoItem todo)
        {
            try
            {
                _context.TodoItem.Update(todo);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> SetTodoPercent(int id, int amount)
        {
            try
            {
                var todo = await _context.TodoItem.FirstOrDefaultAsync(x => x.Id == id);
                todo.CompletePercent = amount;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }


        public async Task<bool> DeleteTodo(int id)
        {
            var todo = await _context.TodoItem.FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null)
                return false;
            _context.TodoItem.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkTodoAsCompleted(int id)
        {
            var todo = await _context.TodoItem.FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null)
                return false;
            todo.CompletePercent = 100;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
