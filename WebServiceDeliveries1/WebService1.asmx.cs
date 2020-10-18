using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLFlyPack;
//using FlyPack;


namespace WebServiceDeliveries1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        
        //[WebMethod]
        //public int GetStatus( )
        //{
        //    return BlOrder.GetOrderStatus();
        //}
        //[WebMethod]
        //public bool IsOrderIDExist(string OrderTableName,string OrderIdName,string Id )
        //{
        //    return DalHelper.RowExists($"SELECT * FROM {OrderTableName} WHERE {OrderIdName}={Id}");
        //}
        //[WebMethod]
        //public DataTable OrderProductsByOrderID(string OrderDetialsTableName, string OrderIdName, string Id)
        //{
        //    return DalHelper.Select($"SELECT * FROM {OrderDetialsTableName} WHERE {OrderIdName}={Id}");
        //}
    }
}
