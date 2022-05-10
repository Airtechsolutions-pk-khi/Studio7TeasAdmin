
import { Component, OnInit,  ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/_services/toastservice';
import { ReportService } from 'src/app/_services/report.service';
import { SummaryReport } from 'src/app/_models/Report';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
import { ExcelService } from 'src/ExportExcel/excel.service';
@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  providers: [ExcelService]
})
export class SummaryComponent implements OnInit {
  public _model = new SummaryReport();
  locationSubscription: Subscription;
  export=[];
  private selectedBrand;
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  constructor(public service: ReportService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public excelService: ExcelService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
  }

  ngOnInit() {
    this.getData();
  }
  exportAsXLSX(): void {

    this.excelService.exportAsExcelFile(this.export, 'Report_Export');
  }
  getData() {
    
    this.service.SalesSummaryRpt(this.selectedBrand, this.parseDate(this._datepicker.fromDate), this.parseDate(this._datepicker.toDate))
      .subscribe((res: any) => {
        if (res != null) {
          this._model = res[0];
          this.export.push(this._model);
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
}
