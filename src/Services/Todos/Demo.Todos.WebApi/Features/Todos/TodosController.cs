using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using Demo.Common.WebApi;
using Demo.Todos.Application.Todos.CreateTodo;
using Demo.Todos.Application.Todos.DeleteTodo;
using Demo.Todos.Application.Todos.GetTodo;
using Demo.Todos.Application.Todos.ListTodos;
using Demo.Todos.Application.Todos.UpdateTodo;
using Demo.Todos.WebApi.Features.Todos.CreateTodo;
using Demo.Todos.WebApi.Features.Todos.DeleteTodo;
using Demo.Todos.WebApi.Features.Todos.GetTodo;
using Demo.Todos.WebApi.Features.Todos.ListTodos;
using Demo.Todos.WebApi.Features.Todos.UpdateTodo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Todos.WebApi.Features.Todos
{
    /// <summary>
    /// Controller for managing todo operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of TodosController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public TodosController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of Todos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<ListTodosResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListTodos(ApiQueryRequestPresentation request, CancellationToken cancellationToken)
        {
            var validator = new ListTodosRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<ListTodosCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<ListTodosResponse>(response));
        }

        /// <summary>
        /// Creates a new todo
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateTodoResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateTodoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateTodoCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<CreateTodoResponse>(response));
        }

        /// <summary>
        /// Retrieves a todo by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetTodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodo([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetTodoRequest { Id = id };
            var validator = new GetTodoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetTodoCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<GetTodoResponse>(response));
        }

        /// <summary>
        /// Updates a todo by ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateTodoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodo([FromRoute] int id, [FromBody] UpdateTodoRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            var validator = new UpdateTodoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateTodoCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<UpdateTodoResponse>(response));
        }

        /// <summary>
        /// Deletes a todo by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new DeleteTodoRequest { Id = id };
            var validator = new DeleteTodoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteTodoCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(true);
        }
    }
}