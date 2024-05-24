import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from '../services/products.service';
import { Router } from '@angular/router';
import { UserloginService } from '../services/userlogin.service';
import { AgreementService } from '../services/agreement.service';
import { RentalAgreement } from '../models/rentalagreement.model';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
})
export class ProductsListComponent implements OnInit {
  products: Product[] = [];
  cartItems: any;
  cartUser: string = '';
  userName: string = '';
  userRole: string = '';
  isloggedIn: boolean = false;
  isAdmin: boolean = false;

  searchInput: string = '';
  currentPage: number = 1;
  pageSize: number = 3;
  totalPages: number = 0;
  selectedCategory: string = 'All';
  productNameSearch: string = '';
  filteredProducts: Product[] = [];

  constructor(
    private productservice: ProductsService,
    private router: Router,
    private userloginservice: UserloginService,
    private agreement: AgreementService
  ) {
    this.isAdmin = this.userloginservice.isAdmin;
    this.isloggedIn = this.userloginservice.isloggedIn;
  }
  productDetails: Product = {
    id: 0,
    maker: '',
    model: '',
    image: '',
    rentalPrice: 0,
    availableQuantity: 0,
    totalRented: 0,
  };
  agreements: RentalAgreement = {
    carId: 0,
    startDate: new Date(),
    endDate: new Date(),
    userId: 0,
  };

  ngOnInit(): void {
    this.productservice.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.totalPages = Math.ceil(products.length / this.pageSize);
        console.log(products);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }

  deleteProduct(id: any) {
    this.productservice.deleteProduct(id).subscribe({
      next: (response) => {
        this.router.navigate(['products']);
      },
    });
  }

  filterProducts(): void {
    if (this.selectedCategory === 'All') {
      this.totalPages = Math.ceil(this.products.length / this.pageSize);
    } else {
      const filteredProducts = this.products.filter(
        (product) => product.maker === this.selectedCategory
      );
      this.totalPages = Math.ceil(filteredProducts.length / this.pageSize);
    }
  }
  Search(search: string) {
    console.log('search', this.searchInput);
    if (this.searchInput == null || this.searchInput == '') {
      this.productservice.getProducts().subscribe({
        next: (products) => {
          this.products = products;
          this.totalPages = Math.ceil(products.length / this.pageSize);
        },
        error: (response) => {
          console.log(response);
        },
      });
      this.totalPages = Math.ceil(this.products.length / this.pageSize);
    } else {
      this.products = this.products.filter(
        (product) =>
          product.maker
            .toLowerCase()
            .includes(this.searchInput.toLowerCase()) ||
          product.model.toLowerCase().includes(this.searchInput.toLowerCase())
      );
      this.totalPages = Math.ceil(this.products.length / this.pageSize);
    }
  }

  getCurrentPageProducts(): Product[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    if (this.selectedCategory === 'All') {
      console.log(this.products);
      return this.products.slice(startIndex, endIndex);
    } else {
      return this.products
        .filter((product) => product.maker === this.selectedCategory)
        .slice(startIndex, endIndex);
    }
  }
  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }
  goToNextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }
}
