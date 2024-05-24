import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductsService } from '../services/products.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit {
  constructor(
    private productservice: ProductsService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  product: Product = {
    id: 0,
    maker: '',
    model: '',
    image: '',
    rentalPrice: 0,
    availableQuantity: 0,
    totalRented: 0,
  };
  ngOnInit(): void {}
  onSubmit() {
    this.productservice.addProduct(this.product).subscribe((product) => {
        console.log('Product:', product);
        this.toastr.success('Car added successfully!');
        this.router.navigate(['/products']);
      },
      (error) => {
        console.log('An error occurred:', error);
      },
    );
  }
}
