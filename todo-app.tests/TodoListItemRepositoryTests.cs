using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetAllItems_ReturnsAllItems()
        {
            var items = _repository.GetAllItems();

            Assert.AreEqual(2, items.Count());
        }

        [Test]
        public void AddItem_AddsItemToList()
        {
            _repository.AddItem(new TodoListItem());

            Assert.AreEqual(3, _items.Count());
        }

        [Test]
        public void AddItem_AssignsIdToItem()
        {
            var item = new TodoListItem();

            var newItem = _repository.AddItem(item);

            Assert.IsNotNull(newItem.Id);
        }

        [Test]
        public void UpdateItem_ThrowsExceptionIfItemCannotBeFound()
        {
            var item = new TodoListItem
            {
                Id = new Guid("99999999999999999999999999999999")
            };
            
            Assert.Throws<InvalidOperationException>(() =>_repository.UpdateItem(item));
        }

        [Test]
        public void UpdateItem_UpdatesDescription()
        {
            var item = new TodoListItem
            {
                Id = new Guid("00000000000000000000000000000001"),
                Description = "Feed the fish"
            };

            var updatedItem = _repository.UpdateItem(item);

            Assert.AreEqual("Feed the fish", updatedItem.Description);
        }

        [Test]
        public void UpdateItem_UpdatesIsCompleted()
        {
            var item = new TodoListItem
            {
                Id = new Guid("00000000000000000000000000000001"),
                IsCompleted = true
            };

            var updatedItem = _repository.UpdateItem(item);

            Assert.IsTrue(updatedItem.IsCompleted);
        }
    }
}