using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_app.Models;
using todo_app.Repositories;

namespace todo_app.Tests
{
    public class TodoListItemTests
    {
        private List<TodoListItem> _items;
        
        private TodoListItemRepository _repository;

        [SetUp]
        public void SetUp()
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

            _repository = new TodoListItemRepository(_items);
        }

        [Test]
        public async Task GetAllItems_ReturnsAllItems()
        {
            var items = await _repository.GetAllItemsAsync();

            Assert.AreEqual(2, items.Count());
        }

        [Test]
        public async Task AddItem_AddsItemToList()
        {
            await _repository.AddItemAsync(new TodoListItem());

            Assert.AreEqual(3, _items.Count());
        }

        [Test]
        public async Task AddItem_AssignsIdToItem()
        {
            var item = new TodoListItem();

            var newItem = await _repository.AddItemAsync(item);

            Assert.IsNotNull(newItem.Id);
        }

        [Test]
        public void UpdateItem_ThrowsExceptionIfItemCannotBeFound()
        {
            var item = new TodoListItem
            {
                Id = new Guid("99999999999999999999999999999999")
            };
            
            Assert.Throws<InvalidOperationException>(() => _repository.UpdateItemAsync(item));
        }

        [Test]
        public async Task UpdateItem_UpdatesDescription()
        {
            var item = new TodoListItem
            {
                Id = new Guid("00000000000000000000000000000001"),
                Description = "Feed the fish"
            };

            var updatedItem = await _repository.UpdateItemAsync(item);

            Assert.AreEqual("Feed the fish", updatedItem.Description);
        }

        [Test]
        public async Task UpdateItem_UpdatesIsCompleted()
        {
            var item = new TodoListItem
            {
                Id = new Guid("00000000000000000000000000000001"),
                IsCompleted = true
            };

            var updatedItem = await _repository.UpdateItemAsync(item);

            Assert.IsTrue(updatedItem.IsCompleted);
        }
    }
}