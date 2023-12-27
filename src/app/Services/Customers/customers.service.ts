import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customers } from 'src/app/Models/Customers';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
//add api url
  url = 'https://localhost:7273/api' + '/customer';
  constructor(private http: HttpClient) { }
  //get all customers
  getCustomers() : Observable<Customers[]> {
    return this.http.get<Customers[]>(this.url);
  }
  //get customer by id
  getCustomerById(id: any): Observable<Customers> {
    return this.http.get<Customers>(this.url + '/' + id);
  }
  //add customer
  addCustomer(customer: Customers): Observable<Customers> {
    return this.http.post<Customers>(this.url, customer);
  }
  //update customer
  updateCustomer(customer: Customers): Observable<Customers> {
    return this.http.put<Customers>(this.url, customer);
  }
  //delete customer
  deleteCustomer(id: number): Observable<Customers> {
    return this.http.delete<Customers>(this.url + '/' + id);
  }
}
