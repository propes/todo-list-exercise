using System;
using System.Collections.Generic;
using todo_app.Models;

namespace todo_app.Repositories
{
    public class TodoListItemRepository : ITodoListItemRepository
    {
        // For this exercise I am using an in-memory list as the data source to
        // avoid the complication of having to connect to a database.
        private readonly List<TodoListItem> _items;

        public TodoListItemRepository(List<TodoListItem> items)
        {
            _items = items;
        }

        public IEnumerable<TodoListItem> GetAllItems()
        {
            return _items;
        }

        public TodoListItem AddItem(TodoListItem item)
        {
            item.Id = Guid.NewGuid();
            _items.Add(item);

            return item;
        }

        public TodoListItem UpdateItem(TodoListItem item)
        {
            var itemToUpdate = _items.Find(i => i.Id == item.Id);

            if (itemToUpdate == null) throw new InvalidOperationException("Item could not be found");

            // Simple mapping. This would normally be handled by a separate mappper.
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsCompleted = item.IsCompleted;

            return itemToUpdate;
        }

        public void DeleteItem(TodoListItem item)
        {
            var indexToDelete = _items.FindIndex(i => i.Id == item.Id);

            if (indexToDelete < 0) throw new InvalidOperationException("Item could not be found");

            _items.RemoveAt(indexToDelete);
        }
    }
}