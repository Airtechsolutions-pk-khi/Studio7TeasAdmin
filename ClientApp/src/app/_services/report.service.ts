import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SummaryReport, SalesdetailReport, SalescategorywiseReport, SalescustomerwiseReport, SalesitemwiseReport, Report } from '../_models/Report';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { SortColumn, SortDirection, State } from '../_models/State';
import { switchMap, tap, map } from 'rxjs/operators';


interface SearchReportsResult {
  data: SalescategorywiseReport[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

function sort(data: SalescategorywiseReport[], column: SortColumn, direction: string): SalescategorywiseReport[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(data: SalescategorywiseReport, term: string) {
  return data.categoryName.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})

export class ReportService {

  constructor(private http: HttpClient) {   
  }
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<SalescategorywiseReport[]>([]);
  private _data$ = new BehaviorSubject<SalescategorywiseReport[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public salescategorywiseReport: SalescategorywiseReport[];  
  private _state: State = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };
  get total$() { return this._total$.asObservable(); }
  get loading$() { return this._loading$.asObservable(); }
  get page() { return this._state.page; }
  get pageSize() { return this._state.pageSize; }
  get searchTerm() { return this._state.searchTerm; }

  set page(page: number) { this._set({ page }); }
  set pageSize(pageSize: number) { this._set({ pageSize }); }
  set searchTerm(searchTerm: any) { this._set({ searchTerm }); }
  set sortColumn(sortColumn: SortColumn) { this._set({ sortColumn }); }
  set sortDirection(sortDirection: SortDirection) { this._set({ sortDirection }); }

  get data$() {
    debugger;
    return this._data$.asObservable();
  }
  get allData$() {
    return this._allData$.asObservable();
  }
  
  private _set(patch: Partial<State>) {
    
    Object.assign(this._state, patch);
    this._search$.next();
  }

private _search(): Observable<SearchReportsResult> {

    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.salescategorywiseReport, sortColumn, sortDirection);

    //// 2. filter
    sortedData = sortedData.filter(data => matches(data, searchTerm));
    const total = sortedData.length;

    // 3. paginate
    const data = sortedData.slice((page - 1) * pageSize, (page - 1) * pageSize + pageSize);
    return of({ data, total });
}

clear() {
  // clear by calling subject.next() without parameters
  this._search$.next();
  this._data$.next(null);
  this._allData$.next(null);
  this._total$.next(null);
  this._loading$.next(null);
  this._state = {
    page: 1,
    pageSize: 10,
    searchTerm: '',
    sortColumn: '',
    sortDirection: ''
  };
}

  SalesSummaryRpt(brandID,fromDate,toDate) {
    return this.http.get<SummaryReport[]>(`api/report/summary/${brandID}/${fromDate}/${toDate}`);
  }

  SalesDetailRpt(brandID,locationID,fromDate,toDate) {
    return this.http.get<SalesdetailReport[]>(`api/report/salesdetail/${brandID}/${locationID}/${fromDate}/${toDate}`);
  }

  SalesItemwiseRpt(brandID,locationID,fromDate,toDate) {
    return this.http.get<SalesitemwiseReport[]>(`api/report/salesitemwise/${brandID}/${locationID}/${fromDate}/${toDate}`);
  }

  SalesCategorywiseRpt(brandID,locationID,fromDate,toDate) {
    return this.http.get<SalescategorywiseReport[]>(`api/report/salescategorywise/${brandID}/${locationID}/${fromDate}/${toDate}`);
  }

  SalesCustomerwiseRpt(brandID,locationID,customerID,fromDate,toDate) {
    return this.http.get<SalescustomerwiseReport[]>(`api/report/salescustomerwise/${brandID}/${locationID}/${customerID}/${fromDate}/${toDate}`);
  }

  SalesUserwiseRpt(brandID,locationID,fromDate,toDate) {
    return this.http.get<SalesdetailReport[]>(`api/report/salesuserwise/${brandID}/${locationID}/${fromDate}/${toDate}`);
  }
  
  loadLocations(brandId) {
    return this.http.get<Location[]>(`api/location/all/${brandId}`);
  }


}
