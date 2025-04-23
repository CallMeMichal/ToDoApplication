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
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

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
        public async Task<ApiResponse> SetTodoPercent(int id, int amount)
        {
            var result = await _toDoRepository.SetTodoPercent(id,amount);

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
