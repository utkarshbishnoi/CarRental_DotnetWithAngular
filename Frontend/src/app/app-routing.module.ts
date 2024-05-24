import { NgModule, importProvidersFrom } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsListComponent } from './products-list/products-list.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { MyAgreementsComponent } from './my-agreements/my-agreements.component';
import { AllAgreementsComponent } from './all-agreements/all-agreements.component';
import { AgreementComponent } from './agreement/agreement.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'products',
    component: ProductsListComponent,
  },
  {
    path: 'add-product',
    component: AddProductComponent,
  },
  {
    path: 'edit-product/:id',
    component: EditProductComponent,
  },
  {
    path: 'product-details/:id',
    component: ProductDetailsComponent,
  },{
    path: 'agreement/:id',
    component: AgreementComponent,
  },
  {
    path: 'user-login',
    component: UserLoginComponent,
  },
  {
    path: 'my-agreements',
    component: MyAgreementsComponent,
  },
  {
    path: 'all-agreements',
    component: AllAgreementsComponent,
  },
  {
    path: 'header',
    component: HeaderComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
