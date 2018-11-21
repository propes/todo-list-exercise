import { Injectable } from '@angular/core';
import { TodoListItem } from './todo-list-item.model';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TodoListService
{
    private baseUrl: string = 'api/todo-list-items';

    constructor(private http: HttpClient) {
    }

    getItems(): Observable<TodoListItem[]> {
        return this.http.get<TodoListItem[]>(this.baseUrl);
    }

    addItem(item: TodoListItem): Observable<TodoListItem> {
        return this.http.post<TodoListItem>(this.baseUrl, item);
    }

    updateItem(item: TodoListItem): Observable<TodoListItem> {
        return this.http.put<TodoListItem>(this.baseUrl, item);
    }

    deleteItem(id: string): Observable<any> {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }

    markAllAsComplete(): Observable<any> {
        return this.http.put(`${this.baseUrl}/complete/all`, null);
    }

    deleteAll(): Observable<any> {
        return this.http.delete(`${this.baseUrl}/all`);
    }
}
