import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Router } from '@angular/router';
import { UserLoginComponent } from '../user-login/user-login.component';
import { UserloginService } from '../services/userlogin.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isAdmin: boolean = false;
  userName: string = '';
  isloggedIn: boolean = false;

  constructor(private authService: UserloginService) {
     this.isloggedIn = this.authService.isloggedIn;
     this.isAdmin = this.authService.isAdmin;
  }
  ngOnInit(): void {}
  logout(){
    this.authService.logout();
    console.log("logout successfully")
  }
}
