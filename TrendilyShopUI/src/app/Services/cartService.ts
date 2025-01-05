import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  price: number;
  quantity: number;
  image:string;
}

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart = new BehaviorSubject<Product[]>(this.getCartFromLocalStorage());
  cart$ = this.cart.asObservable();

  addToCart(product: Product) {
    const currentCart = this.cart.value;
    const existingProductIndex = currentCart.findIndex(
      (p) => p.id === product.id
    );

    if (existingProductIndex > -1) {
      currentCart[existingProductIndex].quantity += 1;
    } else {
      currentCart.push({ ...product, quantity: 1 });
    }

    this.updateCart(currentCart);
  }

  updateQuantity(productId: number, quantity: number) {
    const currentCart = this.cart.value;
    const productIndex = currentCart.findIndex((p) => p.id === productId);

    if (productIndex > -1) {
      if (quantity <= 0) {
        currentCart.splice(productIndex, 1);
      } else {
        currentCart[productIndex].quantity = quantity;
      }
    }

    this.updateCart(currentCart);
  }

  private updateCart(cart: Product[]) {
    this.cart.next(cart);
    localStorage.setItem('cart', JSON.stringify(cart));
  }

  private getCartFromLocalStorage(): Product[] {
    const cart = localStorage.getItem('cart');
    return cart ? JSON.parse(cart) : [];
  }
}
