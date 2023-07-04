using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class ordersViewModel
    {
    }
    public class RspOrderDetail
    {
        public OrderCheckoutBLL OrderCheckouts { get; set; }
        public OrderCustomerBLL CustomerOrders { get; set; }
        public List<OrderDetailBLL> OrderDetails { get; set; }
        public DeliveryBoyInfoBLL DeliveryBoyInfo { get; set; }
        public OrdersBLL Order { get; set; }
    }
    public class RspPrintReceipt
    {
        public int Status { get; set; }
        public string HTML { get; set; }
    }
    public class DeliveryBoyInfoBLL
    {
        public int DeliveryBoyID { get; set; }
        public string DBName { get; set; }
        public string DBAddress { get; set; }
        public string DBContactNo { get; set; }
        public string DBVehicleNo { get; set; }
        public string DBCNICNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public int StatusID { get; set; }
        //public int DBAJuncID { get; set; }
        public int BrandID { get; set; }
        public string DBImage { get; set; }
        public int Amount { get; set; }

    }
    public class OrdersBLL
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? DeliveryBoyID { get; set; }
        public int TransactionNo { get; set; }
        public int OrderNo { get; set; }
        public string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusID { get; set; }
        public string SessionID { get; set; }
        public Nullable<int> OrderTakerID { get; set; }
        public Nullable<int> DeliverUserID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
        public int LocationID { get; set; }
        public Nullable<System.DateTime> OrderPreparedDate { get; set; }
        public Nullable<System.DateTime> OrderOFDDate { get; set; }
        public Nullable<System.DateTime> OrderDoneDate { get; set; }
        public int OrderCheckoutID { get; set; }
        public Nullable<int> PaymentMode { get; set; }
        public double AmountPaid { get; set; }
        public double AmountTotal { get; set; }
        public double GrandTotal { get; set; }
        public double ServiceCharges { get; set; }
        public double Tax { get; set; }
        public Nullable<System.DateTime> CheckoutDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }

        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public string LocationName { get; set; }

        
    }
    public class OrdersEditBLL1
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int? DeliveryBoyID { get; set; }
        public string CustomerAddress { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string OrderType { get; set; }
        //public List<OrderDetailBLL> OrderDetails { get; set; }
        //public OrdersBLL Order { get; set; }
    }
    public class OrdersEditBLL
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public int? DeliveryBoyID { get; set; }
        public string CustomerAddress { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string OrderType { get; set; }
        public List<OrderDetailBLL> OrderDetails { get; set; }
        public OrdersBLL Order { get; set; }
    }
    public partial class OrderDetailBLL
    {

        public int OrderDetailID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public string OrderMode { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
        public List<OrderAddonsBLL> OrderDetailAddons { get; set; }
        public List<OrderModifiersBLL> OrderDetailModifiers { get; set; }
    }
    public class OrderAddonsBLL
    {
        public int OrderDetailAddonID { get; set; }
        public Nullable<int> OrderDetailID { get; set; }
        public string AddonName { get; set; }
        public Nullable<int> AddonID { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
    }
    public class OrderModifiersBLL
    {
        public int OrderDetailModifierID { get; set; }
        public Nullable<int> OrderDetailID { get; set; }
        public string ModifierName { get; set; }
        public Nullable<int> ModifierID { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
    }
    public class OrderReportBLL
    {
        public int OrderID { get; set; }
        public int TransactionNo { get; set; }
        public int OrderNo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public int StatusID { get; set; }
    }
    public class OrderCheckoutBLL
    {
        public int OrderCheckoutID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> PaymentMode { get; set; }
        public Nullable<double> AmountPaid { get; set; }
        public Nullable<double> AmountTotal { get; set; }
        public Nullable<double> ServiceCharges { get; set; }
        public Nullable<double> DiscountAmount { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<System.DateTime> CheckoutDate { get; set; }
        public string SessionID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }
    public class OrderCustomerBLL
    {
        public int CustomerOrderID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LocationURL { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string AddressNickName { get; set; }
        public string AddressType { get; set; }
    }
    public partial class OrderDetailModifierBLL
    {
        public int OrderDetailModifierID { get; set; }
        public Nullable<int> OrderDetailID { get; set; }
        public Nullable<int> ModifierID { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
    }
    public partial class CustomerOrderBLL
    {
        public int CustomerOrderID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string LocationURL { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string AddressType { get; set; }
        public string AddressNickName { get; set; }
    }
}
