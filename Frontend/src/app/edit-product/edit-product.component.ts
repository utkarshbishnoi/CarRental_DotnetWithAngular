import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {

  productDetails: Product = {
    id:0,
    maker: '',
    model: '',
    availableQuantity: 0,
    image: '',
    rentalPrice: 0,
    totalRented:0
  }
  constructor(private route: ActivatedRoute,private toastr:ToastrService, private productservice: ProductsService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.productservice.getProduct(id)
            .subscribe({
              next: (response) => {
                this.productDetails = response;
              }
            })
        }
      }
    })
  }

  updateProduct(id:any) {
    console.log(this.productDetails.id)
    this.productservice.updateProduct(id,this.productDetails)
      .subscribe({
        next: (response) => {
          this.toastr.success("Car Edit successfully!");
          
          this.router.navigate(['/products']);
        },
        error: (error) => {
          this.toastr.error("Error updating product: " + error.message);
        }
      })
  }
  deleteProduct(id: any) {
    this.productservice.deleteProduct(id)
      .subscribe({
        
        next: (response) => {
          this.toastr.success("Car Deleted successfully!")

          this.router.navigate(['/products']);
        }
      })
  }
}
