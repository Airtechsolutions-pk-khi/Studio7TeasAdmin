using System.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositories
{
    public class baseDB
        //: IDisposable
    {

        //StreamWriter _sw;
        //public DB_A35721_lunchboxDBEntities DBContext;
        //public BaseRepository()
        //{
        //    DBContext = new DB_A35721_lunchboxDBEntities();
        //}

        //public BaseRepository(DB_A35721_lunchboxDBEntities ContextDB)
        //{
        //    DBContext = ContextDB;
        //}

        //public void SaveChanges()
        //{
        //    DBContext.SaveChanges();
        //}

        //#region IDisposable Support

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (DBContext != null)
        //        {
        //            DBContext.Dispose();
        //            DBContext = null;

        //        }
        //    }
        //}

        //// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~BaseRepository()
        //{
        //    Dispose();
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //#endregion

        //public void ErrorLog(Exception e, string FnName, string FileName)
        //{
        //    try
        //    {


        //        //ErrorLog1 Log = new ErrorLog1();
        //        //Log.Errorin = FnName + " : " + e.InnerException;
        //        //Log.ErrorMessage = e.Message;
        //        //Log.CreatedDate = DateTime.UtcNow;
        //        ////Log.UserID = 2;
        //        ////Log.CreatedBy= 2;
        //        //DBContext.ErrorLogs1.Attach(Log);
        //        //DBContext.ErrorLogs1.Add(Log);
        //        //DBContext.SaveChanges();
        //        ////function
        //        //LogWrite(Log.ErrorMessage, FileName);
        //    }
        //    catch
        //    {
        //    }
        //}
        //public void LogWrite(string msg, string fileName)
        //{
        //    //var logPath = ConfigurationManager.AppSettings["LogPath"];
        //    //_sw = new StreamWriter(@logPath + fileName + DateTime.UtcNow.ToString("yyyyMMdd") + ".txt", true);

        //    _sw.WriteLine(DateTime.UtcNow.ToLongTimeString() + " " + msg);
        //    _sw.Close();
        //}

        ////public string CurrentDate(SessionInfo session)
        ////{
        ////    #region timmings

        ////    DateTime t1 = DateTime.UtcNow.AddMinutes(session.UTC);
        ////    DateTime t2 = Convert.ToDateTime(session.OpenTime.ToString());
        ////    DateTime t3 = Convert.ToDateTime(session.CloseTime.ToString());

        ////    string startday;

        ////    TimeSpan diff = t3 - t2;

        ////    DateTime NewDate = t2.AddHours(diff.Hours <= 0 ? (24 - (-diff.Hours)) : diff.Hours);

        ////    if (t3.Date != NewDate.Date)
        ////    {
        ////        int b = DateTime.Compare(t1, t3);

        ////        if (b == 1)
        ////        {
        ////            startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
        ////        }
        ////        else
        ////        {
        ////            startday = DateTime.Today.AddDays(-1).ToString();
        ////        }
        ////    }
        ////    else
        ////    {
        ////        startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
        ////    }
        ////    return startday;
        ////    #endregion timmings 
        ////}


        //public string DateFormat(string Date)
        //{
        //    if (Date != "")
        //        return Convert.ToDateTime(Date).ToString("yyyy-MM-dd hh:mm:ss");
        //    else
        //        return "";
        //}
    }
}
