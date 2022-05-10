import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CategoryService } from 'src/app/_services/category.service';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';

@Component({
  selector: 'app-addcategory',
  templateUrl: './addcategory.component.html',
  styleUrls: ['./addcategory.component.css']
})
export class AddcategoryComponent implements OnInit {


  submitted = false;
  categoryForm: FormGroup;

  loading = false;
  loadingCategory = false;
  ButtonText = "Save"; selectedCityIds
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts :ToastService,
    private categoryService: CategoryService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedCategory();
  }

  get f() { return this.categoryForm.controls; }

  private createForm() {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      arabicName: [''],
      description: [''],
      statusID: [true],
      displayOrder: [0],
      categoryID: 0,
      image: [''],
      brandID: this.ls.getSelectedBrand().brandID,
      locationID: null
    });
  }

  private editForm(obj) {

    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.categoryID.setValue(obj.categoryID);
    this.f.image.setValue(obj.image);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedCategory() {    
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCategory = true;
        this.f.categoryID.setValue(sid);
        this.categoryService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCategory = false;
        });
      }
    })
  }

  onSubmit() {
    
    this.categoryForm.markAllAsTouched();
    this.submitted = true;
    if (this.categoryForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);
    
    if (parseInt(this.f.categoryID.value) === 0) {
      //Insert category
      this.categoryService.insert(this.categoryForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/category']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update category
      this.categoryService.update(this.categoryForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/category']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }
}
