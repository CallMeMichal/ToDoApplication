using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using ToDoApplication.Common.Models.Domain.Request;
using ToDoApplication.Common.Models.Domain.Response;

namespace ToDoApplication.Tests.Tests.Integration.ToDo
{
    public class ToDoControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ToDoControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Test for GetAllTodos method Controller
        /// </summary>
        /// <returns>200 status code</returns>
        [Fact]
        public async Task GetAllTodos_ShouldReturn200StatusCode()
        {
            //arrange

            //act
            var response = await _client.GetAsync("/api/v1/todo");

            //assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }


        /// <summary>
        /// Test for GetTodoById method Controller
        /// </summary>
        /// <returns>true</returns>
        [Fact]
        public async Task CreateTodo_ShouldReturnTrue()
        {
            //arange 
            var todo = new CreateTodoRequest
            {
                Title = "Test Todo assa",
                Description = "Test Description",
                ExpirationDate = DateTime.Now.AddDays(1),
                CompletePercent = 0
            };

            HttpContent content = new StringContent(
                JsonSerializer.Serialize(todo),
                Encoding.UTF8,
                "application/json"
            );

            //act
            var response = await _client.PostAsync("/api/v1/todo", content);
            var resContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(resContent);



            //assert
            Assert.NotNull(apiResponse);
            Assert.True(apiResponse.isSuccess);

        }

    }
}
