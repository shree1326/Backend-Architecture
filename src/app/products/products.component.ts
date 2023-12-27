import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Products } from '../Models/Products';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ProductsService } from '../Services/Products/products.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
ProductList?: Observable<Products[]>;
ProductList1?: Observable<Products[]>;
productForm:any;
productId = 0;
message="";
Products:any;
searchText = new FormControl();
itemsPerPage: number = 5;
currentPage: number = 1;
totalPageCount: number;
pageSize: number = 10; // Number of items per page
pages: number[] = [];
  constructor(private formBuilder: FormBuilder, private productservice: ProductsService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    this.productForm = this.formBuilder.group({
      Name: ['', [Validators.required]],
      Description: ['', [Validators.required]],
      Price: ['', [Validators.required]],
      Category: ['', [Validators.required]]
    });
    // this.getProductList();
      this.getProductList();
  }
  getProductList() {
    this.productservice.getProductsAsync().subscribe((data) => {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      this.Products = data.slice(startIndex, endIndex);
      this.totalPageCount = Math.ceil(data.length / this.itemsPerPage);
      console.log("Customers", this.Products);
      // console.log("data",data);
      // this.Products = data;
      // console.log("Products",this.Products);
    });
  }
  PostProduct(product: Products) {
    const product_Master = this.productForm.value;
    this.productservice.addProductAsync(product_Master).subscribe(
      () => {
        this.getProductList();
        this.productForm.reset();
        this.toastr.success('Data Saved Successfully');
      }
    );
  }
  ProductDetailsToEdit(id: any) {
    console.log('id',id);
    this.productservice.getProductByIdAsync(id).subscribe(
      (productResult: Products) => {
        this.productId = productResult.id;
        this.productForm.controls['Name'].setValue(productResult.name);
        this.productForm.controls['Description'].setValue(productResult.description);
        this.productForm.controls['Price'].setValue(productResult.price);
        this.productForm.controls['Category'].setValue(productResult.category);
      }
    );
  }
  UpdateProduct(product: Products) {
    product.id = this.productId;
    const product_Master = this.productForm.value;
    console.log('product_Master',product);
    
    this.productservice.updateProductAsync(product_Master).subscribe(
      () => {
        this.toastr.success('Data Updated Successfully');
        this.productId = 0; // Reset productId after update
        this.productForm.reset();
        this.getProductList();
      }
    );
  }
  DeleteProduct(id: number) {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productservice.deleteProductAsync(id).subscribe(
        () => {
          this.toastr.success('Data Deleted Successfully');
          this.getProductList();
        }
      );
    }
  }
  Clear(product: Products) {
    this.productForm.reset();
  }
  public logOut = () => {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }
  onItemsPerPageChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.itemsPerPage = +selectedValue; // Convert to a number
    this.currentPage = 1; // Reset to the first page
    this.getProductList();
  }
  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getProductList();
    }
  }
  nextPage() {
    if (this.currentPage < this.totalPageCount) {
      this.currentPage++;
      this.getProductList();
    }
  }
  updatePagination() {
    this.totalPageCount = Math.ceil(this.Products.length / this.pageSize);
    this.pages = Array.from({ length: this.totalPageCount }, (_, i) => i + 1);
  }
  goToPage(page: number) {
    if (page >= 1 && page <= this.totalPageCount) {
      this.currentPage = page;
      this.getProductList();
    }
  }
}
