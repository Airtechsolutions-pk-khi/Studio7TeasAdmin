import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { switchMap, tap, map } from 'rxjs/operators';
import { SortColumn, SortDirection } from '../_models/State';
import { State } from '../_models/State';
import DeliveryBoy from '../_models/DeliveryBoy';
import { Brands } from '../_models/Brands';

interface SearchDeliveryBoyResult {
  data: DeliveryBoy[];
  total: number;
}
const compare = (v1: string, v2: string) => v1 < v2 ? -1 : v1 > v2 ? 1 : 0;

function sort(data: DeliveryBoy[], column: SortColumn, direction: string): DeliveryBoy[] {
  if (direction === '' || column === '') {
    return data;
  } else {
    return [...data].sort((a, b) => {
      const res = compare(`${a[column]}`, `${b[column]}`);
      return direction === 'asc' ? res : -res;
    });
  }
}

function matches(data: DeliveryBoy, term: string) {
  
  return data.dbName.toLowerCase().includes(term.toLowerCase())
}

@Injectable({
  providedIn: 'root'
})

export class DeliveryBoyService {

  constructor(private http: HttpClient) {
  }
  private _loading$ = new BehaviorSubject<boolean>(true);
  private _search$ = new Subject<void>();
  private _allData$ = new BehaviorSubject<DeliveryBoy[]>([]);
  private _data$ = new BehaviorSubject<DeliveryBoy[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public categories: DeliveryBoy[];
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
    return this._data$.asObservable();
  }
  get allData$() {
    return this._allData$.asObservable();
  }
  
  private _set(patch: Partial<State>) {
    
    Object.assign(this._state, patch);
    this._search$.next();
  }
  
  getById(id) {
    return this.http.get<DeliveryBoy[]>(`api/DeliveryBoy/${id}`);
  }

  // ExportList(brandId) {
  //   return this.http.get<DeliveryBoy[]>(`api/DeliveryBoy/all/${brandId}`);
  // }

  loadBrands(brandId) {
    return this.http.get<Brands[]>(`api/brand/all/${brandId}`);
  }

  getAllData(brandId) {

    const url = `api/DeliveryBoy/all/${brandId}`;
    console.log(url);
    tap(() => this._loading$.next(true)),
      this.http.get<DeliveryBoy[]>(url).subscribe(res => {
        this.categories = res;
           
        this._data$.next(this.categories);
        this._allData$.next(this.categories);

        this._search$.pipe(
          switchMap(() => this._search()),
          tap(() => this._loading$.next(false))
        ).subscribe(result => {
          this._data$.next(result.data);
          this._total$.next(result.total);
        });

        this._search$.next();
      });
  }

  private _search(): Observable<SearchDeliveryBoyResult> {
     
    const { sortColumn, sortDirection, pageSize, page, searchTerm } = this._state;

    // 1. sort
    let sortedData = sort(this.categories, sortColumn, sortDirection);

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
  insert(data) {
    return this.http.post(`api/DeliveryBoy/insert`, data)
      .pipe(map(res => {
        
        console.log(res);
        return res;
      }));
  }

  update(updateData) {
    return this.http.post(`api/DeliveryBoy/update`, updateData)
      .pipe(map(res => {
        console.log(res);
        return res;
      }));
  }
 
  delete(updateData) {
    return this.http.post(`api/DeliveryBoy/delete`, updateData);
  }
 
  //  delete(updateData) {
  //   return this.http.put(`api/DeliveryBoy/delete`, updateData)
  //   .pipe(map(res => {
  //     console.log(res);
  //     return res;
  //   }));
  //    return this.http.delete<any[]>(`api/DeliveryBoy/delete/${id}`);
  //  }

}
