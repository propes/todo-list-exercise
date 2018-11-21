using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo_app.Models;

namespace todo_app.Repositories
{
    public interface ITodoListItemRepository
    {
        Task<IEnumerable<TodoListItem>> GetAllItemsAsync();
        Task<TodoListItem> AddItemAsync(TodoListItem item);
        Task<TodoListItem> UpdateItemAsync(TodoListItem item);
        Task DeleteItemAsync(TodoListItem item);
        Task DeleteAllAsync();
    }
}