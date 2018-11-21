using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo_app.Models;

namespace todo_app.Repositories
{
    public class TodoListItemRepository : ITodoListItemRepository
    {
        // For this exercise I am using an in-memory list as the data source to
        // avoid the complication of having to connect to a database.
        private readonly List<TodoListItem> _items;

        public TodoListItemRepository()
        {
            _items = new List<TodoListItem>
            {
                new TodoListItem
                {
                    Id = new Guid("00000000000000000000000000000001"),
                    Description = "Walk the dog",
                    IsCompleted = false,
                    IsDeleted = false
                },
                new TodoListItem
                {
                    Id = new Guid("00000000000000000000000000000002"),
                    Description = "Wash the cat",
                    IsCompleted = false,
                    IsDeleted = false
                }
            };
        }

        public TodoListItemRepository(List<TodoListItem> items)
        {
            _items = items;
        }

        public Task<IEnumerable<TodoListItem>> GetAllItemsAsync()
        {
            return Task.FromResult<IEnumerable<TodoListItem>>(_items);
        }

        public Task<TodoListItem> AddItemAsync(TodoListItem item)
        {
            item.Id = Guid.NewGuid();
            item.IsCompleted = false;
            item.IsDeleted = false;

            _items.Add(item);

            return Task.FromResult(item);
        }

        public Task<TodoListItem> UpdateItemAsync(TodoListItem item)
        {
            var itemToUpdate = _items.Find(i => i.Id == item.Id);

            if (itemToUpdate == null) throw new InvalidOperationException("Item could not be found");

            // Simple mapping. This would normally be handled by a separate mappper.
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsCompleted = item.IsCompleted;

            return Task.FromResult(itemToUpdate);
        }

        public Task DeleteItemAsync(TodoListItem item)
        {
            var indexToDelete = _items.FindIndex(i => i.Id == item.Id);

            if (indexToDelete < 0) throw new InvalidOperationException("Item could not be found");

            return Task.Run(() =>_items.RemoveAt(indexToDelete));
        }

        public Task DeleteAllAsync()
        {
            return Task.Run(() =>_items.Clear());
        }
    }
}