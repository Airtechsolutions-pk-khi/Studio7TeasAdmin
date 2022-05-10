import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './admin/dashboard/dashboard.component';
import { LayoutComponent } from './layout/layout.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoryComponent } from './admin/menu/category/category.component';
import { AddcategoryComponent } from './admin/menu/category/addcategory/addcategory.component';
import { ImageuploadComponent } from './imageupload/imageupload.component';
import { ItemsComponent } from './admin/menu/items/items.component';
import { AdditemsComponent } from './admin/menu/items/additem/additem.component';
import { ModifiersComponent } from './admin/menu/modifiers/modifiers.component';
import { AddmodifierComponent } from './admin/menu/modifiers/addmodifier/addmodifier.component';
import { CustomersComponent } from './admin/customer/customers/customers.component';
import { AddcustomerComponent } from './admin/customer/customers/addcustomers/addcustomer.component';
import { LocationsComponent } from './admin/company/locations/locations.component';
import { AddlocationComponent } from './admin/company/locations/addlocation/addlocation.component';
import { NgSelectModule} from '@ng-select/ng-select';
import { AddbrandComponent } from './admin/company/brands/addbrand/addbrand.component';

import { NgApexchartsModule } from 'ng-apexcharts';
import { ToastrModule } from 'ngx-toastr';
import { BrandComponent } from './admin/company/brands/brands.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SummaryComponent } from './admin/report/summary/summary.component';
import { NgbdDatepickerRangePopup } from './datepicker-range/datepicker-range-popup';
import { BannerComponent } from './admin/settings/banner/banner.component';
import { AddbannerComponent } from './admin/settings/banner/addbanner/addbanner.component';
import { SalesdetailComponent } from './admin/report/salesdetail/salesdetail.component';
import { SalesuserwiseComponent } from './admin/report/salesuserwise/salesuserwise.component';
import { SalescustomerwiseComponent } from './admin/report/salescustomerwise/salescustomerwise.component';
import { SalescategorywiseComponent } from './admin/report/salescategorywise/salescategorywise.component';
import { SalesitemwiseComponent } from './admin/report/salesitemwise/salesitemwise.component';
import { OffersComponent } from './admin/settings/offers/offers.component';
import { AddoffersComponent } from './admin/settings/offers/addoffers/addoffers.component';
import { OrdersComponent } from './admin/sales/orders/orders.component';
import { OrderdetailsComponent } from './admin/sales/orderdetails/orderdetails.component';
import { ItemsettingsComponent } from './admin/menu/items/itemsettings/itemsettings.component';
import { ExcelService } from 'src/ExportExcel/excel.service';
import { DeliveryComponent } from './admin/settings/delivery/delivery.component';
import { AdddeliveryComponent } from './admin/settings/Delivery/adddelivery/adddelivery.component';
import { AppsettingsComponent } from './admin/settings/appsettings/appsettings.component';
import { AddonsComponent } from './admin/menu/addons/addons.component';
import { AddaddonsComponent } from './admin/menu/addons/addaddons/addaddons.component';



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DashboardComponent,
    LayoutComponent,
    CounterComponent,
    LoginComponent,
    FetchDataComponent,
    CategoryComponent,
    AddcategoryComponent,
    ItemsComponent,
    AdditemsComponent,
    ModifiersComponent,
    AddmodifierComponent,
    CustomersComponent,
    AddcustomerComponent,
    BrandComponent,
    AddbrandComponent,
    LocationsComponent,
    AddlocationComponent,
    ImageuploadComponent,
    SummaryComponent,
    NgbdDatepickerRangePopup,
    BannerComponent,
    AddbannerComponent,
    OffersComponent,
    AddoffersComponent,
    SalesdetailComponent,
    SalescategorywiseComponent,
    SalescustomerwiseComponent,
    SalesitemwiseComponent,
    SalesuserwiseComponent,
    OrdersComponent,
    OrderdetailsComponent,
    ItemsettingsComponent,
    DeliveryComponent,
    AdddeliveryComponent,
    AppsettingsComponent,
    AddonsComponent,
    AddaddonsComponent,
    
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgApexchartsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      {
        path: 'admin', component: LayoutComponent,
        children: [
          { path: 'dashboard', component: DashboardComponent },
          { path: 'category', component: CategoryComponent },
          { path: 'category/add', component: AddcategoryComponent },
          { path: 'category/edit/:id', component: AddcategoryComponent },

          { path: 'item', component: ItemsComponent },
          { path: 'item/add', component: AdditemsComponent },
          { path: 'item/settings', component: ItemsettingsComponent },
          { path: 'item/edit/:id', component: AdditemsComponent },

          { path: 'modifier', component: ModifiersComponent },
          { path: 'modifier/add', component: AddmodifierComponent },
          { path: 'modifier/edit/:id', component: AddmodifierComponent },

          { path: 'orders', component: OrdersComponent },
          { path: 'orders/add', component: OrderdetailsComponent },
          { path: 'orders/view/:id', component: OrderdetailsComponent },

          { path: 'customer', component: CustomersComponent },
          { path: 'customer/add', component: AddcustomerComponent },
          { path: 'customer/edit/:id', component: AddcustomerComponent },

          { path: 'location', component: LocationsComponent },
          { path: 'location/add', component: AddlocationComponent },
          { path: 'location/edit/:id', component: AddlocationComponent },

          { path: 'brand', component: BrandComponent },
          { path: 'brand/add', component: AddbrandComponent },
          { path: 'brand/edit/:id', component: AddbrandComponent },

          { path: 'banner', component: BannerComponent },
          { path: 'banner/add', component: AddbannerComponent },
          { path: 'banner/edit/:id', component: AddbannerComponent },

          { path: 'offers', component: OffersComponent },
          { path: 'offers/add', component: AddoffersComponent },
          { path: 'offers/edit/:id', component: AddoffersComponent },

          { path: 'report/summary', component: SummaryComponent },
          { path: 'report/salesdetail', component: SalesdetailComponent },
          { path: 'report/salesuserwise', component: SalesuserwiseComponent },
          { path: 'report/salescustomerwise', component: SalescustomerwiseComponent },
          { path: 'report/salescategorywise', component: SalescategorywiseComponent },
          { path: 'report/salesitemwise', component: SalesitemwiseComponent },

          { path: 'delivery', component: DeliveryComponent },
          { path: 'delivery/add', component: AdddeliveryComponent },
          { path: 'delivery/edit/:id', component: AdddeliveryComponent },

          { path: 'appsettings', component: AppsettingsComponent },

          { path: 'addons', component: AddonsComponent },
          { path: 'addons/add', component: AddaddonsComponent },
          { path: 'addons/edit/:id', component: AddaddonsComponent },
       
        ]
      }
    ]),
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    NgbModule
  ],
  providers: [],
  exports: [NgbdDatepickerRangePopup],
  bootstrap: [AppComponent,NgbdDatepickerRangePopup]
})
export class AppModule { }
