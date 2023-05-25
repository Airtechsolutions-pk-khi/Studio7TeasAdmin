import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';


import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import  DeliveryBoy  from 'src/app/_models/DeliveryBoy';
import { ToastService } from 'src/app/_services/toastservice';
import { DeliveryBoyService } from 'src/app/_services/deliveryBoy.service';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

@Component({
  selector: 'app-delivery-boy',
  templateUrl: './delivery-boy.component.html',
  styleUrls: ['./delivery-boy.component.css']
})
export class DeliveryBoyComponent implements OnInit {
  data$: Observable<DeliveryBoy[]>;  
  oldData: DeliveryBoy[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;
  
  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: DeliveryBoyService,
    public ls :LocalStorageService,
    public ts :ToastService,
    public router:Router) {
     this.selectedBrand =this.ls.getSelectedBrand().brandID;

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
    console.log(this.service.data$)
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

  Edit(DeliveryBoy) {
        this.router.navigate(["admin/deliveryBoy/edit", DeliveryBoy]);
  }

  Delete(obj) {
    this.service.delete(obj).subscribe((res: any) => {
      if(res!=0){
        this.ts.showSuccess("Success","Record deleted successfully.")
        this.getData();
      }
      else
      this.ts.showError("Error","Failed to delete record.")

    }, error => {
      this.ts.showError("Error","Failed to delete record.")
    });
  }
}
