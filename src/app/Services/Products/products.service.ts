import { Injectable } from '@angular/core';
import { Products } from 'src/app/Models/Products';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
   url = 'http://localhost:5213/api' + '/product';
  //url = 'api/products';
  constructor(private http: HttpClient) {}
    getProductsAsync(): Observable<Products[]> {
      return this.http.get<Products[]>(this.url);
   }
    getProductByIdAsync(id: any): Observable<Products> {
      console.log('id',id);
      
      return this.http.get<Products>(this.url + '/' + id);
    }
    addProductAsync(product: Products): Observable<Products> {
      const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
      return this.http.post<Products>(this.url, product, httpHeaders);
    }
    updateProductAsync(product: Products): Observable<Products> {
      const httpHeaders = { headers:new HttpHeaders({'Content-Type': 'application/json'}) };
      const updateUrl = `${this.url}`;
      return this.http.put<Products>(updateUrl, product, httpHeaders);
    }
    deleteProductAsync(id: number): Observable<Products> {
      return this.http.delete<Products>(this.url + '/' + id);
    }
}
