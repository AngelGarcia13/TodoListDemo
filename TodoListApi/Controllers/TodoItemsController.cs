using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Models.Entities;
using TodoListApi.Models.Inputs;
using TodoListApi.Models.Data;
using TodoListApi.Services;
using TodoListApi.Models.Exceptions;

namespace TodoListApi.Controllers
{
    /// <summary>
    /// API controller for managing TodoItems.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;

        public TodoItemsController(ITodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
        }

        /// <summary>
        /// Retrieves all TodoItems.
        /// </summary>
        /// <returns>A list of TodoItems.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoItem>), 200)]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return _todoItemsService.GetTodoItems().ToList();
        }

        /// <summary>
        /// Retrieves a specific TodoItem by ID.
        /// </summary>
        /// <param name="id">The ID of the TodoItem.</param>
        /// <returns>The TodoItem with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItem), 200)]
        [ProducesResponseType(404)]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            try
            {
                return _todoItemsService.GetTodoItem(id);
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new TodoItem.
        /// </summary>
        /// <param name="todoItem">The TodoItem to create.</param>
        /// <returns>The created TodoItem.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItem), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public ActionResult<TodoItem> CreateTodoItem(TodoItemCreateRequest todoItem)
        {
            try
            {
                var newToDoItem = _todoItemsService.CreateTodoItem(todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = newToDoItem.Id - 1}, newToDoItem);
            }
            catch (RequiredFieldsException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates an existing TodoItem.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to update.</param>
        /// <param name="todoItem">The updated TodoItem data.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTodoItem(int id, TodoItemUpdateRequest todoItem)
        {
            try
            {
                _todoItemsService.UpdateTodoItem(id, todoItem);
                return NoContent();
            }
            catch (Exception ex) when (ex is FieldsDoNotMatchException || ex is RequiredFieldsException)
            {
                return BadRequest(ex.Message);
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Deletes a TodoItem by ID.
        /// </summary>
        /// <param name="id">The ID of the TodoItem to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTodoItem(int id)
        {
            try
            {
                _todoItemsService.DeleteTodoItem(id);
                return NoContent();
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
