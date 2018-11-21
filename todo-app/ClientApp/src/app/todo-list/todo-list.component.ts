import { Component } from '@angular/core';
import { TodoListItem } from './todo-list-item.model';
import { TodoListService } from './todo-list.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'todo-list-component',
  templateUrl: './todo-list.component.html',
  styleUrls: [ './todo-list.component.css' ]
})
export class TodoListComponent {
  listItems: TodoListItem[];
  newItemDescription: string;

  constructor(private todoListService: TodoListService) {
    this.getAllItems()
  }

  getAllItems() {
    this.todoListService.getItems().pipe(first()).subscribe(items =>
      this.listItems = items
    );
  };

  addItem() {
    const item = <TodoListItem>{
      description: this.newItemDescription,
      isCompleted: false,
      isDeleted: false
    };

    this.todoListService.addItem(item).pipe(first()).subscribe(item => {
      this.listItems.push(item);
      this.newItemDescription = '';
    });
  }

  updateItem(item: TodoListItem) {
    this.todoListService.updateItem(item).pipe(first()).subscribe();
  }

  deleteItem(item: TodoListItem, index: number) {
    this.todoListService.deleteItem(item.id).pipe(first()).subscribe(() =>
      this.listItems.splice(index, 1)
    );
  }

  markAllAsComplete() {
    this.todoListService.markAllAsComplete().pipe(first()).subscribe(() =>
        this.getAllItems()
      );
  }

  deleteAll() {
    this.todoListService.deleteAll().pipe(first()).subscribe(() =>
      this.getAllItems()
    );
  }
}
