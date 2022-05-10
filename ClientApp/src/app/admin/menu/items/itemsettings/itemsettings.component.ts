import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ItemsService } from 'src/app/_services/items.service';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';


@Component({
  selector: 'app-itemsettings',
  templateUrl: './itemsettings.component.html',
  styleUrls: ['./itemsettings.component.css']
})

export class ItemsettingsComponent implements OnInit {
  submitted = false;
  itemsForm: FormGroup;
  loading = false;
  loadingItems = false;

  ItemsList = [];
  selectedItemIds: string[];
  itemSettingTitle:string;
  isItemSetting:boolean;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private itemsService: ItemsService

  ) {

    this.loadItems();
  
  }

  ngOnInit() {

  }

  setSelecteditem() {
    this.itemsService.getTodaysItems(this.ls.getSelectedBrand().brandID)
      .subscribe((res: any) => {
         
        var stringToConvert = res.items;
        this.isItemSetting = res.isItemSetting;
        this.itemSettingTitle = res.itemSettingTitle;
        this.selectedItemIds = stringToConvert.split(',').map(Number);
        
      });
  }

  onSubmit() {
    debugger;
    var obj = new Object();
    obj["Items"] = this.selectedItemIds.toString();
    obj["BrandID"] = this.ls.getSelectedBrand().brandID;
    obj["ItemSettingTitle"] = this.itemSettingTitle;
    obj["IsItemSetting"] = this.isItemSetting;
    this.loading = true;
    this.itemsService.updateSettings(obj).subscribe(data => {
      this.loading = false;
      if (data != 0) {
        this.ts.showSuccess("Success", "Updated successfully.")
        this.router.navigate(['/admin/item/settings']);
      }
    }, error => {
      this.ts.showError("Error", "Failed to update.")
      this.loading = false;
    });
  }

  private loadItems() {
    
    this.itemsService.loadItems(this.ls.getSelectedBrand().brandID).subscribe((res: any) => {
      this.ItemsList = res;
      this.setSelecteditem();
    });
  }
}
