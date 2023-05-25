import { Component, OnInit, QueryList, ViewChildren, ViewChild } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { DeliveryBoyService } from 'src/app/_services/deliveryboydetailReport';
import { DeliveryboydetailReport } from 'src/app/_models/Report';
import { Location } from 'src/app/_models/Location';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { delay, map, switchMap, tap } from 'rxjs/operators';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { NgbdSortableHeader } from 'src/app/_directives/sortable.directive';

@Component({
  selector: 'app-deliveryboydetail',
  templateUrl: './deliveryboydetail.component.html',
  styleUrls: ['./deliveryboydetail.component.css'],
  providers: [ExcelService]
})

export class DeliveryboydetailComponent implements OnInit {
  data$: Observable<DeliveryboydetailReport[]>;

  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  private selectedBrand;
  private selectedLocation;
  Locations: Location[] = [];
  selectedLocations = [];
  locationID = 0;
  locationSubscription: Subscription;
  submit: boolean;
  DeliveryboyDetails: DeliveryboydetailReport[] = [];
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;
  @ViewChild('locationDrp') drplocation: any;
  constructor(public service: DeliveryBoyService,
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

  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }

  getData(locationIDs) {   
    this.service.DeliveryBoyDetailRpt(this.selectedBrand, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate));
    this.data$ = this.service.data$;
    console.log(this.service.data$);
    
    // this.total$ = this.service.total$;
    // this.loading$ = this.service.loading$;
    console.log(this.service.data$)
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
  
  // getData(locaionIDs) {
  //   this.service.DeliveryBoyDetailRpt(this.selectedBrand, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
  //     .subscribe((res: any) => {
  //       if (res != null) {
          
  //         this.DeliveryboyDetails = res;

  //         // this.service._search$.pipe(
  //         //   switchMap(() => this.service._search()),
  //         //   tap(() => this.service._loading$.next(false))
  //         // ).subscribe(result => {
  //         //   this.service._data$.next(result.data);
  //         //   this.service._total$.next(result.total);
  //         // });
  
  //         // this.service._search$.next();

  //       }
  //       else
  //         this.ts.showError("Error", "Something went wrong");

  //     }, error => {
  //       this.ts.showError("Error", "Failed to delete record.")
  //     });
  // }

  exportAsXLSX(): void {
    debugger;
    this.excelService.exportAsExcelFile(this.service.DeliveryboyDetails, 'Report_Export');
  }
  Filter() {
    
    this.getData(this.selectedLocations.toString());
  }
}
