using System;
using System.Collections.Generic;
using todo_app.Models;

namespace todo_app.Repositories
{
    public interface ITodoListItemRepository
    {
        IEnumerable<TodoListItem> GetAllItems();
        TodoListItem AddItem(TodoListItem item);
        TodoListItem UpdateItem(TodoListItem item);
        void DeleteItem(TodoListItem item);
        void DeleteAll();
    }
}