
import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';

import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { BrandsService } from 'src/app/_services/brands.service';
import { Brands } from 'src/app/_models/Brands';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-brand',
  templateUrl: './brands.component.html',
  providers: []
})
export class BrandComponent implements OnInit {
  data$: Observable<Brands[]>;  
  oldData: Brands[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;


  
  locationSubscription: Subscription;

  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: BrandsService,
    public ls :LocalStorageService,
    public ts :ToastService,
    public router:Router) {      
    this.loading$ = service.loading$;
    this.submit = false;    
  }

  ngOnInit() {
    this.getData();
  }

  getData() {    
    this.service.getAllData(this.selectedBrand);    
    this.data$ = this.service.data$;
    this.total$ = this.service.total$;
    this.loading$ = this.service.loading$;
  }

  onSort({ column, direction }: SortEvent) {

    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });
    this.service.sortColumn = column;
    this.service.sortDirection = direction;
  }


  Edit(brand) {
    this.router.navigate(["admin/brand/edit", brand]);
  }

  Delete(id) {
    this.service.delete(parseInt(id)).subscribe((res: any) => {
      if(res!=0){
        this.ts.showSuccess("Success","Record deleted successfully.")
        this.getData();
      }
      else
      this.ts.showError("Error","Failed to delete record.");

    }, error => {
     this.ts.showError("Error","Failed to delete record.")
    });
  }
  Deactive(id, rowVersion) {

  //   this.service.deactive(parseInt(id), rowVersion).subscribe((res: any) => {
  //     this.alertService.success("Brand has been Deactived");
  //     this.getBrandData();
  //   }, error => {
  //     this.alertService.error(error);
  //   });
  }
}
