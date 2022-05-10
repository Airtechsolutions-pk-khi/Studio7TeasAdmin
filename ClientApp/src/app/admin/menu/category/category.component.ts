
import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { AddcategoryComponent } from './addcategory/addcategory.component';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';
import { Category } from 'src/app/_models/Cateogry';
import { CategoryService } from 'src/app/_services/category.service';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ToastrService } from 'ngx-toastr';
import { ExcelService } from 'src/ExportExcel/excel.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  providers: [ExcelService]
})

export class CategoryComponent implements OnInit {
 
  data$: Observable<Category[]>;
  oldData: Category[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;
  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  categories: Category[] = [];
  constructor(public service: CategoryService,
    public excelService: ExcelService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public tss: ToastrService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
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

  Edit(category) {
    this.router.navigate(["admin/category/edit", category]);
  }

  Delete(obj) {
    this.service.delete(obj).subscribe((res: any) => {
      if (res != 0) {
        this.ts.showSuccess("Success", "Record deleted successfully.")
        this.getData();
      }
      else {
        this.ts.showError("Error", "Failed to delete record.");
      }
    }, error => {
      this.ts.showError("Error","Failed to delete record.")
    });
  }
  Deactive(id, rowVersion) {

    //   this.service.deactive(parseInt(id), rowVersion).subscribe((res: any) => {
    //     this.alertService.success("Category has been Deactived");
    //     this.getBrandData();
    //   }, error => {
    //     this.alertService.error(error);
    //   });
  }
}
