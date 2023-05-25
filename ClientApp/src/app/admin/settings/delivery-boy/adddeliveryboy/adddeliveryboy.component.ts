import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { DeliveryBoyService } from 'src/app/_services/deliveryBoy.service';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-adddeliveryboy',
  templateUrl: './adddeliveryBoy.component.html',
  styleUrls: ['./adddeliveryBoy.component.css']
})
export class AdddeliveryboyComponent implements OnInit {

  submitted = false;
  deliveryBoyForm: FormGroup;
  loading = false;
  loadingCustomer = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];
  BrandsList = [];
  selectedBrandIds: string[];
  selectedModifierIds: string[];
  private selectedBrand;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private deliveryBoyService: DeliveryBoyService

  ) 
  
  {
    this.createForm();
    this.loadBrands();
     this.selectedBrand =this.ls.getSelectedBrand();
  }
  

  ngOnInit() {
    this.setSelecteditem();
  }
  

 
  get f() { return this.deliveryBoyForm.controls; }

  private createForm() {
    this.deliveryBoyForm = this.formBuilder.group({
      dbName: ['', Validators.required],
      dbAddress: [''],
      dbContactNo: [''],
      dbVehicleNo: [''],
      dbCNICNo: [''],
      createdBy: [''],
      updatedBy: [''],
      deliveryBoyID: 0,
      brandID: this.ls.getSelectedBrand().brandID,
      statusID: 1,
      amount: null
    });
  }

  private editForm(obj) {
    debugger;
    this.f.dbName.setValue(obj.dbName);
    this.f.dbAddress.setValue(obj.dbAddress);
    this.f.dbContactNo.setValue(obj.dbContactNo);
    this.f.dbCNICNo.setValue(obj.dbcnicNo);
    this.f.dbVehicleNo.setValue(obj.dbVehicleNo);
    this.f.deliveryBoyID.setValue(obj.deliveryBoyID);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.brandID.setValue(this.ls.getSelectedBrand().brandID);
    this.f.amount.setValue(obj.amount);
    // debugger
    // if (obj.brands != "") {
    //   var stringToConvert = obj.brands;
    //   this.selectedBrandIds = stringToConvert.split(',').map(Number);
    //   this.f.brands.setValue(obj.brands);
    // }
  }

  setSelecteditem() {    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loading = true;
        this.f.deliveryBoyID.setValue(sid);
        this.deliveryBoyService.getById(sid).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loading = false;

          //BrandsFill
          // this.deliveryService.getBrands(this.ls.getSelectedBrand().brandID)
          // .subscribe((res: any) => {            
          //   var stringToConvert = res.items;
          //   this.selectedBrandIds = stringToConvert.split(',').map(Number);              
          // });

        });
      }
    })
  }

  onSubmit() {
    debugger;
    this.deliveryBoyForm.markAllAsTouched();
    this.submitted = true;
    if (this.deliveryBoyForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2); 
    this.f.createdBy.setValue("switch");
    this.f.updatedBy.setValue("switch");
    // this.f.brands.setValue(this.selectedBrandIds == undefined ? "" : this.selectedBrandIds.toString());

    if (parseInt(this.f.deliveryBoyID.value) === 0) {
      //Insert delivery
      this.deliveryBoyService.insert(this.deliveryBoyForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/deliveryBoy']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update delivery
      this.deliveryBoyService.update(this.deliveryBoyForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/deliveryBoy']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }

  private loadBrands() {
    
    this.deliveryBoyService.loadBrands(this.f.brandID).subscribe((res: any) => {
      this.BrandsList = res;
      // this.setSelecteditem();
    });
  }

}
