import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { AddonsService } from 'src/app/_services/addons.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-addaddons',
  templateUrl: './addaddons.component.html',
  styleUrls: ['./addaddons.component.css']
})
export class AddaddonsComponent implements OnInit {
  submitted = false;
  addonForm: FormGroup;
  loading = false;
  loadingaddon = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private addonsService: AddonsService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedaddon();
  }

  get f() { return this.addonForm.controls; }

  private createForm() {
    this.addonForm = this.formBuilder.group({
      name: ['', Validators.required],
      arabicName: [''],
      description: [''],
      statusID: [true],
      displayOrder: [0],
      price: [0],
      addonID: 0,
      image: [''],
      brandID: this.ls.getSelectedBrand().brandID,
      locationID: null
    });
  }

  private editForm(obj) {
    debugger;
    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.price.setValue(obj.price);
    this.f.addonID.setValue(obj.addonID);
    this.f.image.setValue(obj.image);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedaddon() {
    debugger;
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingaddon = true;
        this.f.addonID.setValue(sid);
        this.addonsService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingaddon = false;
        });
      }
    })
  }

  onSubmit() {
    debugger;
    this.addonForm.markAllAsTouched();
    this.submitted = true;
    if (this.addonForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.addonID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.addonForm.value));
      this.addonsService.insert(this.addonForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/addons']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.addonsService.update(this.addonForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/addons']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}

