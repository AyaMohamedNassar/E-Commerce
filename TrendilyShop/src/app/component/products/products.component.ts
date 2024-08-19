import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from './product-card/product-card.component';
import { IProduct } from '../../Models/IProduct';
import { ProductService } from '../../Services/product.service';
import { map } from 'rxjs';
import { HeaderComponent } from '../header/header.component';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { CartService } from '../../Services/cartService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    ProductCardComponent,
    HeaderComponent,
    CommonModule,
    MatPaginatorModule,
  ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent implements OnInit {
  products: IProduct[] = [];
  page: number = 1;
  pageSize: number = 10;
  totalItems: number = 100;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadProducts();

    this.productService.getCount().subscribe({
      next: (response) => {
        this.totalItems = response;

        console.log(response);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {},
    });
  }

  loadProducts(): void {
    this.productService
      .getProducts(this.page, this.pageSize)
      .pipe(
        map((response: any) => {
          let allProducts = [];

          for (const product of response) {
            console.log(product);
            allProducts.push(product);
          }
          return allProducts;
        })
      )
      .subscribe({
        next: (values) => {
          this.products = [];

          values.forEach((item) => this.products.push(item));

          console.log(this.products);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });
  }

  onPageChange(event: PageEvent): void {
    console.log(
      'New page:',
      event.pageIndex + 1,
      'New page size:',
      event.pageSize
    );
    this.page = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadProducts();
  }


}
