import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategory } from '../Models/ICategory';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
 private baseUrl: string = 'http://localhost:5215/api/Category/';

  constructor(private http: HttpClient) {}

  getAll()
  {
    return this.http.get<ICategory>(this.baseUrl + 'All');
  }
}
