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