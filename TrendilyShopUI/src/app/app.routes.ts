import { Routes } from '@angular/router';
import { ProductsComponent } from './component/products/products.component';
import { ProductsTableComponent } from './component/products/products-table/products-table.component';
import { ProductDetailsComponent } from './component/products/product-details/product-details.component';
import { NotFoundComponent } from './component/not-found/not-found.component';
import { LoginComponent } from './component/login/login.component';
import { RegisterationComponent } from './component/registeration/registeration.component';
import { ProductFormComponent } from './component/products/product-form/product-form.component';
import { ShoppingCartComponent } from './component/products/shopping-cart/shopping-cart.component';

export const routes: Routes = [
  { path: '', component: ProductsComponent },
  { path: 'signUp', component: RegisterationComponent },
  { path: 'login', component: LoginComponent },
  { path: 'products', component: ProductsTableComponent },
  { path: 'products/shoppingCart', component: ShoppingCartComponent },
  { path: 'products/:id', component: ProductDetailsComponent },
  { path: 'products/:id/edit', component: ProductFormComponent },
  { path: 'products/:id/add', component: ProductFormComponent },
  { path: '**', component: NotFoundComponent },
];
