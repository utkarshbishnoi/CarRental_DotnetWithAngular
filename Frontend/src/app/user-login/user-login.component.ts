import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserloginService } from '../services/userlogin.service';
import { ProductsService } from '../services/products.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css'],
})
export class UserLoginComponent {
  cachedData: any;
  errorMessage: string = '';

  constructor(
    private router: Router,
    private toastr: ToastrService,
    private authService: UserloginService,
    private productservice: ProductsService
  ) {}

  login(data: any): void {
    this.authService.login(data.email, data.password).subscribe(
      (returnData) => {
        this.cachedData = returnData;

        if (this.cachedData.token) {
          this.authService.username = this.cachedData.Email;
          this.authService.isloggedIn = true;
          this.productservice.isloggedIn = true;
          localStorage.setItem('userInfo', JSON.stringify(this.cachedData));
          if (this.cachedData.role === 'Admin') {
            this.productservice.isAdmin = true;
            this.authService.isAdmin = true;
          }
          this.router.navigate(['/products']);
          this.toastr.success('logged In successfully!');
        }
      },
      (error) => {
        this.errorMessage = 'Invalid username or password';
      }
    );
  }
  clearErrorMessage(): void {
    this.errorMessage = '';
  }
  ngOnInit(): void {}
}
