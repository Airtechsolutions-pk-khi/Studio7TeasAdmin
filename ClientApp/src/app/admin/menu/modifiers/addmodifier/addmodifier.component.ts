import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ModifiersService } from 'src/app/_services/modifiers.service';
import { ToastService } from 'src/app/_services/toastservice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-addmodifier',
  templateUrl: './addmodifier.component.html',
  styleUrls: ['./addmodifier.component.css']
})
export class AddmodifierComponent implements OnInit {
  submitted = false;
  modifierForm: FormGroup;
  loading = false;
  loadingmodifier = false;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public ts: ToastService,
    private ls: LocalStorageService,
    private modifierService: ModifiersService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedmodifier();
  }

  get f() { return this.modifierForm.controls; }

  private createForm() {
    this.modifierForm = this.formBuilder.group({
      name: ['', Validators.required],
      arabicName: [''],
      description: [''],
      statusID: [true],
      displayOrder: [0],
      price: [0],
      modifierID: 0,
      image: [''],
      brandID: this.ls.getSelectedBrand().brandID,
      locationID: null
    });
  }

  private editForm(obj) {
    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.price.setValue(obj.price);
    this.f.modifierID.setValue(obj.modifierID);
    this.f.image.setValue(obj.image);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedmodifier() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingmodifier = true;
        this.f.modifierID.setValue(sid);
        this.modifierService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingmodifier = false;
        });
      }
    })
  }

  onSubmit() {
    this.modifierForm.markAllAsTouched();
    this.submitted = true;
    if (this.modifierForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    if (parseInt(this.f.modifierID.value) === 0) {

      //Insert modifier
      console.log(JSON.stringify(this.modifierForm.value));
      this.modifierService.insert(this.modifierForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/modifier']);
        }
        // this.alertService.success("Item has been created");
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update modifier
      this.modifierService.update(this.modifierForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/modifier']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
}
