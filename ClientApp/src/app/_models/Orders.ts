export class Orders {
  customerID: number;
  orderNo: string;
  transactionNo: string;
  customerMobile: string;
  customerAddress: string;
  customerName: string;
  amountTotal: number;
  tax: number;
  serviceCharges: number;
  discountAmount: number;
  grandTotal: number;
  locationID: number;
  brandID: number;
  statusID: number;
  orderDate: string;
  orderType: string
  orderID: number;
  orderPreparedDate: string;
  orderOFDDate: string;
  deliveryBoyID: number;
}
export class DeliveryBoyOrders {
  deliveryBoyID: number
  dbName: string
  dbAddress: string
  dbContactNo: string
  dbVehicleNo: string
  dbcnicNo: string
  image: string
  createdOn: string
  createdBy: string
  updatedOn: string
  updatedBy: string
  statusID: number
  brandID: number
  amount: number
}
export class OrderDetailAddons {
  orderDetailAddonID: number;
  orderDetailID: number;
  addonID: number;
  quantity: number;
  price: number;
  cost: number;
  statusID: number;
}
export class OrderDetails {
  orderDetailID: number;
  orderID: number;
  name: string;
  itemID: number;
  quantity: number;
  price: number;
  cost: number;
  statusID: number;
  orderDetailModifier: OrderDetailModifiers[]
}

export class OrderDetailModifiers {
  orderDetailModifierID: number;
  orderDetailID: number;
  orderID: number;
  modifierID: number;
  quantity: number;
  price: number;
  cost: number;
  modifierName: string;
  statusID: number;
}
export class OrderCheckout {
  orderCheckoutID: number;
  orderID: string;
  paymentMode: string;
  amountPaid: string;
  amountTotal: string;
  tax: number;
  serviceCharges: number;
  discountAmount: number;
  grandTotal: number;
  checkoutDate: string;
}
export class CustomerOrders {
  customerOrderID: number;
  name: string;
  email: string;
  mobile: string;
  description: string;
  address: string;
  longitude: string;
  latitude: string;
  locationURL: string;
  addressNickName: string;
  addressType: string;
}