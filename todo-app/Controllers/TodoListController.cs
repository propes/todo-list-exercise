using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo_app.Models;
using todo_app.Repositories;

namespace todo_app.Controllers
{
    [Route("api/todo-list-items")]
    public class TodoListController : Controller
    {
        private readonly ITodoListItemRepository _repository;

        public TodoListController(ITodoListItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public Task<IEnumerable<TodoListItem>> GetAllItemsAsync()
        {
            return _repository.GetAllItemsAsync();
        }

        [HttpPost()]
        public Task<TodoListItem> AddItemAsync([FromBody] TodoListItem item)
        {
            return _repository.AddItemAsync(item);
        }

        [HttpPut()]
        public Task<TodoListItem> UpdateItemAsync([FromBody] TodoListItem item)
        {
            return _repository.UpdateItemAsync(item);
        }

        [HttpDelete("{id}")]
        public Task DeleteItemAsync(Guid id)
        {
            return _repository.DeleteItemAsync(new TodoListItem { Id = id });
        }

        [HttpPut("complete/all")]
        public async Task MarkAllItemsCompleted()
        {
            var items = await _repository.GetAllItemsAsync();

            items.ToList().ForEach(async item =>
            {
                item.IsCompleted = true;
                await _repository.UpdateItemAsync(item);
            });
        }

        [HttpDelete("all")]
        public Task DeleteAllItems()
        {
            return _repository.DeleteAllAsync();
        }
    }
}
