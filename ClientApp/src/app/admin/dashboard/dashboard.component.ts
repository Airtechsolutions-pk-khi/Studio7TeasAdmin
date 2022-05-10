import { Component, NgModule, ViewChild } from '@angular/core';


import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexTitleSubtitle,
  ApexMarkers,
  AnnotationStyle,
  ApexDataLabels,
  ApexStroke,
  ApexGrid,
  ApexYAxis,
  ApexAnnotations,
  ApexNonAxisChartSeries,
  ApexFill,
  ApexLegend,
  ApexTooltip,
  ApexPlotOptions,
  ApexResponsive,
  ApexStates,
  ApexTheme
} from "ng-apexcharts";
import { DashboadService } from 'src/app/_services/dashboard.service';
import { DashboardSummary } from 'src/app/_models/Dashboard';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';
export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  title: ApexTitleSubtitle;
};
const now = new Date();
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions;  
  public chartOptionsDonut;
  selectedLocations = [];
  dashboardSummary=new DashboardSummary();
  ngOnInit() {
    const date: NgbDate = new NgbDate(now.getFullYear(), now.getMonth() + 1, 1);   
    this._datepicker.fromDate=date;
    this.GetDataDashboard();
    
  }
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
 
  constructor(public service: DashboadService,public ls: LocalStorageService) {
    
    
  }
  BindTodaysSales(sales, timeSlot) {
    this.chartOptions = {
      series: [
        {
          name: "Sales",
          data: sales
        }
      ],
      chart: {
        height: 300,
        type: "bar"
      },
      title: {
        text: ""
      },
      xaxis: {
        categories: timeSlot
      }
    };

  }
  BindMAEN(maen) {
    
    this.chartOptionsDonut = {
      series: [maen.morning, maen.afterNoon, maen.evening, maen.night],
      chart: {
        type: "donut"
      },
      labels: ["Morning", "Evening", "Afternoon", "Night"],
      responsive: [
        {
          breakpoint: 250,
          options: {
            colors: ['#2E93fA', '#66DA26', '#546E7A', '#E91E63', '#FF9800'],
            chart: {
              width: 100
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    };
  }
  GetDashboard() {
    
    this.service.GetDashboard(this.ls.getSelectedLocation().locationID,new Date()).subscribe((res: any) => {
      
      this.dashboardSummary = res.summarysales;
      this.BindTodaysSales(res.todaysales.sales, res.todaysales.timeSlot);
      this.BindMAEN(res.maensales);
    }, error => {

    });

  }

  GetDataDashboard(){
   
    this.service.GetDashboardRange(this.ls.getSelectedLocation().locationID,this.parseDate(this._datepicker.fromDate),this.parseDate(this._datepicker.toDate)).subscribe((res: any) => {
    
      this.dashboardSummary = res.summarysales;
      this.BindTodaysSales(res.todaysales.sales, res.todaysales.timeSlot);
      this.BindMAEN(res.maensales);
    }, error => {

    });
  }
  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }
 
}
