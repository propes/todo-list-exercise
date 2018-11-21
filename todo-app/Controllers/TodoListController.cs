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
        public IEnumerable<TodoListItem> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        [HttpPost()]
        public TodoListItem AddItem(TodoListItem item)
        {
            return _repository.AddItem(item);
        }

        [HttpPut()]
        public TodoListItem UpdateItem(TodoListItem item)
        {
            return _repository.UpdateItem(item);
        }

        [HttpDelete()]
        public void DeleteItem(Guid id)
        {
            _repository.DeleteItem(new TodoListItem { Id = id });
        }
    }
}
