import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Location } from '../_models/Location';
import { BehaviorSubject, Subject } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { DashboardSummary } from '../_models/Dashboard';
import { State } from '../_models/State';

interface SearchDashboardResult {
  data: DashboardSummary[];
  total: number;
}
@Injectable({
  providedIn: 'root'
})

export class DashboadService {

  constructor(private http: HttpClient) {
  }
  
  private _allData$ = new BehaviorSubject<DashboardSummary[]>([]);
  private _total$ = new BehaviorSubject<number>(0);
  public dashboard: DashboardSummary[];

  GetDashboard(locationID, date) {
    
    var today = date;
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = yyyy + '-' + dd + '-' + mm;
    return this.http.get<any[]>(`api/dashboard/get/${locationID}/${today}`);
  }

  GetDashboardRange(locationID, fdate,tdate) {
    
    return this.http.get<any[]>(`api/dashboard/range/get/${locationID}/${fdate}/${tdate}`);
  }
}
