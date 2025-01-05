import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product.service';
import { IProduct } from '../../../Models/IProduct';
import { CartService, Product } from '../../../Services/cartService';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shopping-cart.component.html',
  styleUrl: './shopping-cart.component.css',
})
export class ShoppingCartComponent implements OnInit {
  cart: Product[] = [];
  total: number = 0;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.cart$.subscribe((cart) => {
      this.cart = cart;
      this.calculateTotal();
    });
  }

  calculateTotal() {
    this.total = this.cart.reduce(
      (acc, product) => acc + product.price * product.quantity,
      0
    );
  }

  updateQuantity(productId: number, quantity: number) {
    this.cartService.updateQuantity(productId, quantity);
  }
}
