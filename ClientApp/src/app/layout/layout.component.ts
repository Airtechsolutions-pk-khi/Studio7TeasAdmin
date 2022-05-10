import { Component, OnInit, NgModule } from '@angular/core';
import { Router } from '@angular/router';
// import { TranslateService } from '@ngx-translate/core';
import { environment } from 'src/environments/environment.prod';
// import { LoginService } from '../services/login.service';
// import { DashboardService } from '../services/dashboard.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { LocationsService } from '../_services/locations.service';
import { LocalStorageService } from '../_services/local-storage.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],

})
export class LayoutComponent implements OnInit {
  _Langname = "";
  branchname = "";
  email = "";
  locationID: 0;
  Locations: [];
  private selectedBrand;

  ngOnInit() {
    var data = this.ls.getSelectedBrand();

    this.loadLocations();


    if (data == null)
      this.router.navigate(["/"]);

  }
  constructor(private router: Router
    , public service: LocationsService
    , public ls: LocalStorageService) {
    this.branchname = this.ls.getSelectedBrand().name;
    this.email = this.ls.getSelectedBrand().email;
  }
  Logout() {

    sessionStorage.clear();
    this.router.navigate(['/']);
  }

  private loadLocations() {
    // var loc = this.ls.getLocation();
    // if (loc != null) {
    //   this.Locations = this.ls.getLocation();
    //   this.locationID = this.ls.getSelectedLocation().locationID;
    // }
    // else {
    //   this.service.getAllLocations(this.ls.getSelectedBrand().brandID).subscribe((res: any) => {
    //     debugger
    //     if (res.length > 0) {
    //       this.ls.setLocation(res);
    //       this.ls.setSelectedLocation(res[0]);
    //       this.locationID =res[0].locationID;
    //       this.Locations =res;
    //     }
    //     else {
    //       this.router.navigate(['/']);
    //     }
    //   });
    // }
    this.Locations = this.ls.getLocation();
    this.locationID = this.ls.getSelectedLocation().locationID;

  }
  changeloc(LocObj) {

    this.locationID = this.ls.selectedLocation().locationID;
  }
}
