import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../Models/Ibrand';

@Injectable({
  providedIn: 'root',
})
export class BrandService {
 private baseUrl: string = 'http://localhost:5215/api/Brand/';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<IBrand>(this.baseUrl + 'All');
  }
}
