import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { LocationsService } from 'src/app/_services/locations.service';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-addlocation',
  templateUrl: './addlocation.component.html',
  styleUrls: ['./addlocation.component.css']
})
export class AddlocationComponent implements OnInit {
  submitted = false;
  locationForm: FormGroup;
  loading = false;
  loadingLocations = false;
  ButtonText = "Save";
  opentime = { hour: new Date().getHours(), minute: new Date().getMinutes() };
  closetime = { hour: new Date().getHours(), minute: new Date().getMinutes() };
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private locationService: LocationsService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedLocations();
  }

  get f() { return this.locationForm.controls; }

  private createForm() {

    this.locationForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: [''],
      address: [''],
      deliveryServices: [false],
      deliveryCharges: [0],
      tax: [0],
      discounts: [0],
      minOrderAmount: [0],
      contactNo: [''],
      password: [''],
      currency: [''],
      latitude: [''],
      longitude: [''],
      description: [''],
      passcode: [''],
      statusID: [true],
      locationID: 0,
      opentime: [''],
      closetime: [''],
      brandID: this.ls.getSelectedBrand().brandID
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.email.setValue(obj.email);
    this.f.contactNo.setValue(obj.contactNo);
    this.f.minOrderAmount.setValue(obj.minOrderAmount);
    this.f.deliveryCharges.setValue(obj.deliveryCharges);
    this.f.tax.setValue(obj.tax);
    this.f.discounts.setValue(obj.discounts);
    this.f.address.setValue(obj.address);
    // this.f.password.setValue(obj.password);
    this.f.currency.setValue(obj.currency);
    this.f.latitude.setValue(obj.latitude);
    this.f.longitude.setValue(obj.longitude);
    this.f.description.setValue(obj.description);
    this.f.passcode.setValue(obj.passcode);
    this.f.locationID.setValue(obj.locationID);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);


    this.f.opentime.setValue(obj.opentime);
    this.f.closetime.setValue(obj.closetime);

    //this.opentime.hour = new Date("01/01/1900 " + obj.opentime).getHours();
    //this.opentime.minute = new Date("01/01/1900 " + obj.opentime).getMinutes();

    //this.closetime.hour = new Date("01/01/1900 " + obj.closetime).getHours();
    //this.closetime.minute = new Date("01/01/1900 " + obj.closetime).getMinutes();
    this.opentime = { hour: new Date("1/1/1900 " + obj.opentime).getHours(), minute: new Date("1/1/1900 " + obj.opentime).getMinutes() };
    this.closetime = { hour: new Date("1/1/1900 " + obj.closetime).getHours(), minute: new Date("1/1/1900 " + obj.closetime).getMinutes() };
  }

  setSelectedLocations() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingLocations = true;
        this.f.locationID.setValue(sid);
        this.locationService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingLocations = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.locationForm.markAllAsTouched();
    this.submitted = true;
    this.f.opentime.setValue(this.opentime.hour + ":" + this.opentime.minute);
    this.f.closetime.setValue(this.closetime.hour + ":" + this.closetime.minute);
    if (this.locationForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);

    if (parseInt(this.f.locationID.value) === 0) {
debugger
      //Insert location
      this.locationService.insert(this.locationForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/location']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update location
      this.locationService.update(this.locationForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/location']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }



}
