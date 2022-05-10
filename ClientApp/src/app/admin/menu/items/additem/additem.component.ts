import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ItemsService } from 'src/app/_services/items.service';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
  selector: 'app-additem',
  templateUrl: './additem.component.html',
  styleUrls: ['./additem.component.css']
})

export class AdditemsComponent implements OnInit {
  submitted = false;
  itemsForm: FormGroup;
  loading = false;
  loadingItems = false;
  Categories = [];
  ModifiersList = [];
  AddonsList = [];
  selectedModifierIds: string[];
  selectedAddonIds: string[];

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private itemsService: ItemsService

  ) {
    this.createForm();
    this.loadCategory();
    this.loadModifiers();
    this.loadAddons();
  }

  ngOnInit() {
    this.setSelecteditem();
  }

  get f() { return this.itemsForm.controls; }

  private createForm() {
    this.itemsForm = this.formBuilder.group({
      name: ['', Validators.required],
      arabicName: [''],
      description: [''],
      statusID: [true],
      isFeatured: [false],
      displayOrder: [0],
      cost: [0],
      categoryID: [null],
      price: [0, Validators.required],
      itemID: 0,
      calories: [0, Validators.required],
      image: [''],
      brandID: this.ls.getSelectedBrand().brandID,
      locationID: null,
      modifiers: [],
      addons: [],
    });
  }

  private editForm(obj) {

    this.f.name.setValue(obj.name);
    this.f.arabicName.setValue(obj.arabicName);
    this.f.displayOrder.setValue(obj.displayOrder);
    this.f.price.setValue(obj.price);
    this.f.cost.setValue(obj.cost);
    this.f.calories.setValue(obj.calories);
    this.f.itemID.setValue(obj.itemID);

    debugger
    if (obj.modifiers != "") {
      var stringToConvert = obj.modifiers;
      this.selectedModifierIds = stringToConvert.split(',').map(Number);
      this.f.modifiers.setValue(obj.modifiers);
    }
    debugger
    if (obj.addons != "") {
      var stringToConvert = obj.addons;
      this.selectedAddonIds = stringToConvert.split(',').map(Number);
      this.f.addons.setValue(obj.addons);
    }
    this.f.categoryID.setValue(obj.categoryID);
    this.f.image.setValue(obj.image);
    this.f.description.setValue(obj.description);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.f.isFeatured.setValue(obj.isFeatured);
    this.imgComp.imageUrl = obj.image;

  }

  setSelecteditem() {
    debugger
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingItems = true;
        this.f.itemID.setValue(sid);
        this.itemsService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingItems = false;
        });
      }
    })
  }

  onSubmit() {

    this.itemsForm.markAllAsTouched();
    this.submitted = true;
    if (this.itemsForm.invalid) { return; }
    this.loading = true;
    this.f.modifiers.setValue(this.selectedModifierIds == undefined ? "" : this.selectedModifierIds.toString());
    this.f.addons.setValue(this.selectedAddonIds == undefined ? "" : this.selectedAddonIds.toString());
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    if (parseInt(this.f.itemID.value) === 0) {

      //Insert item
      console.log(JSON.stringify(this.itemsForm.value));
      this.itemsService.insert(this.itemsForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success", "Record added successfully.")
          this.router.navigate(['/admin/item']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error", "Failed to insert record.")
        this.loading = false;
      });

    } else {
      //Update item
      this.itemsService.update(this.itemsForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/item']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }

  private loadCategory() {
    this.itemsService.loadCategories(this.f.brandID.value).subscribe((res: any) => {
     
      this.Categories = res;
    });
  }
  private loadModifiers() {    
    this.itemsService.loadModifierList(this.f.brandID.value).subscribe((res: any) => {
      this.ModifiersList = res;
    });
  }
  private loadAddons() {    
    this.itemsService.loadAddonList(this.f.brandID.value).subscribe((res: any) => {
      this.AddonsList = res;
    });
  }
}
