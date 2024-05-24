import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserloginService {
  private apiUrl = 'http://localhost:5000/api/v1/account/login';
  user: any;
  username: string = '';
  isloggedIn: boolean = false;
  isAdmin: boolean = false;
  role: string = '';
  constructor(private http: HttpClient) {
    this.user = localStorage.getItem('userInfo');
    this.user = JSON.parse(this.user);
    if (this.user) {
      this.username = this.user.email;
      this.isloggedIn = true;
      this.role = this.user.role;
      if (this.role === 'Admin') {
        this.isAdmin = true;
      }
    }
  }

  // Login user and store the token
  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    return this.http.post<any>(this.apiUrl, loginData).pipe(
      tap((returnData) => {
        if (returnData.token) {
          this.username = returnData.email;
          this.isloggedIn = true;
          this.role = returnData.role;
          if (this.role === 'Admin') {
            this.isAdmin = true;
          }
          localStorage.setItem('userInfo', JSON.stringify(returnData));
          console.log(this.isAdmin);
          console.log(returnData);
        }
      })
    );
  }
  logout(): void {
    localStorage.removeItem('userInfo');
    this.user = null;
    this.username = '';
    this.isloggedIn = false;
    this.isAdmin = false;
    this.role = '';
  }
}
