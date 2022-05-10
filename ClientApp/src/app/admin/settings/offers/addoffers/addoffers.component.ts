import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { OffersService } from 'src/app/_services/offers.service';
import { NgbdDatepickerRangePopup } from 'src/app/datepicker-range/datepicker-range-popup';

@Component({
  selector: 'app-addoffers',
  templateUrl: './addoffers.component.html',
  styleUrls: ['./addoffers.component.css']
})
export class AddoffersComponent implements OnInit {

  submitted = false;
  offersForm: FormGroup;
  loading = false;
  loadingOffers = false;
  ButtonText = "Save"; 
  selectedSubCategoriesIds: string[];
  selectedLocationIds: string[];
  selectedgroupModifierIds: string[];
  @ViewChild(NgbdDatepickerRangePopup, { static: true }) _datepicker;
  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private offersService: OffersService

  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedOffers();
  }

  get f() { return this.offersForm.controls; }

  private createForm() {
    this.offersForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      statusID: [true],
      offerID: 0,
      image: [''],
      fromDate: [''],
      toDate: [''],
      brandID: this.ls.getSelectedBrand().brandID,
      locationID: null
    });
  }

  private editForm(obj) {
    debugger
    this.f.name.setValue(obj.name);
    this.f.description.setValue(obj.description);
    this.f.offerID.setValue(obj.offerID);
    this.f.fromDate.setValue(obj.fromDate);
    this.f.toDate.setValue(obj.toDate);
    this.f.image.setValue(obj.image);
    this.f.statusID.setValue(obj.statusID === 1 ? true : false);
    this.imgComp.imageUrl = obj.image;
  }

  setSelectedOffers() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingOffers = true;
        this.f.offerID.setValue(sid);
        this.offersService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingOffers = false;
        });
      }
    })
  }

  onSubmit() {
    this.offersForm.markAllAsTouched();
    this.submitted = true;
    if (this.offersForm.invalid) { return; }
    this.loading = true;
    this.f.statusID.setValue(this.f.statusID.value === true ? 1 : 2);
    this.f.image.setValue(this.imgComp.imageUrl);

    this.f.fromDate.setValue(this.parseDate(this._datepicker.fromDate));
    this.f.toDate.setValue(this.parseDate(this._datepicker.toDate));

    if (parseInt(this.f.offerID.value) === 0) {
      //Insert offers
      debugger
      this.offersService.insert(this.offersForm.value).subscribe(data => {
        if (data != 0) {
          this.ts.showSuccess("Success","Record added successfully.")
          this.router.navigate(['/admin/offers']);
        }
        this.loading = false;
      }, error => {
        this.ts.showError("Error","Failed to insert record.")
        this.loading = false;
      });
    } else {
      //Update offers
      this.offersService.update(this.offersForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success","Record updated successfully.")
          this.router.navigate(['/admin/offers']);
        }
      }, error => {
        this.ts.showError("Error","Failed to update record.")
        this.loading = false;
      });
    }
  }

  parseDate(obj) {
    return obj.year + "-" + obj.month + "-" + obj.day;;
  }

}
