import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from '../../../Models/IProduct';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../Services/product.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css',
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;
  productId: any;
  constructor(
    public productService: ProductService,
    public activatedRoute: ActivatedRoute,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.productId = this.activatedRoute.snapshot.params['id'];
    this.productService.getById(this.productId).subscribe({
      next: (res) => {
        console.log(res);
        this.product = res as IProduct;
      },
      error: (error) => {},
    });
  }
}
