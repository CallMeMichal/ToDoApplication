using Moq;
using System.Net;
using ToDoApplication.Common.Interfaces;
using ToDoApplication.Common.Models.Domain.Request;
using ToDoApplication.Common.Models.Domain.Response;

namespace ToDoApplication.Tests.Unit.Services
{
    public class ToDoServiceTests
    {
        private readonly Mock<IToDoService> _toDoServiceMock;

        public ToDoServiceTests()
        {
            _toDoServiceMock = new Mock<IToDoService>();
        }

        [Fact]
        public async Task GetAllTodos_ShouldReturnFilledList_WhenIsAnyTodo()
        {
            // Arrange
            var expected = new List<GetAllTodosResponse>
            {
                new GetAllTodosResponse
                {
                    Title = "Test Todo",
                    Description = "Test Description",
                    ExpirationDate = DateTime.Now.AddDays(1),
                    CompletePercent = 50
                }
            };

            _toDoServiceMock.Setup(service => service.GetAllTodos())
                .ReturnsAsync(expected);

            // Act
            var result = await _toDoServiceMock.Object.GetAllTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(expected[0].Title, result[0].Title);
        }

        [Fact]
        public async Task GetSpecifiedTodo_ShouldReturnSpecifiedTodo_WhenAnyTodo()
        {
            // Arrange
            int todoId = 1;
            var expectedResponse = new GetTodoByIdResponse
            {
                Title = "Test Title",
                Description = "Test Description",
                ExpirationDate = DateTime.Now.AddDays(1),
                CompletePercent = 50
            };

            _toDoServiceMock
                .Setup(service => service.GetTodoById(todoId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _toDoServiceMock.Object.GetTodoById(todoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Title, result.Title);
            Assert.Equal(expectedResponse.Description, result.Description);
            Assert.Equal(expectedResponse.ExpirationDate, result.ExpirationDate);
            Assert.Equal(expectedResponse.CompletePercent, result.CompletePercent);
        }

        [Fact]
        public async Task GetIncomingTodos_ShouldReturnListOfIncomingTodos_WhenAnyTodo()
        {
            // Arrange
            var mockIncomingTodos = new List<GetIncomingTodosResponse>
            {
                new GetIncomingTodosResponse
                {
                    Title = "Incoming Todo 1",
                    Description = "Description of Todo 1",
                    ExpirationDate = DateTime.Now.AddDays(1),
                    CompletePercent = 30
                },
                new GetIncomingTodosResponse
                {
                    Title = "Incoming Todo 2",
                    Description = "Description of Todo 2",
                    ExpirationDate = DateTime.Now.AddDays(2),
                    CompletePercent = 0
                }
            };

            _toDoServiceMock
                .Setup(service => service.GetIncomingTodos())
                .ReturnsAsync(mockIncomingTodos);

            // Act
            var result = await _toDoServiceMock.Object.GetIncomingTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Incoming Todo 1", result[0].Title);
            Assert.Equal("Incoming Todo 2", result[1].Title);
            Assert.Equal("Description of Todo 1", result[0].Description);
            Assert.Equal("Description of Todo 2", result[1].Description);
            Assert.Equal(mockIncomingTodos[0].ExpirationDate, result[0].ExpirationDate);
            Assert.Equal(mockIncomingTodos[1].ExpirationDate, result[1].ExpirationDate);
            Assert.Equal(30, result[0].CompletePercent);
            Assert.Equal(0, result[1].CompletePercent);
        }

        [Fact]
        public async Task CreateTodo_ShouldReturnSuccessResponse_WhenTodoIsCreatedSuccessfully()
        {
            // Arrange
            var createRequest = new CreateTodoRequest
            {
                Title = "New Todo",
                Description = "Description for new todo",
                ExpirationDate = DateTime.Now.AddDays(7),
                CompletePercent = 0
            };

            var expectedResponse = new ApiResponse
            {
                isSuccess = true,
                Message = "Todo created successfully",
                StatusCode = HttpStatusCode.Created
            };

            _toDoServiceMock
                .Setup(service => service.CreateTodo(createRequest))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _toDoServiceMock.Object.CreateTodo(createRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.Equal("Todo created successfully", result.Message);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
    }
}
