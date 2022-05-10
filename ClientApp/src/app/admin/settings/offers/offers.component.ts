import { Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { NgbdSortableHeader, SortEvent } from 'src/app/_directives/sortable.directive';

import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { Router } from '@angular/router';
import { Offers } from 'src/app/_models/Offers';
import { ToastService } from 'src/app/_services/toastservice';
import { OffersService } from 'src/app/_services/offers.service';

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  providers: []
})

export class OffersComponent implements OnInit {
  data$: Observable<Offers[]>;
  oldData: Offers[];
  total$: Observable<number>;
  loading$: Observable<boolean>;
  private selectedBrand;

  locationSubscription: Subscription;
  submit: boolean;
  @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

  constructor(public service: OffersService,
    public ls: LocalStorageService,
    public ts: ToastService,
    public router: Router) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;

    this.loading$ = service.loading$;
    this.submit = false;

  }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.service.getAllData(this.selectedBrand);
    this.data$ = this.service.data$;
    this.total$ = this.service.total$;
    this.loading$ = this.service.loading$;
  }

  onSort({ column, direction }: SortEvent) {

    this.headers.forEach(header => {
      if (header.sortable !== column) {
        header.direction = '';
      }
    });
    this.service.sortColumn = column;
    this.service.sortDirection = direction;
  }

  Edit(offers) {
    this.router.navigate(["admin/offers/edit", offers]);
  }

  Delete(obj) {
    this.service.delete(obj).subscribe((res: any) => {
      if (res != 0) {
        this.ts.showSuccess("Success", "Record deleted successfully.")
        this.getData();
      }
      else
        this.ts.showError("Error", "Failed to delete record.")

    }, error => {
      this.ts.showError("Error", "Failed to delete record.")
    });
  }

}
