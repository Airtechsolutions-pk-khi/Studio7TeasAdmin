using BAL.Repositories;
using StudioAdmin._Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudioAdmin.BLL._Services
{
    public class reportService : baseService
    {
        reportDB _service;
        public reportService()
        {
            _service = new reportDB();
        }

        public List<salesSummarytBLL> GetSalesSummaryRpt(int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetSalesSummaryRpt(brandID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<salesSummarytBLL>();
            }
        }
        public List<SalesDetailBLL> GetSalesDetailRpt(string locaitonID, int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetSalesDetailRpt(brandID, locaitonID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<SalesDetailBLL>();
            }
        }
        public List<SalesUserwiseBLL> GetSalesUserwiseRpt(string locaitonID, int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetSalesUserwiseRpt(brandID, locaitonID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<SalesUserwiseBLL>();
            }
        }
        public List<SalesItemwiseBLL> GetSalesItemwiseRpt(string locaitonID, int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetSalesItemwiseRpt( brandID, locaitonID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<SalesItemwiseBLL>();
            }
        }
        public List<SalesCustomerwiseBLL> GetSalesCustomerwiseRpt(string locaitonID, int brandID,int customerID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetSalesCustomerwiseRpt( brandID, locaitonID, customerID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<SalesCustomerwiseBLL>();
            }
        }
        public List<SalesCategorywiseBLL> GetSalesCategorywiseRpt(string locaitonID, int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetSalesCategorywiseRpt( brandID, locaitonID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<SalesCategorywiseBLL>();
            }
        }
        public List<DeliveryBoyDetailBLL> GetDeliveryBoyDetailRpt(int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetDeliveryBoyDetailRpt(brandID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<DeliveryBoyDetailBLL>();
            }
        }
       
        public List<OrderReportBLL> OrderReportByType(int brandID, int orderType, int locationId, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.OrderReportByType(brandID, orderType, locationId, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<OrderReportBLL>();
            }
        }
    }
}
