using Microsoft.EntityFrameworkCore;
using ToDoApplication.Common.Interfaces;
using ToDoApplication.Common.Models.Database;
using ToDoApplication.Infrastructure.Context;

namespace ToDoApplication.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepostiory
    {
        private readonly AppDbContext _context;

        public ToDoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {

            return await _context.TodoItem.ToListAsync();
        }
        public async Task<TodoItem> GetTodoById(int id)
        {
            return await _context.TodoItem.FirstOrDefaultAsync(x=>x.Id == id);
        }
/*        public async Task<TodoItem> GetTodoByTime(DateTime time, bool isRange)
        {
            throw new NotImplementedException();
            return null;*//*await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);*//*
        }*/

        public async Task<List<TodoItem>> GetIncomingTodos()
        {
            return await _context.TodoItem.Where(x => x.ExpirationDate > DateTime.Now).ToListAsync();
        }

        public async Task<TodoItem> CreateTodo(TodoItem todo)
        {
            await _context.TodoItem.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<TodoItem> UpdateTodo(TodoItem todo)
        {
            _context.TodoItem.Update(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task SetTodoPercent()
        {
            throw new NotImplementedException();
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

        public async Task MarkTodoAsCompleted()
        {
            throw new NotImplementedException();
        }

    }
}
