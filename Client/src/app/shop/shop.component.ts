import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: Array<IProduct> = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected = 'name';
  sortOptions = [
    { name: 'Alpabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc'},
    { name: 'Price: High to Low', value: 'priceDesc'}

  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {

  this.getProducts();
  this.getBrands();
  this.getTypes();

  }

  getProducts(): void{

    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe(response => {


      if (response?.data )
      {

         this.products = response.data;
         console.log('pagination: ' + response.data);
      }

    }, error => {
      console.log(error);
    });

  }

  getBrands(): void{
    this.shopService.getBrands().subscribe(response =>{
      this.brands = [{id: 0, name: 'All'}, ...response];
      console.log('brands: ' + response);
    }, error => {
      console.log(error);
    });
  }

  getTypes(): void{
    this.shopService.getTypes().subscribe(response =>{
      this.types = [{id: 0, name: 'All'}, ...response];
      console.log('types: ' + this.types);
    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brandId: number){
    this.brandIdSelected = brandId;
    this.getProducts();

  }

  onTypeSelected(typeId: number){
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(e: Event){
    this.sortSelected = (e.target as HTMLSelectElement).value.toString();
    this.getProducts();
  }
  

}
