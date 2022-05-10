import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { DeliveryService } from 'src/app/_services/delivery.service';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-adddelivery',
  templateUrl: './adddelivery.component.html',
  styleUrls: ['./adddelivery.component.css']
})
export class AdddeliveryComponent implements OnInit {

  submitted = false;
  deliveryForm: FormGroup;
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
    private deliveryService: DeliveryService

  ) 
  
  {
    this.createForm();
    this.loadBrands();
     this.selectedBrand =this.ls.getSelectedBrand();
  }
  

  ngOnInit() {
    this.setSelecteditem();
  }
  

 
  get f() { return this.deliveryForm.controls; }

  private createForm() {
    this.deliveryForm = this.formBuilder.group({
      name: ['', Validators.required],
      amount: [''],
      statusID: [true],
      // brandIDs :[''],
      deliveryAreaID: 0,    
      brandID: this.ls.getSelectedBrand().brandID,
      brands:[],
    });
  }

  private editForm(obj) {
    this.f.name.setValue(obj.name);
    this.f.amount.setValue(obj.amount);
    this.f.deliveryAreaID.setValue(obj.deliveryAreaID);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);   
    debugger
    if (obj.brands != "") {
      var stringToConvert = obj.brands;
      this.selectedBrandIds = stringToConvert.split(',').map(Number);
      this.f.brands.setValue(obj.brands);
    }
  }

  setSelecteditem() {    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loading = true;
        this.f.deliveryAreaID.setValue(sid);
        this.deliveryService.getById(sid).subscribe(res => {
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
   
    this.deliveryForm.markAllAsTouched();
    this.submitted = true;
    if (this.deliveryForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);    
    this.f.brands.setValue(this.selectedBrandIds == undefined ? "" : this.selectedBrandIds.toString());

    if (parseInt(this.f.deliveryAreaID.value) === 0) {
      //Insert delivery
      this.deliveryService.insert(this.deliveryForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/delivery']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update delivery
      this.deliveryService.update(this.deliveryForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/delivery']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }

  private loadBrands() {
    
    this.deliveryService.loadBrands(this.f.brandID).subscribe((res: any) => {
      this.BrandsList = res;
      // this.setSelecteditem();
    });
  }

}
