import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthenticationService } from '../../Services/authentication.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required),
  });

  errorMessage: string | null = null;

  constructor(
    public authenticationService: AuthenticationService,
    public router: Router,
    public activatedRoute: ActivatedRoute
  ) {}

  login() {
    if (this.loginForm.valid) {
      this.authenticationService.userLogin(
        `${this.loginForm.value.email}`,
        `${this.loginForm.value.password}`
      ).subscribe({
        next: (res) => {
          this.authenticationService.loggedIn = true;
          this.router.navigate(['/']);
          // console.log(this.AuthenticationService.User);
        },
        error: (error) => {
          this.setErrorMessage(error);
        },
        complete: () => {},
      });
    }
  }

  private setErrorMessage(err: string) {
    this.errorMessage = err;

    setTimeout(() => {
      this.errorMessage = null;
    }, 3000);
  }
}
