import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { BrandsService } from 'src/app/_services/brands.service';

@Component({
  selector: 'app-addbrand',
  templateUrl: './addbrand.component.html',
  styleUrls: ['./addbrand.component.css']
})
export class AddbrandComponent implements OnInit {
  submitted = false;
  brandForm: FormGroup;
  fileData: any;
  loading = false;
  loadingBrand = false;
  ButtonText = "Save";
  imageBgUrl: any = "";
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private brandService: BrandsService

  ) {
    this.createForm();
  }


  ngOnInit() {
    this.setSelectedBrand();
  }
  get f() { return this.brandForm.controls; }

  private createForm() {

    this.brandForm = this.formBuilder.group({

      name: ['', Validators.required],
      email: [''],
      password: [''],
      address: [''],
      companyURl: [''],
      currency: [''],
      statusID: [true],
      displayOrder: [0],
      brandID: 0,
      image: [''],
      locationID: null
    });
  }



  private editForm(obj) {

    this.f.name.setValue(obj.name);
    this.f.email.setValue(obj.email);
    this.f.password.setValue(obj.password);
    this.f.brandID.setValue(obj.brandID);
    this.f.image.setValue(obj.image);
    this.f.address.setValue(obj.address);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.companyURl.setValue(obj.companyURl);

    this.imageBgUrl = obj.imageUrl;
    this.imgComp.imageUrl = obj.image;
  }


  setSelectedBrand() {
   
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingBrand = true;
        this.f.brandID.setValue(sid);
        this.brandService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingBrand = false;
        });
      }
    })
  }

  onSubmit() {
    debugger
    this.brandForm.markAllAsTouched();
    this.submitted = true;

    if (this.brandForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.brandID.value) === 0) {

      //Insert brand
      console.log(JSON.stringify(this.brandForm.value));
      this.brandService.insert(this.brandForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/brand']);
        }

        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update brand
      this.brandService.update(this.brandForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/brand']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
  selectFile(event) {
    this.fileData = event.target.files[0];

    if (this.fileData.type == 'image/jpeg' || this.fileData.type == 'image/jpeg' || this.fileData.type == 'image/jpg') {
      debugger
      const reader = new FileReader();
      reader.readAsDataURL(this.fileData);
      reader.onload = () => {
        debugger
        this.imageBgUrl = reader.result;
        this.f.companyURl.setValue(this.imageBgUrl);

      };
    } else {
      alert("file type should be image")
      return;
    }

  }


}
