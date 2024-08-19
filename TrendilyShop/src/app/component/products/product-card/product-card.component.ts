import { Component, Input } from '@angular/core';
import { IProduct } from '../../../Models/IProduct';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CartService } from '../../../Services/cartService';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css',
})
export class ProductCardComponent {
  constructor(private cartService: CartService, private router: Router) {}
  @Input() product: IProduct = {
    id: 0,
    name: '',
    description: '',
    image: '',
    price: 0,
    stockQuantity: 0,
    category: '',
    brand: '',
    categoryId: 0,
    brandId: 0
  };

  addToCart(product: any) {
    this.cartService.addToCart(product);
    this.router.navigate(['products/shoppingCart']);
  }
}
