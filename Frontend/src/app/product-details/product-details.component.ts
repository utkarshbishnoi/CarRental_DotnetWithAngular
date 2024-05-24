import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductsService } from '../services/products.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  productDetails:Product={
    id:0,
    maker: '',
    model: '',
    availableQuantity: 0,
    image: '',
    rentalPrice: 0,
    totalRented:0
    
  }
  itemid:any;
  model:any;
  constructor(private productservice: ProductsService,private router:Router,private route:ActivatedRoute){} 
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');
  
        if(id){
          this.itemid=id;
       this.productservice.getProduct(id)
       .subscribe({
        next:(response)=>{
          this.productDetails=response;
          console.log(this.productDetails);
        }
       })
        }
      }
    });

    this.productservice.getProduct(this.itemid).subscribe((data)=>{
      this.model=data;
   
    });

  }
}
