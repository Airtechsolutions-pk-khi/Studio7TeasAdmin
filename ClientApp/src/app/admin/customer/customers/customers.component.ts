import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { CustomersService } from 'src/app/_services/customers.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { Customers } from 'src/app/_models/Customers';
import { ToastService } from 'src/app/_services/toastservice';
import { ExcelService } from 'src/ExportExcel/excel.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  providers: [ExcelService]
})

export class CustomersComponent implements OnInit {
  data$: Observable<Customers[]>;  
  oldData: Customers[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;
  
  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: CustomersService,
    public ls :LocalStorageService,
    public excelService: ExcelService,
    public ts :ToastService,
    public router:Router) {
     this.selectedBrand =this.ls.getSelectedBrand().brandID;

    this.loading$ = service.loading$;
    this.submit = false;
    
  }

  ngOnInit() {
    this.getData();
  }
  exportAsXLSX(): void {
    this.service.ExportList(this.selectedBrand).subscribe((res: any) => {    
      this.excelService.exportAsExcelFile(res, 'Report_Export');
    }, error => {
      this.ts.showError("Error","Failed to export")
    });
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

  Edit(customers) {
        this.router.navigate(["admin/customer/edit", customers]);
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

  Deactive(id, rowVersion) {

  //   this.service.deactive(parseInt(id), rowVersion).subscribe((res: any) => {
  //     this.alertService.success("Customers has been Deactived");
  //     this.getBrandData();
  //   }, error => {
  //     this.alertService.error(error);
  //   });
  }
}
