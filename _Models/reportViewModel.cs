using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin._Models
{
    public class reportViewModel
    {
    }

    public class salesSummarytBLL
    {
        public float TotalSales { get; set; }
        public float TotalTax { get; set; }
        public float TotalDiscount { get; set; }
        public float TotalReturn { get; set; }
        public float TotalNetSales { get; set; }
        public int TotalSalesOrders { get; set; }
        public int TotalDeliveryOrders { get; set; }

        public int TotalPickUpOrders { get; set; }
        public int TotalCancelOrders { get; set; }

    }
    public class SalesDetailBLL
    {
        public int OrderNo { get; set; }
        public int TransactionNo { get; set; }
        public float GrandTotal { get; set; }
        public float Tax { get; set; }
        public float ServiceCharges { get; set; }
        public float AmountTotal { get; set; }
        public float DiscountAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public int CustomerID { get; set; }
        public int StatusID { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate{ get; set; }

    }
    public class SalesUserwiseBLL
    {
        public int OrderNo { get; set; }
        public int TransactionNo { get; set; }
        public string customerName { get; set; }
        public string customerContact { get; set; }
        public DateTime orderDate { get; set; }
        public int statusID { get; set; }
        public float amountTotal { get; set; }

        public int orderID { get; set; }

    }
    public class SalesCustomerwiseBLL
    {
        public int OrderNo { get; set; }
        public int TransactionNo { get; set; }
        public float GrandTotal { get; set; }
        public float Tax { get; set; }
        public float ServiceCharges { get; set; }
        public float AmountTotal { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public int CustomerID { get; set; }
        public int StatusID { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

    }
    public class SalesItemwiseBLL
    {
        public string ItemName { get; set; }
        public int ItemID { get; set; }
        public float? Quantity { get; set; }
        public float? Cost { get; set; }
        public float? Price { get; set; }
        public float? Profit { get; set; }
    }
    public class SalesCategorywiseBLL
    {
        public string CategoryName { get; set; }
        public int ItemID { get; set; }
        public float? Quantity { get; set; }
        public float? Cost { get; set; }
        public float? Price { get; set; }
        public float? Profit { get; set; }

    }
    public class DeliveryBoyDetailBLL
    {
        public string DBName { get; set; }
        public string DBAddress { get; set; }
        public string DBContactNo { get; set; }
        public string DBVehicleNo { get; set; }
        public string DBCNICNo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? BrandID { get; set; }
        public int? StatusID { get; set; }
        public int? Amount { get; set; }

    }
}
