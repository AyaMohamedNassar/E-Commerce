import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product.service';
import { IProduct } from '../../../Models/IProduct';
import { map } from 'rxjs';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-products-table',
  standalone: true,
  imports: [RouterLink, MatPaginatorModule],
  templateUrl: './products-table.component.html',
  styleUrl: './products-table.component.css',
})
export class ProductsTableComponent implements OnInit {
  products: IProduct[];
  page: number = 1;
  pageSize: number = 10;
  totalItems: number = 100;

  constructor(
    public productService: ProductService,
    public activatedRoute: ActivatedRoute,
    public router: Router
  ) {
    this.products = [];
  }

  ngOnInit(): void {
    this.loadProducts();
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

  deleteProduct(productId: any) {
    this.productService.deleteProduct(productId).subscribe({
      next: (response) => {
        console.log(response);
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
