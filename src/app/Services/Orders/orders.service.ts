import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Orders } from 'src/app/Models/Orders';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
//add api url
  url = 'https://localhost:7028/api' + '/order';
  constructor(private http: HttpClient) { }
  //get all orders
  getOrders() : Observable<Orders[]> {
    return this.http.get<Orders[]>(this.url);
  }
  //get order by id
  getOrderById(id: any): Observable<Orders> {
    console.log('id',id);
    return this.http.get<Orders>(this.url + '/' + id);
  }
  //add or update order logic in same method named SaveOrder
  SaveOrder(order: Orders): Observable<Orders> {
      const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
      return this.http.post<Orders>(this.url, order, httpHeaders);
    }
  //delete order
  deleteOrder(id: number): Observable<Orders> {
    return this.http.delete<Orders>(this.url + '/' + id);
  }
}
