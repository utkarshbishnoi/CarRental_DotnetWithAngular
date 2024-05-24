import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { Observable } from 'rxjs/internal/Observable';
import {  Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  apiurl = 'http://localhost:5000/api/v1/car';
  user: any;
  userid: string = '';
  Role: string = '';
  isloggedIn: boolean = false;
  isAdmin: boolean = false;
  

  constructor(private http: HttpClient,private router: Router) {
    this.user = localStorage.getItem('userInfo');
    if(this.user){
    this.user = JSON.parse(this.user);
    console.log(this.user.token);
    this.router.navigate(['/products']);
  }
  else
  {
    this.router.navigate(['/user-login']);
  }}

  getProducts(): Observable<any> {
    return this.http.get<any>(`${this.apiurl}/cars`);
  }
  getCarsByMaker(maker: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiurl}/cars/maker/${maker}`);
  }

  addProduct(product: any): Observable<any> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.post<Product>(
      `${this.apiurl}/create`,
      product,{
        headers,
      }
      
    );
  }

  getProduct(id: any): Observable<Product> {
    return this.http.get<Product>(`${this.apiurl}/${id}`);
  }

  updateProduct(id: any, product: Product): Observable<Product> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.put<Product>(`${this.apiurl}/update/${id}`, product,{headers});
  }

  deleteProduct(id: any): Observable<Product> {
    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.user.token}`);
    return this.http.delete<Product>(`${this.apiurl}/delete/${id}`,{headers});
  }
}
