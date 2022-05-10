import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { NgbdSortableHeader } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ReportService } from 'src/app/_services/report.service';
import { SalesdetailReport } from 'src/app/_models/Report';
import { Location } from 'src/app/_models/Location';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { delay, map } from 'rxjs/operators';
import { ExcelService } from 'src/ExportExcel/excel.service';
@Component({
  selector: 'app-salesdetail',
  templateUrl: './salesdetail.component.html',
  providers: [ExcelService]
})

export class SalesdetailComponent implements OnInit {
  data$: Observable<SalesdetailReport[]>;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedBrand;
  private selectedLocation;
  Locations: Location[] = [];
  selectedLocations = [];
  locationID = 0;
  locationSubscription: Subscription;
  submit: boolean;
  orderDetails: SalesdetailReport[] = [];
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: ReportService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public excelService: ExcelService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
    // this.selectedLocation = this.ls.getSelectedLocation().locationID


    this.loadLocations();
  }

  ngOnInit() {
    
  }

  getData(locaionIDs) {
    this.service.SalesDetailRpt(this.selectedBrand, locaionIDs, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
      .subscribe((res: any) => {
        if (res != null) {
          
          this.orderDetails = res;
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
  exportAsXLSX(): void {
    debugger;
    this.excelService.exportAsExcelFile(this.orderDetails, 'Report_Export');
  }
  loadLocations() {
    this.service.loadLocations(this.selectedBrand).subscribe((res: any) => {

      this.Locations = res;
      this.locationID = this.selectedLocation;

      this.loadLocationsMulti()
      .pipe(map(x => x.filter(y => !y.disabled)))
      .subscribe((res) => {
        this.Locations = res;
        var arr=[];
        this.Locations.forEach(element => {
           arr.push(element.locationID);
        });
        this.selectedLocations=arr;
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
