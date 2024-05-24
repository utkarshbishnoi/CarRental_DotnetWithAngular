import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AddProductComponent } from './add-product/add-product.component';
import { FormsModule } from '@angular/forms';
import { EditProductComponent } from './edit-product/edit-product.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { ToastrModule } from 'ngx-toastr';
import { MyAgreementsComponent } from './my-agreements/my-agreements.component';
import { AllAgreementsComponent } from './all-agreements/all-agreements.component';
import { AgreementComponent } from './agreement/agreement.component';
@NgModule({
  declarations: [
    AppComponent,
    ProductsListComponent,
    AddProductComponent,
    EditProductComponent,
    ProductDetailsComponent,
    UserLoginComponent,
    HeaderComponent,
    HomeComponent,
    MyAgreementsComponent,
    AllAgreementsComponent,
    AgreementComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
