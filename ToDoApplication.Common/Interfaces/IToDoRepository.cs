﻿using ToDoApplication.Common.Models.Database;
using ToDoApplication.Common.Models.DTO;

namespace ToDoApplication.Common.Interfaces
{
    public interface IToDoRepository
    {
        Task<List<CreateTodoDTO>> GetAllAsync();
        Task<GetTodoById?> GetTodoById(int id);
        Task<List<GetIncomingTodosDTO>> GetIncomingTodos();
        Task<bool> CreateTodo(CreateTodoDTO createTodoDTO);
        Task<bool> UpdateTodo(TodoItem todo);
        Task<bool> SetTodoPercent(int id, int amount);
        Task<bool> DeleteTodo(int id);
        Task<bool> MarkTodoAsCompleted(int id);
    }
}
