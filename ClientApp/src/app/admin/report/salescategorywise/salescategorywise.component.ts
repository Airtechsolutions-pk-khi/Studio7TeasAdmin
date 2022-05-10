import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { NgbdSortableHeader } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ReportService } from 'src/app/_services/report.service';
import { SalescategorywiseReport } from 'src/app/_models/Report';
import { delay, map } from 'rxjs/operators';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { Location } from 'src/app/_models/Location';

import { ExcelService } from 'src/ExportExcel/excel.service';
@Component({
  selector: 'app-salescategorywise',
  templateUrl: './salescategorywise.component.html',
  providers: [ExcelService]
})

export class SalescategorywiseComponent implements OnInit {
  data$: Observable<SalescategorywiseReport[]>;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedBrand;
  private selectedLocation;
  Locations: Location[] = [];
  selectedLocations = [];
  locationID = 0;
  locationSubscription: Subscription;
  submit: boolean;
  salesCategoryWise: SalescategorywiseReport[] = [];
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: ReportService,
    public ls: LocalStorageService,
    public excelService: ExcelService,
    public ts: ToastService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;

    this.loadLocations();
  }

  ngOnInit() {

  }
  exportAsXLSX(): void {

    this.excelService.exportAsExcelFile(this.salesCategoryWise, 'Report_Export');
  }
  getData(locaionID) {
    this.service.SalesCategorywiseRpt(this.selectedBrand, locaionID, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
      .subscribe((res: any) => {
        if (res != null) {
         
          this.salesCategoryWise = res;
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
  loadLocations() {
    this.service.loadLocations(this.selectedBrand).subscribe((res: any) => {

      this.Locations = res;
      this.locationID = this.selectedLocation;

      this.loadLocationsMulti()
        .pipe(map(x => x.filter(y => !y.disabled)))
        .subscribe((res) => {
          this.Locations = res;
          var arr = [];
          this.Locations.forEach(element => {
            arr.push(element.locationID);
          });
          this.selectedLocations = arr;

          this.getData(this.selectedLocations.toString());
        });

    });

  }
  loadLocationsMulti(term: string = null): Observable<Location[]> {
    let items = this.Locations;
    if (term) {
      items = items.filter(x => x.name.toLocaleLowerCase().indexOf(term.toLocaleLowerCase()) > -1);
    }
    return of(items).pipe(delay(500));
  }
  Filter() {
    
    this.getData(this.selectedLocations.toString());
  }
}
