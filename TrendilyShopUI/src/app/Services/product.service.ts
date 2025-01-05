import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProduct } from '../Models/IProduct';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl: string = 'http://localhost:5215/api/Product/';

  constructor(private http: HttpClient) {}

  // getProducts() {
  //   return this.http.get(this.baseUrl + 'All');
  // }

  getProducts(page: number, pageSize: number): Observable<IProduct[]> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<IProduct[]>(this.baseUrl + `All`, { params });
  }

  getById(productId: any) {
    return this.http.get<IProduct>(this.baseUrl + productId);
  }

  addProduct(product: any) {
    return this.http.post(this.baseUrl + 'Add', product);
  }

  editProductPhoto(product: any) {
    return this.http.post(this.baseUrl + 'EditProductPhoto', product);
  }

  editProduct(product: any) {
    console.log(product);
    return this.http.post(this.baseUrl + 'Edit', product);
  }

  deleteProduct(productId: number): Observable<any> {
    return this.http.delete(this.baseUrl + 'Delete/' + productId);
  }

  getCount(): Observable<number> {
    const url = `${this.baseUrl}Count`;
    return this.http.get<number>(url);
  }
}
