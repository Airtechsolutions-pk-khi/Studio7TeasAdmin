import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  selectedBrand: any;
  selectedLocation: any;

  constructor() {
    
  }


  setSelectedBrand(brand) {
    sessionStorage.setItem('_autheticatedUser', JSON.stringify(brand));
  }

  setSelectedLocation(location) {
    sessionStorage.setItem('selectedLocation', JSON.stringify(location));
  }
  getSelectedLocation() {
    return JSON.parse(sessionStorage.getItem('selectedLocation'));
  }
  setLocation(location) {
    sessionStorage.setItem('_Locations', JSON.stringify(location));
  }
  getLocation() {
    
    return JSON.parse(sessionStorage.getItem('_Locations'));
  }
  getSelectedBrand() {
    
    return JSON.parse(sessionStorage.getItem('_autheticatedUser'));
  }

  getSelectedUser() {
    let userInfo = JSON.parse(sessionStorage.getItem("currentUser"));
    if(userInfo !==null){
      userInfo = JSON.parse(userInfo.data);
      return userInfo;
    }
  }


  
}
