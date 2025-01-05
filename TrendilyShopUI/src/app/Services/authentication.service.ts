import { Injectable } from '@angular/core';
import { IUser } from '../Models/IUser';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { User } from '../Models/User';
import { RegisteredUser } from '../Models/IRegisteredUser';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  User = new BehaviorSubject<IUser | null>(null);

  baseUrl: string = 'http://localhost:5215/api/Account';
  constructor(private http: HttpClient) {}
  loggedIn = false;
  userLogin(Email: string, password: string) {
    const body = {
      email: Email,
      password: password,
    };

    return this.http.post<IUser>(`${this.baseUrl}/login`, body).pipe(
      catchError(this.handleError),
      tap((res) => {
        const user = new User(res.email, res.userName, res.token, res.role);

        this.User.next(user);
      })
    );
  }

  register(user: RegisteredUser) {
    return this.http.post<IUser>(`${this.baseUrl}/Register`, user).pipe(
      catchError(this.handleError),
      tap((res) => {
        const user = new User(res.email, res.userName, res.token, res.role);

        this.User.next(user);
      })
    );
  }

  signOut() {
    return this.http.post(`${this.baseUrl}/LogOut`, null).pipe(
      catchError(this.handleError),
      tap((res) => {
        this.User.next(null);
      })
    );
  }

  private handleError(err: any) {
    console.log(err);
    let errorMessage = err.error.message;

    return throwError(() => errorMessage);
  }
}
