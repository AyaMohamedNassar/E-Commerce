import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RegisteredUser } from './../../Models/IRegisteredUser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../Services/authentication.service';

@Component({
  selector: 'app-registeration',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './registeration.component.html',
  styleUrls: ['./registeration.component.css', './style.scss'],
})
export class RegisterationComponent {
  registrationForm = new FormGroup({
    userName: new FormControl(null, Validators.required),
    email: new FormControl(null, Validators.required),
    phone: new FormControl(null),
    password: new FormControl(null, Validators.required),
    confirmPassword: new FormControl(null, Validators.required),
  });

  errorMessage: string | null = null;

  constructor(
    public AuthenticationService: AuthenticationService,
    public router: Router,
    public activatedRoute: ActivatedRoute
  ) {}

  register() {

    if (this.registrationForm.valid) {
      const user: RegisteredUser = {
        UserName: `${this.registrationForm.value.userName}`,
        Password: `${this.registrationForm.value.password}`,
        ConfirmPassword: `${this.registrationForm.value.confirmPassword}`,
        Email: `${this.registrationForm.value.email}`,
        PhoneNumber: `${this.registrationForm.value.phone}`,
      };

      this.AuthenticationService.register(user).subscribe({
        next: (res) => {
          this.router.navigate(['/']);
        },
        error: (error) => {
          this.setErrorMessage(error);
        },
        complete: () => {},
      });
    }else{
      this.setErrorMessage("Compele your data.");
    }
  }

  private setErrorMessage(err: string) {
    this.errorMessage = err;

    setTimeout(() => {
      this.errorMessage = null;
    }, 3000);
  }
}
