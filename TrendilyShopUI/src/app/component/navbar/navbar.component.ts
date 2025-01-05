import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
  RouterLink,
  RouterLinkActive,
} from '@angular/router';
import { AuthenticationService } from '../../Services/authentication.service';
import { Subscription, exhaustMap, of, take } from 'rxjs';
import { CartService } from '../../Services/cartService';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent implements OnInit, OnDestroy {
  constructor(
    public authenticationService: AuthenticationService,
    private cartService: CartService,
    public router: Router,
    public activatedRoute: ActivatedRoute
  ) {}

  itemCount: number = 0;
  logged = false;
  admin = false;
  private userSubscription: Subscription = new Subscription();

  ngOnInit() {
    this.cartService.cart$.subscribe((cart) => {
      this.itemCount = cart.reduce((acc, product) => acc + product.quantity, 0);
    });

    this.userSubscription = this.authenticationService.User.subscribe(
      (user) => {
        this.logged = !!user;
        if (this.logged) {
          this.admin = user?.role == 'Admin';
          console.log(this.admin);
        } else {
          this.admin = false;
        }
      }
    );
  }

  ngOnDestroy() {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }

  signOut() {
    this.authenticationService.signOut().subscribe({
      next: (res) => {
        this.authenticationService.loggedIn = false;
        this.router.navigate(['/']);
      },
      error: (error) => {
        console.log(error);
      },
      complete: () => {},
    });
  }
}
