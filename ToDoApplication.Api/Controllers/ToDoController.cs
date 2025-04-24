using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDoApplication.Api.Helpers;
using ToDoApplication.Api.Validators;
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
        private readonly ILogger<ToDoController> _logger;

        public ToDoController(IToDoService iToDoService, ILogger<ToDoController> logger)
        {
            _IToDoService = iToDoService;
            _logger = logger;
        }



        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<List<GetAllTodosResponse>> GetAllTodos()
        {
            _logger.LogInformation("GetAllTodos START");

            var response = await _IToDoService.GetAllTodos();
            _logger.LogInformation($"Response: {JsonSerializer.Serialize(response)}");
            _logger.LogInformation("GetAllTodos END");
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
            _logger.LogInformation("GetTodoById START");
            var response = await _IToDoService.GetTodoById(id);
            _logger.LogInformation($"Response: {JsonSerializer.Serialize(response)}");
            _logger.LogInformation("GetTodoById END");
            return response;
        }

        /// <summary>
        /// Get incoming todos
        /// </summary>
        /// <returns></returns>
        [HttpGet("incoming")]
        public async Task<ActionResult<List<GetIncomingTodosResponse>>> GetIncomingTodos(DateTime dateTime)
        {
            _logger.LogInformation("GetIncomingTodos START: " + dateTime);
            var result = ValidatorHelper.ValidateRequest(dateTime, new GetIncomingTodosValidator());

            if (result.isSuccess.Equals(false))
            {
                _logger.LogError($"Response Error: {JsonSerializer.Serialize(result)}");
                return BadRequest(result);
            }
            
            var response = await _IToDoService.GetIncomingTodos();
            _logger.LogInformation($"GetIncomingTodos END: {JsonSerializer.Serialize(response)}");
            return Ok(response);
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
            _logger.LogInformation($"CreateTodo START: {JsonSerializer.Serialize(request)}");
            var result = ValidatorHelper.ValidateRequest(request, new CreateTodoValidator());

            if (result.isSuccess.Equals(false))
            {
                _logger.LogError($"Response Error: {JsonSerializer.Serialize(result)}");
                return result;
            }
            var response = await _IToDoService.CreateTodo(request);
            _logger.LogInformation($"CreateTodo END: {JsonSerializer.Serialize(response)}");
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
            _logger.LogInformation($"UpdateTodo START: {JsonSerializer.Serialize(request)}");
            var result = ValidatorHelper.ValidateRequest(request, new UpdateTodoValidator());

            if (result.isSuccess.Equals(false))
            {
                _logger.LogError($"Response Error: {JsonSerializer.Serialize(result)}");
                return result;
            }

            var response = await _IToDoService.UpdateTodo(request);
            _logger.LogInformation($"UpdateTodo END: {JsonSerializer.Serialize(response)}");
            return response;
        }

        /// <summary>
        /// Set todo percent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpPatch("{id}/percent")]
        public async Task<ApiResponse> SetTodoPercent(int id, int amount)
        {
            _logger.LogInformation($"SetTodoPercent START: id:{id},percent:{amount}");
            var result = ValidatorHelper.ValidateRequest(amount, new SetTodoPercentValidator());
            if (result.isSuccess.Equals(false))
            {
                _logger.LogError($"Response Error: {JsonSerializer.Serialize(result)}");
                return result;
            }
            var response = await _IToDoService.SetTodoPercent(id, amount);
            _logger.LogInformation($"SetTodoPercent END: {JsonSerializer.Serialize(response)}");
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
            _logger.LogInformation($"DeleteTodo START: id:{id}");
            var response = await _IToDoService.DeleteTodo(id);
            _logger.LogInformation($"DeleteTodo END: {JsonSerializer.Serialize(response)}");
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
            _logger.LogInformation($"MarkDoneTodo START: id:{id}");
            var response =  await _IToDoService.MarkDoneTodo(id);
            _logger.LogInformation($"MarkDoneTodo END: {JsonSerializer.Serialize(response)}");
            return response;
        }
    }
}