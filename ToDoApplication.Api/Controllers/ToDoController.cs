using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoApplication.Application.Service;

namespace ToDoApplication.Api.Controllers
{

    /// <summary>
    /// Controller for managing todos
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ToDoController : Controller
    {
        private readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }



        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task GetAllTodos()
        {
            
        }

        /// <summary>
        /// Get todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task GetTodoById(int id)
        {
        }

        /// <summary>
        /// Get incoming todos
        /// </summary>
        /// <returns></returns>
        [HttpGet("incoming")]
        public async Task GetIncomingTodos()
        {
        }

        /// <summary>
        /// Create todo
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task CreateTodo()
        {
        }

        /// <summary>
        /// Update todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task UpdateTodo(int id)
        {
        }

        [HttpPatch("{id}/percent")]
        public async Task SetTodoPercent(int id /*[FromBody] PercentUpdateModel percentModel*/)
        {
        }

        /// <summary>
        /// Delete todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteTodo(int id)
        {
        }

        /// <summary>
        /// Mark todo as complete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/done")]
        public async Task MarkDoneTodo(int id)
        {
        }
    }
}