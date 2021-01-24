import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products: IProduct[] = [];


  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get<IPagination>('https://localhost:44335/api/Products?pageSize=50').subscribe(
      (response: IPagination) => {
     this.products = response.data;
     console.log(this.products);
    }, error => {

      console.log(error);


    });
  }
}
