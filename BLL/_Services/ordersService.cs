using BAL.Repositories;
using StudioAdmin._Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudioAdmin.BLL._Services
{
    public class ordersService : baseService
    {

        ordersDB _service;
        locationDB _serviceLocation;
        public ordersService()
        {
            _service = new ordersDB();
            _serviceLocation = new locationDB();
        }

        public List<OrdersBLL> GetAll(string locaitonID, int brandID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetAll(brandID, locaitonID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<OrdersBLL>();
            }
        }

        public RspPrintReceipt GetPrint(int id, IWebHostEnvironment _env)
        {
            try
            {
                var order = Get(id, 0);
                var location = _serviceLocation.Get(order.Order.LocationID, 0);
                string[] filePaths = Directory.GetFiles(Path.Combine(_env.ContentRootPath, "Template/"));
                var content = System.IO.File.ReadAllText(filePaths.Where(x => x.Contains("receiptv3.txt")).FirstOrDefault());


                string itemsReceipt = "";
                foreach (var obj in order.OrderDetails.Where(x => x.StatusID == 201))
                {
                    double? ModifiersPrice = 0;
                    var ModifiersHtml = obj.OrderDetailModifiers.Count > 0 ? string.Join(", ", obj.OrderDetailModifiers.Select(x => x.ModifierName).ToList()) : "";

                    itemsReceipt += "<tr>";
                    itemsReceipt += "<td><p>" + obj.Name + ModifiersHtml + "</p></td>";
                    itemsReceipt += "<td><p>" + obj.Quantity + "</p></td>";
                    itemsReceipt += "<td><p>" + (obj.Price / obj.Quantity) + "</p></td>";
                    itemsReceipt += "<td><p>" + (obj.Price + ModifiersPrice) + "</p></td>";
                    itemsReceipt += "</tr>";
                }
                string customerReceiptData = "";
                var _dtocustomer = order.CustomerOrders;
                if (_dtocustomer != null)
                {
                    customerReceiptData += "<hr style='border-top:1px solid #000;border-bottom:1px solid #000;background: #000;' /><p>Customer Name: " + _dtocustomer.Name + "</br>";
                    //customerReceiptData += "Contact: " + _dtocustomer.Mobile + "</br>";
                    customerReceiptData += "Address & Contact: " + _dtocustomer.Address +" || "+ _dtocustomer.AddressNickName + "</br>";
                    customerReceiptData += "Email: " + _dtocustomer.Email + "</br>";
                    customerReceiptData += "</p>";
                }

                var _dtocheckout = order.OrderCheckouts;
                content = content
                .Replace("#orderno#", "<h4 style='text-align: center;border: 2px solid #000;font-weight: 800;font-size: 20px;width: 180px;padding: 5px;margin: auto;'>Order # " + order.Order.OrderNo + "</h4>")
                .Replace("#companyname#", "Contact: " +location.Name)
                .Replace("#contact#", location.ContactNo)
                .Replace("#email#", location.Email)
                .Replace("#address#", location.Address)
                .Replace("#footernotes#", "Thanyou for ordering.")

                .Replace("#transactionno#", order.Order.TransactionNo.ToString())
                .Replace("#orderitems#", itemsReceipt.ToString())
                .Replace("#orderdate#", Convert.ToDateTime(order.Order.OrderDate).ToString("dd/MM/yyyy hh:mm tt"))
                .Replace("#customerdata#", customerReceiptData)
                .Replace("#receipttype#", "TAX INVOICE")
                .Replace("#total#", _dtocheckout.AmountTotal == null ? "0.00" : String.Format("{0:0.00}", _dtocheckout.AmountTotal))
                .Replace("#discount#", _dtocheckout.DiscountAmount == null ? "0.00" : String.Format("{0:0.00}", _dtocheckout.DiscountAmount))
                .Replace("#tax#", _dtocheckout.Tax == null ? "0.00" : String.Format("{0:0.00}", _dtocheckout.Tax))
                .Replace("#deliverycharges#", _dtocheckout.ServiceCharges == null ? "0.00" : String.Format("{0:0.00}", _dtocheckout.ServiceCharges))
                .Replace("#grandtotal#", _dtocheckout.GrandTotal == null ? "0.000" : location.Currency + " " + String.Format("{0:0.00}", _dtocheckout.GrandTotal));


                return new RspPrintReceipt
                {
                    Status = 1,
                    HTML = content
                };
            }
            catch (Exception ex)
            {
                return new RspPrintReceipt
                {
                    Status = 0,
                    HTML = ""
                };
            }
        }
        //private string GetPdf(string html, int orderid)
        //{

        //    var htmlContent = html.ToString();
        //    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
        //    var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
        //    var filename = orderid + "-" + DateTime.Now.Ticks.ToString();
        //    string folderPath = "~/OrderLetters/";   // set folder

        //    //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folderPath)))
        //    //    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folderPath));
        //    //string FilePath = System.Web.HttpContext.Current.Server.MapPath(folderPath + filename + ".pdf");
        //    var bw = new BinaryWriter(System.IO.File.Open(""/*FilePath*/, FileMode.OpenOrCreate));
        //    bw.Write(pdfBytes);
        //    bw.Close();



        //    return folderPath + filename + ".pdf";
        //}
        public RspOrderDetail Get(int id, int brandID)
        {
            try
            {
                RspOrderDetail rsp = new RspOrderDetail();
                var lstOD = new List<OrderDetailBLL>();
                var lstODM = new List<OrderModifiersBLL>();
                var lstADN = new List<OrderAddonsBLL>();
                var oc = new OrderCheckoutBLL();
                var ocustomer = new OrderCustomerBLL();
                var bll = new List<OrdersBLL>();
                var db = new List<DeliveryBoyInfoBLL>();
                var ds = _service.Get(id, brandID);
                var _dsOrders = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<OrdersBLL>>();
                var _dsorderdetail = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<OrderDetailBLL>>();
                var _dsorderdetailmodifier = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[2])).ToObject<List<OrderModifiersBLL>>();
                var _dsorderdetailaddons = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[3])).ToObject<List<OrderAddonsBLL>>();
                var _dsOrdercheckout = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[4])).ToObject<List<OrderCheckoutBLL>>();
                var _dsOrderCustomerData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[5])).ToObject<List<OrderCustomerBLL>>();
                var _dsDeliveryBoyData = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[6])).ToObject<List<DeliveryBoyInfoBLL>>();

                foreach (var i in _dsOrders)
                {
                    lstOD = new List<OrderDetailBLL>();
                    foreach (var j in _dsorderdetail.Where(x => x.OrderID == i.OrderID))
                    {
                        lstODM = new List<OrderModifiersBLL>();
                        double? modPrice = 0;
                        double? adnPrice = 0;
                        foreach (var k in _dsorderdetailmodifier.Where(x => x.StatusID == 201 && x.OrderDetailID == j.OrderDetailID))
                        {
                            lstODM.Add(new OrderModifiersBLL
                            {
                                StatusID = k.StatusID,
                                Price = k.Price,
                                ModifierID = k.ModifierID,
                                Cost = k.Cost,
                                LastUpdateBy = k.LastUpdateBy,
                                LastUpdateDT = k.LastUpdateDT,
                                OrderDetailID = k.OrderDetailID,
                                OrderDetailModifierID = k.OrderDetailModifierID,
                                Quantity = k.Quantity,
                                ModifierName = k.ModifierName
                            });
                            modPrice += (j.Quantity * k.Price);


                        }
                        foreach (var l in _dsorderdetailaddons.Where(x => x.StatusID == 201 && x.OrderDetailID == j.OrderDetailID))
                        {
                            lstADN.Add(new OrderAddonsBLL
                            {
                                StatusID = l.StatusID,
                                Price = l.Price,
                                AddonID = l.AddonID,
                                Cost = l.Cost,
                                LastUpdateBy = l.LastUpdateBy,
                                LastUpdateDT = l.LastUpdateDT,
                                OrderDetailID = l.OrderDetailID,
                                OrderDetailAddonID = l.OrderDetailAddonID,
                                Quantity = l.Quantity,
                                AddonName = l.AddonName
                            });
                            adnPrice += (j.Quantity * l.Price);


                        }
                        lstOD.Add(new OrderDetailBLL
                        {
                            StatusID = j.StatusID,
                            Cost = j.Cost,
                            Price = j.Price + modPrice,
                            Quantity = j.Quantity,
                            OrderDetailID = j.OrderDetailID,
                            LastUpdateDT = j.LastUpdateDT,
                            LastUpdateBy = j.LastUpdateBy,
                            ItemID = j.ItemID,
                            Name = j.Name,
                            OrderDetailModifiers = lstODM,
                            OrderDetailAddons = lstADN,
                            OrderID = j.OrderID,
                            OrderMode = j.OrderMode
                        });
                    }


                    bll.Add(new OrdersBLL
                    {
                        StatusID = i.StatusID,
                        LastUpdateDT = i.LastUpdateDT,
                        LastUpdateBy = i.LastUpdateBy,
                        OrderID = i.OrderID,
                        CustomerID = i.CustomerID,
                        DeliverUserID = i.DeliverUserID,
                        LocationID = i.LocationID,
                        OrderDate = i.OrderDate,
                        OrderNo = i.OrderNo,
                        OrderTakerID = i.OrderTakerID,
                        OrderType = i.OrderType,
                        SessionID = i.SessionID,
                        TransactionNo = i.TransactionNo,
                        OrderDoneDate = i.OrderDoneDate,
                        OrderOFDDate = i.OrderOFDDate,
                        OrderPreparedDate = i.OrderPreparedDate,
                        DeliveryBoyID = i.DeliveryBoyID

                    });

                    rsp.Order = bll.FirstOrDefault();
                    rsp.OrderDetails = lstOD;
                    rsp.OrderCheckouts = _dsOrdercheckout.Where(x => x.OrderID == i.OrderID).FirstOrDefault();
                    rsp.CustomerOrders = _dsOrderCustomerData.Where(x => x.OrderID == i.OrderID).FirstOrDefault();
                    rsp.DeliveryBoyInfo = _dsDeliveryBoyData.Where(x => x.DeliveryBoyID == i.DeliveryBoyID).FirstOrDefault();
                }

                return rsp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(OrdersBLL data, IWebHostEnvironment _env)
        {
            try
            {
                //data.Image = UploadImage(data.Image, "Orders", _env);
                //data.LastUpdatedDate = _UTCDateTime_SA();
                //var result = _service.Insert(data);
                return 0;
                //return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(OrdersBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Update(data);
                if (data.StatusID == 102)
                {
                    try
                    {
                        var ds = _service.GetToken(data.CustomerID);
                        var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                        foreach (var item in getTokens)
                        {
                            var token = new PushNotificationBLL();
                            token.Title = "Studio7Teas" + " | Order Update";
                            token.Message = "Your Order Has Been confirmed";
                            token.DeviceID = item.Token;
                            _service.PushNotificationAndroid(token);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (data.StatusID == 104)
                {
                    try
                    {
                        var ds = _service.GetToken(data.CustomerID);
                        var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                        foreach (var item in getTokens)
                        {
                            var token = new PushNotificationBLL();
                            token.Title = "Studio7Teas" + " | Order Update";
                            token.Message = "Your Order is now out for Delivery";
                            token.DeviceID = item.Token;
                            _service.PushNotificationAndroid(token);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (data.StatusID == 100)
                {
                    try
                    {
                        var ds = _service.GetToken(data.CustomerID);
                        var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                        foreach (var item in getTokens)
                        {
                            var token = new PushNotificationBLL();
                            token.Title = "Studio7Teas" + " | Order Update";
                            token.Message = "Your Order Has Been Delivered Successfully";
                            token.DeviceID = item.Token;
                            _service.PushNotificationAndroid(token);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (data.StatusID == 103)
                {
                    try
                    {
                        var ds = _service.GetToken(data.CustomerID);
                        var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                        foreach (var item in getTokens)
                        {
                            var token = new PushNotificationBLL();
                            token.Title = "Studio7Teas" + " | Order Update";
                            token.Message = "Your Order Has Been Cancelled";
                            token.DeviceID = item.Token;
                            _service.PushNotificationAndroid(token);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Edit(OrdersEditBLL obj, IWebHostEnvironment _env)
        {
            obj.LastUpdatedDate = _UTCDateTime_SA();
            var result = _service.Edit(obj);

            return result;
        }
        public int Delete(OrdersBLL data)
        {
            try
            {
                data.LastUpdatedDate = _UTCDateTime_SA();
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
