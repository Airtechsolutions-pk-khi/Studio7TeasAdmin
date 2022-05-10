import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';
import { NgbdSortableHeader } from 'src/app/_directives/sortable.directive';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ReportService } from 'src/app/_services/report.service';
import { SalesitemwiseReport } from 'src/app/_models/Report';
import { Location } from 'src/app/_models/Location';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { delay, map } from 'rxjs/operators';
import { ExcelService } from 'src/ExportExcel/excel.service';
@Component({
  selector: 'app-salesitemwise',
  templateUrl: './salesitemwise.component.html',
  providers: [ExcelService]
})

export class SalesitemwiseComponent implements OnInit {
  data$: Observable<SalesitemwiseReport[]>;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedBrand;
  private selectedLocation;
  Locations: Location[] = [];
  selectedLocations = [];
  locationID = 0;
  locationSubscription: Subscription;
  submit: boolean;
  salesItemWise: SalesitemwiseReport[] = [];
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
    debugger;
    this.excelService.exportAsExcelFile(this.salesItemWise, 'Report_Export');
  }
  getData(locaionID) {
    
    this.service.SalesItemwiseRpt(this.selectedBrand, locaionID, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
      .subscribe((res: any) => {
        if (res != null) {
     
          this.salesItemWise = res;
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
  private loadLocations() {
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
}
