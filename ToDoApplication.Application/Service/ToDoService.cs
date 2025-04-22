using System.Net;
using ToDoApplication.Application.Service.ServiceHelpers;
using ToDoApplication.Common.Interfaces;
using ToDoApplication.Common.Models.Database;
using ToDoApplication.Common.Models.Domain.Request;
using ToDoApplication.Common.Models.Domain.Response;
using ToDoApplication.Common.Models.DTO;

namespace ToDoApplication.Application.Service
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepostiory _toDoRepository;

        public ToDoService(IToDoRepostiory toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        //zrobione ewentulanie dodać obsługe błedów
        public async Task<List<GetAllTodosResponse>> GetAllTodos()
        {
            var todos = await _toDoRepository.GetAllAsync();
            var responseList = new List<GetAllTodosResponse>();

            foreach (var todo in todos)
            {
                var createResponse = new GetAllTodosResponse
                {
                    Title = todo.Title,
                    Description = todo.Description,
                    ExpirationDate = todo.ExpirationDate,
                    CompletePercent = todo.CompletePercent,
                };
                responseList.Add(createResponse);
            }

            return responseList;
        }

        public async Task<GetTodoByIdResponse> GetTodoById(int id)
        {
            var todo = await _toDoRepository.GetTodoById(id);

            GetTodoByIdResponse response = new GetTodoByIdResponse
            {
                Title = todo.Title,
                Description = todo.Description,
                ExpirationDate = todo.ExpirationDate,
                CompletePercent = todo.CompletePercent,
            };

            return response;
        }

        public async Task<List<GetIncomingTodosResponse>> GetIncomingTodos()
        {
            var todos = await _toDoRepository.GetIncomingTodos();
            var responseList = new List<GetIncomingTodosResponse>();
            foreach (var todo in todos)
            {
                var createResponse = new GetIncomingTodosResponse
                {
                    Title = todo.Title,
                    Description = todo.Description,
                    ExpirationDate = todo.ExpirationDate,
                    CompletePercent = todo.CompletePercent,
                };
                responseList.Add(createResponse);
            }

            return responseList;
        }

        public async Task<ApiResponse> CreateTodo(CreateTodoRequest request)
        {
            // Empty Date Validation
            if (request.ExpirationDate == default(DateTime))
            {
                return ToDoHelpers.CreateResponse(false, "Expiration date cannot be empty", HttpStatusCode.BadRequest);
            }

            // Data in the future validation
            if (request.ExpirationDate <= DateTime.Now)
            {
                return ToDoHelpers.CreateResponse(false, "Expiration date must be greater than current date", HttpStatusCode.BadRequest);
            }

            // Too far date validation
            if (request.ExpirationDate > DateTime.Now.AddYears(1))
            {
                return ToDoHelpers.CreateResponse(false, "Expiration date cannot be more than one year in the future", HttpStatusCode.BadRequest);
            }

            // Weekend Validation :)
            if (request.ExpirationDate.DayOfWeek == DayOfWeek.Saturday || request.ExpirationDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return ToDoHelpers.CreateResponse(false, "Expiration date cannot be set to a weekend", HttpStatusCode.BadRequest);
            }

            // Working Hours validation
            TimeSpan workDayStart = new TimeSpan(8, 0, 0);
            TimeSpan workDayEnd = new TimeSpan(18, 0, 0);
            TimeSpan expirationTime = request.ExpirationDate.TimeOfDay;

            if (expirationTime < workDayStart || expirationTime > workDayEnd)
            {
                return ToDoHelpers.CreateResponse(false, "Expiration time must be between 8:00 AM and 6:00 PM", HttpStatusCode.BadRequest);
            }

            CreateTodoDTO createTodoDTO = new CreateTodoDTO
            {
                Title = request.Title,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                CompletePercent = request.CompletePercent,
            };

            var result = await _toDoRepository.CreateTodo(createTodoDTO);

            if (result)
            {
                return ToDoHelpers.CreateResponse(result, "Todo created successfully", HttpStatusCode.Created);
            }
            else
            {
                return ToDoHelpers.CreateResponse(result, "Failed to create todo", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse> UpdateTodo(UpdateTodoRequest request)
        {
            var todo = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
                ExpirationDate = request.ExpirationDate,
                CompletePercent = request.CompletePercent,
            };

            var response = await _toDoRepository.UpdateTodo(todo);

            if (response)
            {
                return ToDoHelpers.CreateResponse(response, "Todo updated successfully", HttpStatusCode.OK);
            }
            else
            {
                return ToDoHelpers.CreateResponse(response, "Failed to update todo", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ApiResponse> SetTodoPercent(int id)
        {
            var result = await _toDoRepository.SetTodoPercent(id);

            if(result)
            {
                return ToDoHelpers.CreateResponse(result, "Todo percent updated successfully", HttpStatusCode.OK);
            }
            else
            {
                return ToDoHelpers.CreateResponse(result, "Failed to update todo percent", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ApiResponse> DeleteTodo(int id)
        {
            var result =  await _toDoRepository.DeleteTodo(id);

            if (result)
            {
                return ToDoHelpers.CreateResponse(result, "Todo deleted successfully", HttpStatusCode.OK);
            }
            else
            {
                return ToDoHelpers.CreateResponse(result, "Failed to delete todo", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse> MarkDoneTodo(int id)
        {
            var result = await _toDoRepository.MarkTodoAsCompleted(id);
            if (result)
            {
                return ToDoHelpers.CreateResponse(result, "Todo marked as done successfully", HttpStatusCode.OK);
            }
            else
            {
                return ToDoHelpers.CreateResponse(result, "Failed to mark todo as done", HttpStatusCode.InternalServerError);
            }
        }
    }
}
