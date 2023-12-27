import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Orders } from '../Models/Orders';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { OrdersService } from '../Services/Orders/orders.service';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
OrderList?: Observable<Orders[]>;
OrderList1?: Observable<Orders[]>;
orderForm:any;
orderId = 0;
message="";
Orders:any;
searchText = new FormControl();
itemsPerPage: number = 5;
currentPage: number = 1;
totalPageCount: number;
pageSize: number = 10; // Number of items per page
pages: number[] = [];
  constructor(private formBuilder: FormBuilder, private orderservice: OrdersService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    this.orderForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      Description: ['', [Validators.required]],
      Price: ['', [Validators.required]],
      Quantity: ['', [Validators.required]]
    });
     this.getOrderList();
  }
 getOrderList() {
    this.orderservice.getOrders().subscribe((data) => {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      this.Orders = data.slice(startIndex, endIndex);
      this.totalPageCount = Math.ceil(data.length / this.itemsPerPage);
      console.log("Customers", this.Orders);
      // console.log("data",data);
      // this.Orders = data;
      // console.log("Products",this.Orders);
    });
  }
 //add or update data from one function only
  PostOrder(order: Orders) {
    
    const order_Master = this.orderForm.value;
    console.log('order_Master',order_Master);
    
    this.orderservice.SaveOrder(order_Master).subscribe(
      () => {
        this.getOrderList();
        this.orderForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }
  OrderDetailsToEdit(id: any) {
    this.orderservice.getOrderById(id).subscribe(
      (orderResult: Orders) => {
        this.orderId = orderResult.id;
        this.orderForm.controls['Name'].setValue(orderResult.name);
        this.orderForm.controls['Description'].setValue(orderResult.description);
        this.orderForm.controls['Price'].setValue(orderResult.price);
        this.orderForm.controls['Quantity'].setValue(orderResult.quantity);
      }
    );
  }
  DeleteOrder(id: number) {
    if (confirm('Are you sure you want to delete this order?')) {
      this.orderservice.deleteOrder(id).subscribe(
        () => {
          this.getOrderList();
          this.toastr.warning('Data Deleted Successfully');
        }
      );
    }
  }
  Clear(order: Orders) {
    this.orderForm.reset();
  }
  onItemsPerPageChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.itemsPerPage = +selectedValue; // Convert to a number
    this.currentPage = 1; // Reset to the first page
    this.getOrderList();
  }
  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getOrderList();
    }
  }
  nextPage() {
    if (this.currentPage < this.totalPageCount) {
      this.currentPage++;
      this.getOrderList();
    }
  }
  updatePagination() {
    this.totalPageCount = Math.ceil(this.Orders.length / this.pageSize);
    this.pages = Array.from({ length: this.totalPageCount }, (_, i) => i + 1);
  }
  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPageCount) {
      this.currentPage = page;
      this.getOrderList();
    }
  }
}
// function to store the data in local storage

