import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ItemsService } from 'src/app/_services/items.service';
import { ImageuploadComponent } from 'src/app/imageupload/imageupload.component';
import { Router, ActivatedRoute } from '@angular/router';
import { LocalStorageService } from 'src/app/_services/local-storage.service';
import { ToastService } from 'src/app/_services/toastservice';
import { OrdersService } from 'src/app/_services/orders.service';
//import { CustomerOrders, DeliveryBoyOrders, OrderCheckout, OrderDetailAddons, OrderDetailModifiers, OrderDetails, Orders } from '../../../_models/Orders';
import { Delivery } from '../../../_models/Delivery';
import { CustomerOrders, DeliveryBoyOrders, OrderCheckout, OrderDetailAddons, OrderDetailModifiers, OrderDetails, Orders } from 'src/app/_models/Orders';
//import { debug } from 'console';

@Component({
  selector: 'app-editorder',
  templateUrl: './editorder.component.html',
  styleUrls: ['./editorder.component.css']
})

export class EditOrderComponent implements OnInit {
  submitted = false;
  orderForm: FormGroup;
  loading = false;
  loadingCategory = false;
  Categories = [];
  Addons = [];
  ItemList = [];
  ModifiersList = [];
  OrderDetailList = [];
  OrderModifierList = [];
  DeliveryBoys = [];
  selectedModifierIds: string[];
  private selectedBrand;
  public order = new Orders();
  public orderDetails = new OrderDetails();
  public orderDetailModifiers = new OrderDetailModifiers();
  public orderDetailAddons = new OrderDetailAddons();
  public deliveryBoyOrder = new DeliveryBoyOrders();
  public orderOrderCheckout = new OrderCheckout();
  public orderCustomerInfo = new CustomerOrders();

  @ViewChild(ImageuploadComponent, { static: true }) imgComp;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private ls: LocalStorageService,
    public ts: ToastService,
    private orderService: OrdersService

  ) {
    this.selectedBrand = this.ls.getSelectedBrand().brandID;
    this.loadCategory();
    this.loadAddons();
    this.GetAllDeliveryBoys();
    this.createForm();
  }

  ngOnInit() {
    this.setSelectedorder();
  }

  get f() { return this.orderForm.controls; }

  private createForm() {
    this.orderForm = this.formBuilder.group({
      customerMobile: [''],
      customerAddress: [''],
      customerName: [''],
      amountTotal: [],
      tax: [],
      serviceCharges: [],
      discountAmount: [],
      grandTotal: [],
      locationID: [],
      brandID: [],
      statusID: [],
      orderDate: [''],
      orderType: [''],
      orderID: [],
      deliveryBoyID: [],
      orderDetails: [],
      orderDetailModifiers: [],
      orderDetailAddons: []
    });
  }
  private editForm(obj) {
    debugger
    this.OrderDetailList = obj.orderDetails;
    //this.ModifiersList = obj.orderDetails[0].orderDetailModifiers;
    this.f.customerName.setValue(obj.customerOrders.name);
    this.f.customerMobile.setValue(obj.customerOrders.mobile);
    this.f.customerAddress.setValue(obj.customerOrders.addressNickName);
    this.f.orderType.setValue(obj.order.orderType);
    this.f.orderDate.setValue(obj.order.orderDate);
    this.f.statusID.setValue(obj.order.statusID);
    this.f.orderDetails.setValue(this.OrderDetailList);
    //this.f.orderDetails[0].orderDetailModifiers.setValue(this.ModifiersList);
    this.order.deliveryBoyID === null || this.order.deliveryBoyID === undefined ? this.order.deliveryBoyID = 0 : console.log('ok');
  }
  GetAllDeliveryBoys() {
    this.orderService.getDeliveryBoys(this.selectedBrand).subscribe(data => {
      if (Object.keys(data).length > 0) {
        this.DeliveryBoys = data;
      }
    })
  }
  setSelectedorder() {
    this.route.paramMap.subscribe(param => {
      const sid = +param.get('id');
      if (sid) {
        this.loadingCategory = true;
        this.f.orderID.setValue(sid);
        this.orderService.getById(sid, this.f.brandID.value).subscribe(res => {
          //Set Forms
          this.editForm(res);
          this.loadingCategory = false;
        });
      }
    })
  }
  private loadCategory() {
    this.orderService.loadCategory().subscribe((res: any) => {
      this.Categories = res;
    });
  }
  private loadAddons() {
    debugger
    this.orderService.loadAddon().subscribe((res: any) => {
      this.Addons = res;
    });
  }


  onChange(event) {
    let selectElementValue = event.target.value;
    let [index, value] = selectElementValue.split(':').map(item => item.trim());

    this.orderService.loadItems(value).subscribe((res: any) => {
      this.ItemList = res;
    });
  }
  onSelect(newValue) {
    this.orderService.loadModifiers(newValue).subscribe((res: any) => {      
      this.ModifiersList = res;
    });     
  }
  onSubmit() {
    
    this.orderForm.markAllAsTouched();
    this.submitted = true;
    if (this.orderForm.invalid) { return; }
    this.loading = true;
    this.f.orderDetails.setValue(this.OrderDetailList);
    if (parseInt(this.f.orderID.value) === 0) {


    } else {
      
      //Update order
      this.orderService.updateOrder(this.orderForm.value).subscribe(data => {
        this.loading = false;
        if (data != 0) {
          this.ts.showSuccess("Success", "Record updated successfully.")
          this.router.navigate(['/admin/orders']);
        }
      }, error => {
        this.ts.showError("Error", "Failed to update record.")
        this.loading = false;
      });
    }
  }
  RemoveChild(obj) {
    const index = this.OrderDetailList.indexOf(obj);
    this.OrderDetailList.splice(index, 1);
  }

  AddChild(val) {
    debugger
    var obj = this.ItemList.find(element => element.itemID == val.itemID);
    if (val.itemID != null) {
      var addon = [];
      var addontotal = 0;
      if (val.addon != undefined) {
        val.addon.forEach(ele => {
          var obj1 = this.Addons.find(i => i.addonID == ele);
          addon.push({
            name: obj1.name,
            price: obj1.price == null ? 0 : obj1.price,
            quantity: val.quantity == null ? 1 : val.quantity,
            total: val.quantity * obj1.price,
            addonID: obj1.addonID,
          });
          addontotal += val.quantity * obj1.price;
        });
      }
      var modifier = [];
      var modifiertotal = 0;
      if (val.modifier != undefined) {        
          var obj2 = this.ModifiersList.find(j => j.modifierID == val.modifier);
          modifier.push({
            name: obj2.name,
            price: obj2.price == null ? 0 : obj2.price,
            quantity: val.quantity == null ? 1 : val.quantity,
            total: val.quantity * obj2.price,
            addonID: obj2.addonID,
          });
          modifiertotal += val.quantity * obj2.price;        
      }
      this.OrderDetailList.push({
        name: obj.name,
        price: obj.price == null ? 0 : obj.price,
        quantity: val.quantity == null ? 1 : val.quantity,
        total: ((val.quantity * obj.price) + modifiertotal + addontotal),
        itemID: obj.itemID,
        image: obj.image == null ? 0 : obj.image,
        orderDetailAddons: addon,
        orderDetailModifiers: modifier
      });

      this.clear();
      this.orderDetails.quantity = 1;
      this.orderDetails.price = 0;
    }
  }
  clear() {
    this.orderDetails.quantity = 1;
    this.orderDetails.price = 0;

  }
}
