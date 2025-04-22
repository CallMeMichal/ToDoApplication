using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Common.Interfaces;
using ToDoApplication.Common.Models.Domain.Request;
using ToDoApplication.Common.Models.Domain.Response;

namespace ToDoApplication.Api.Controllers
{

    /// <summary>
    /// Controller for managing todos
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ToDoController : Controller
    {
        private readonly IToDoService _IToDoService;

        public ToDoController(IToDoService iToDoService)
        {
            _IToDoService = iToDoService;
        }

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<List<GetAllTodosResponse>> GetAllTodos()
        {
            var response = await _IToDoService.GetAllTodos();
            return response;
        }

        /// <summary>
        /// Get todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<GetTodoByIdResponse> GetTodoById(int id)
        {
            var response = await _IToDoService.GetTodoById(id);
            return response;
        }

        /// <summary>
        /// Get incoming todos
        /// </summary>
        /// <returns></returns>
        [HttpGet("incoming")]
        public async Task<List<GetIncomingTodosResponse>> GetIncomingTodos(DateTime dateTime)
        {
            var response = await _IToDoService.GetIncomingTodos();
            return response;
        }


        /// <summary>
        /// Create todo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ApiResponse> CreateTodo(CreateTodoRequest request)
        {
            var response = await _IToDoService.CreateTodo(request);
            return response;
        }

        /// <summary>
        /// Update todo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateTodo(UpdateTodoRequest request)
        {
            var response = await _IToDoService.UpdateTodo(request);
            return response;
        }

        /// <summary>
        /// Change Complete Percent of Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/percent")]
        public async Task<ApiResponse> SetTodoPercent(int id)
        {
            var response = await _IToDoService.SetTodoPercent(id);
            return response;
        }

        /// <summary>
        /// Delete todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteTodo(int id)
        {
            var response = await _IToDoService.DeleteTodo(id);
            return response;
        }

        /// <summary>
        /// Mark todo as complete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/done")]
        public async Task<ApiResponse> MarkDoneTodo(int id)
        {
            var response =  await _IToDoService.MarkDoneTodo(id);
            return response;
        }
    }
}