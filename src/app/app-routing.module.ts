import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from '../app/homepage/homepage.component';
import { ProductsComponent } from './products/products.component';
import { OrdersComponent } from './orders/orders.component';
import { CustomersComponent } from './customers/customers.component';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './Guard/auth.guard';

const routes: Routes = [
    { path: '', component: HomepageComponent },
    { path: 'login', component: LoginComponent },

// {path: '', component: HomepageComponent},
{path:'products', component: ProductsComponent, canActivate: [AuthGuard]},
{path:'orders', component: OrdersComponent, canActivate: [AuthGuard]},
{path: 'customers', component: CustomersComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppRoutingModule { }
