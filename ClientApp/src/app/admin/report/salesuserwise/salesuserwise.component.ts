import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ReportService } from 'src/app/_services/report.service';
import { SalesuserwiseReport } from 'src/app/_models/Report';

import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { ExcelService } from 'src/ExportExcel/excel.service';
@Component({
  selector: 'app-salesuserwise',
  templateUrl: './salesuserwise.component.html',
  providers: []
})

export class SalesuserwiseComponent implements OnInit {
  data$: Observable<SalesuserwiseReport[]>;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedBrand;
  private selectedLocation;
  Locations: []
  locationID = 0;
  locationSubscription: Subscription;
  submit: boolean;
  salesUserWise: SalesuserwiseReport[] = [];
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: ReportService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public excelService: ExcelService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
    this.selectedLocation = this.ls.getSelectedLocation().locationID
  }

  ngOnInit() {
    this.getData(this.selectedLocation);
    this.loadLocations();
  }
  exportAsXLSX(): void {
    this.excelService.exportAsExcelFile(this.salesUserWise, 'Report_Export');
  }
  getData(locaionID) {
    this.service.SalesDetailRpt(this.selectedBrand, locaionID, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
      .subscribe((res: any) => {
        if (res != null) {
          this.data$ = res;
        }
        else
          this.ts.showError("Error", "Something went wrong");

      }, error => {
        this.ts.showError("Error", "Failed to delete record.")
      });
  }

  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }
  private loadLocations() {
    this.service.loadLocations(this.selectedBrand).subscribe((res: any) => {

      this.Locations = res;
      this.locationID=this.selectedLocation ;
    });
  }
  Filter() {
    debugger
    // this.getData(obj.target.value);
  }
  
}
