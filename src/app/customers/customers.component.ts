import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Customers } from '../Models/Customers';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { CustomersService } from '../Services/Customers/customers.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  CustomerList?: Observable<Customers[]>;
  CustomerList1?: Observable<Customers[]>;
  customerForm:any;
  customerId = 0;
  message="";
  Customers:any;
  now: string;
  itemsPerPage: number = 5;
  currentPage: number = 1;
totalPageCount: number;
  searchText = new FormControl();
  // Properties for pagination
pageSize: number = 10; // Number of items per page
pages: number[] = [];
  constructor(private formBuilder: FormBuilder, private customerservice: CustomersService, private router: Router, private toastr: ToastrService) {
    this.now = this.formatDateForInput(new Date());
   }
  ngOnInit() {
    this.customerForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      DateOfBirth: [null, [Validators.required]],
      Address: ['', [Validators.required]],
    });
    this.getCustomerList();
  }
  formatDateForInput(date: Date): string {
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear();
    return `${year}-${month}-${day}`;
  }
  setFormattedDate(inputDate: string): void {
    this.customerForm.get('DateOfBirth').setValue(inputDate);
  }
  formatDateForDisplay(date: string): string {
    if (!date) return ''; // Handle the case when date is not set
    const parts = date.split('T')[0].split('-');
    if (parts.length !== 3) return date; // Return the date as is if it's not in expected format
    const [year, month, day] = parts;
    return `${year}-${month}-${day}`;
  }
  getCustomerList() {
    this.customerservice.getCustomers().subscribe((data) => {
        const startIndex = (this.currentPage - 1) * this.itemsPerPage;
        const endIndex = startIndex + this.itemsPerPage;
        this.Customers = data.slice(startIndex, endIndex);
        this.totalPageCount = Math.ceil(data.length / this.itemsPerPage);
        console.log("Customers", this.Customers);
      // console.log("data",data);
      // this.Customers = data;
      // console.log("Customers",this.Customers);
    });
  }
  PostCustomer(customer: Customers) {
    const customer_Master = this.customerForm.getRawValue();
    this.customerservice.addCustomer(customer_Master).subscribe(
      () => {
        this.getCustomerList();
        this.customerForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }
  CustomerDetailsToEdit(id: any) {
    console.log('Edit button clicked with ID:', id);
    this.customerservice.getCustomerById(id).subscribe(
      (customerResult: Customers) => {
        this.customerId = customerResult.id;
        this.customerForm.controls['Name'].setValue(customerResult.name);
        if (customerResult.dateOfBirth) {
          const formattedDate = this.formatDateForDisplay(customerResult.dateOfBirth.toString());
          this.customerForm.controls['DateOfBirth'].setValue(formattedDate);
        } else {
          this.customerForm.controls['DateOfBirth'].setValue('');
        }
        
        this.customerForm.controls['Address'].setValue(customerResult.address);
      }
    );
  }
  convertBackendDateToFrontendFormat(backendDate: string): string {
    const parts = backendDate.split(' ');
    if (parts.length === 2) {
      const datePart = parts[0].substr(1); // Remove the leading '{'
      const [day, month, year] = datePart.split('-');
      return `${year}-${month}-${day}`;
    }
    return backendDate; // Return as is if not in expected format
  }
  
  UpdateCustomer(customer: Customers) {
    console.log('customer', customer);
    customer.id = this.customerId;
    const customer_Master = this.customerForm.getRawValue();
    this.customerservice.updateCustomer(customer_Master).subscribe(
      () => {
        this.toastr.success('Data Updated Successfully');
        // Reset productId after update
        this.customerForm.reset();
        this.getCustomerList();
      }
    );
  }
  DeleteCustomer(id: number) {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerservice.deleteCustomer(id).subscribe(
        () => {
          this.toastr.success('Data Deleted Successfully');
          this.getCustomerList();
        }
      );
    }
  }
  Clear(customer: Customers) {
    this.customerForm.reset();
  }
  public logOut = () => {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }
  onItemsPerPageChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.itemsPerPage = +selectedValue; // Convert to a number
    this.currentPage = 1; // Reset to the first page
    this.getCustomerList();
  }
  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getCustomerList();
    }
  }
  nextPage() {
    if (this.currentPage < this.totalPageCount) {
      this.currentPage++;
      this.getCustomerList();
    }
  }
  updatePagination() {
    this.totalPageCount = Math.ceil(this.Customers.length / this.pageSize);
    this.pages = Array.from({ length: this.totalPageCount }, (_, i) => i + 1);
  }
  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPageCount) {
      this.currentPage = page;
      this.getCustomerList();
    }
  }
}