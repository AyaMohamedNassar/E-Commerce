import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductService } from '../../../Services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../../Models/IProduct';
import { ICategory } from '../../../Models/ICategory';
import { IBrand } from '../../../Models/Ibrand';
import { CategoryService } from '../../../Services/category.service';
import { BrandService } from '../../../Services/brand.service';
import { map } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css',
})
export class ProductFormComponent implements OnInit {
  productForm = new FormGroup({
    name: new FormControl(null),
    description: new FormControl(null),
    image: new FormControl(null),
    price: new FormControl(null),
    Instock: new FormControl(null),
    category: new FormControl(null),
    brand: new FormControl(null),
  });

  categories: ICategory[] = [];
  brands: IBrand[] = [];

  selectedFile: File | null = null;
  productId: any;
  product?: IProduct;

  constructor(
    public productService: ProductService,
    public categoryService: CategoryService,
    public brandService: BrandService,
    public activatedRoute: ActivatedRoute,
    public router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.productId = params['id'];
      },
    });

    if (this.productId != 0) {
      this.productService.getById(this.productId).subscribe({
        next: (res) => {
          this.product = res;
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });
    }

    this.brandService
      .getAll()
      .pipe(
        map((response: any) => {
          let allbrands = [];

          for (const brand of response) {
            allbrands.push(brand);
          }
          return allbrands;
        })
      )
      .subscribe({
        next: (values) => {
          values.forEach((item) => this.brands.push(item));
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });

    this.categoryService
      .getAll()
      .pipe(
        map((response: any) => {
          let allcategoies = [];

          for (const category of response) {
            allcategoies.push(category);
          }
          return allcategoies;
        })
      )
      .subscribe({
        next: (values) => {
          values.forEach((item) => this.categories.push(item));
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });
  }

  ProductHandler() {
    const formData = new FormData();
    formData.append('id', `${this.productId}`);
    formData.append('Name', `${this.productForm.value.name}`);
    formData.append('Description', `${this.productForm.value.description}`);
    formData.append('Price', `${this.productForm.value.price}`);
    formData.append('StockQuantity', `${this.productForm.value.Instock}`);
    formData.append('CategoryId', `${this.productForm.value.category}`);
    formData.append('BrandId', `${this.productForm.value.brand}`);
    if (this.selectedFile) {
      formData.append('Image', this.selectedFile, this.selectedFile.name);

      if (this.productId == 0) {
        this.productService.addProduct(formData).subscribe({
          next: (values) => {
            this.router.navigate(['/products']);
          },
          error: (error) => {
            console.log(error);
          },
          complete: () => {},
        });
      } else {
        this.productService.editProductPhoto(formData).subscribe({
          next: (values) => {
            console.log(values);
            this.router.navigate(['/products']);
          },
          error: (error) => {
            console.log(error);
          },
          complete: () => {},
        });
      }
    } else {
      let url = this.product?.image;
      let lastPart = url?.split('/').pop();
      console.log(lastPart);

      formData.append('Image', `${lastPart}`);

      this.productService.editProduct(formData).subscribe({
        next: (values) => {
          console.log(values);
          this.router.navigate(['/products']);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {},
      });
    }
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
}
