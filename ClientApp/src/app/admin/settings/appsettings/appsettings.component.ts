import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastService } from 'src/app/_services/toastservice';
import { AppsettingService } from 'src/app/_services/appsetting.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';

@Component({
  selector: 'app-appsettings',
  templateUrl: './appsettings.component.html',
  styleUrls: ['./appsettings.component.css']
})
export class AppsettingsComponent implements OnInit {
  submitted = false;
  categoryForm: FormGroup;
  loadingCategory = false;  
  loading = false;
  constructor(  
    public ts :ToastService,
    private appsettingService: AppsettingService,
    private router: Router,
    private route: ActivatedRoute,    
    private formBuilder: FormBuilder,
    private ls: LocalStorageService
    ) { 
      this.createForm();
     
      brandID: this.ls.getSelectedBrand().brandID;
      this.setSelectedCategory();
  }

  ngOnInit(): void {
  }
 
 

  get f() { return this.categoryForm.controls; }
  
  private createForm() {
   
    this.categoryForm = this.formBuilder.group({
      branchName: ['', Validators.required],
      branchAddress: [''],
      branchTiming: [''],
      statusID: [true],
      deliveryNo: [''],
      whatsappNo:[''],
      discount: 0,
      discountdescription:[''],          
      brandID: this.ls.getSelectedBrand().brandID,
      ID: 0,
      AppInfoID: 0,
      appDescription:[''],     
      facebook:[''],
      twitter:[''],
      instagram:[''],
    });
  }

  private editForm(obj) {

    this.f.branchName.setValue(obj.branchName);
    this.f.branchAddress.setValue(obj.branchAddress);
    this.f.branchTiming.setValue(obj.branchTiming);
    this.f.deliveryNo.setValue(obj.deliveryNo);
    this.f.discount.setValue(obj.discount);
    this.f.discountdescription.setValue(obj.discountdescription);
    this.f.whatsappNo.setValue(obj.whatsappNo);
    this.f.appDescription.setValue(obj.appDescription);
    this.f.facebook.setValue(obj.facebook);
    this.f.twitter.setValue(obj.twitter);
    this.f.instagram.setValue(obj.instagram);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.ID.setValue(obj.id);
    this.f.appInfoID.setValue(obj.appInfoID);
  }

  setSelectedCategory() {    
    this.loadingCategory = true;
    this.appsettingService.getById(this.f.brandID.value).subscribe(res => {
      //Set Forms
      this.editForm(res);
      this.loadingCategory = false;
    });
  }
  onSubmit() {
    this.categoryForm.markAllAsTouched();
    this.submitted = true;
    if (this.categoryForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);    
    debugger;
    if (parseInt(this.f.ID.value) === 0) {
      //Insert appsetting
      this.appsettingService.insert(this.categoryForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/appsettings']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update appsetting
      this.appsettingService.update(this.categoryForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/appsettings']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }
}
