import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PaginationModule} from 'ngx-bootstrap/pagination';
import { from } from 'rxjs';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    [PaginationModule.forRoot()]
  ],
  exports:[PaginationModule]
})
export class SharedModule { }
