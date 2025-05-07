using System.Net;
using System.Net.Http.Json;
using Demo.Common.Tests.Functional;
using Demo.Common.WebApi;
using Demo.Todos.Tests.Faker;
using Demo.Todos.WebApi.Features.Todos.CreateTodo;
using Demo.Todos.WebApi.Features.Todos.GetTodo;
using Demo.Todos.WebApi.Features.Todos.ListTodos;
using Demo.Todos.WebApi.Features.Todos.UpdateTodo;
using FluentAssertions;

namespace Demo.Todos.Tests.Integration;

public class TodosControllerTests : BaseControllerTests
{
    public TodosControllerTests(FunctionalDatabaseFixture fixture) : base(fixture) { }

    [Fact(DisplayName = "POST /api/todos - Deve criar uma nova todo com sucesso")]
    public async Task CreateTodo_ShouldReturn_CreatedStatus()
    {
        var newTodo = TodoHandlerTestData.GenerateValidEntity();
        var request = new { Title = newTodo.Title };

        var response = await _client.PostAsJsonAsync("/api/todos", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateTodoResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "GET /api/todos - Deve retornar a lista de todos")]
    public async Task ListTodos_ShouldReturn_TodosList()
    {
        var newTodo = TodoHandlerTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/todos", new { Title = newTodo.Title });

        var response = await _client.GetAsync("/api/todos");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<ListTodosResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Data.Should().HaveCountGreaterThan(0);
    }

    [Fact(DisplayName = "GET /api/todos/{id} - Deve retornar a todo correta")]
    public async Task GetTodo_ShouldReturn_CorrectTodo()
    {
        var newTodo = TodoHandlerTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/todos", new { Title = newTodo.Title });

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateTodoResponse>>();
        var createdTodoId = createData.Data.Id;

        var response = await _client.GetAsync($"/api/todos/{createdTodoId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetTodoResponse>>();
        responseData.Should().NotBeNull();
        responseData.Data.Id.Should().Be(createdTodoId);
        responseData.Data.Title.Should().Be(newTodo.Title);
    }

    [Fact(DisplayName = "PUT /api/todos/{id} - Deve atualizar a todo corretamente")]
    public async Task UpdateTodo_ShouldUpdate_TodoSuccessfully()
    {
        var newTodo = TodoHandlerTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/todos", new { Title = newTodo.Title });

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateTodoResponse>>();
        var createdTodoId = createData.Data.Id;

        var updatedRequest = new { Title = "Updated Todo Title" };
        var updateResponse = await _client.PutAsJsonAsync($"/api/todos/{createdTodoId}", updatedRequest);

        updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var updateData = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateTodoResponse>>();
        updateData.Should().NotBeNull();
        updateData.Data.Id.Should().Be(createdTodoId);
        updateData.Data.Title.Should().Be("Updated Todo Title");
    }

    [Fact(DisplayName = "DELETE /api/todos/{id} - Deve remover a todo corretamente")]
    public async Task DeleteTodo_ShouldDelete_TodoSuccessfully()
    {
        var newTodo = TodoHandlerTestData.GenerateValidEntity();
        var createResponse = await _client.PostAsJsonAsync("/api/todos", new { Title = newTodo.Title });

        var createData = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateTodoResponse>>();
        var createdTodoId = createData.Data.Id;

        var deleteResponse = await _client.DeleteAsync($"/api/todos/{createdTodoId}");

        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getResponse = await _client.GetAsync($"/api/todos/{createdTodoId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
