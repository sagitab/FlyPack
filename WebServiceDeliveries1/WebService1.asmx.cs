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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShopManagerPassword"></param>
        /// <returns>List<BlUser> that order from your shop</returns>
        [WebMethod]
        public List<BlUser> GetUserList(string ShopManagerPassword)
        {
            BlShopManager shopManager = new BlShopManager(ShopManagerPassword);
           return shopManager.CustomersList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Num Of Available Deliveries</returns>
        [WebMethod]
        public int GetNumOfAvailableDeliveries()
        {
            return Deliver.NumOfAvailableDeliveries();
        }
        //mast do functions
        [WebMethod]
        public int GetStatus(int orderId)
        {
            BlOrder order = BlOrder.GetBlOrderById(orderId);
            return order.Status;
        }
        [WebMethod]
        public DataTable GetUserNewOrderList(string Password)
        {
            BlOrderUser user = new BlOrderUser(Password);
            return user.GetOrders(true,"");
        }
        [WebMethod]
        public DataTable GetUserOldOrderList(string Password)
        {
            BlOrderUser user = new BlOrderUser(Password);
            return user.GetOrders(false, "");
        }
        [WebMethod]
        public DataTable GetShopAndManagerList()
        {
            return BLshopDB.ShopAndManagerTable();
        }
        [WebMethod]
        public int GetNumberOfOrders(string Password)
        {
            BlOrderUser user = new BlOrderUser(Password);
            return user.GetNumOfOrders();
        }
        [WebMethod]
        public bool Register(string userId, int type, string email, string phoneNum, string firstName, string lastName, string password, double lat, double lng)
        {
            BlUser user = null;
            try
            {
                user = new BlUser(userId, type, email, phoneNum, firstName, lastName, password, lat, lng);//add new user to DB
                return user.UserId!= "-1";
            }
            catch
            {
                return false;
            }
        }
        [WebMethod]
        public int GetNumberOfCustomers(string Password)
        {
            BlOrderUser user = new BlOrderUser(Password);
            string numOfCustomers= user.GetNumOfCustomers();
            int ret;
            if (!int.TryParse(numOfCustomers,result:out ret))
            {
                ret = 0;
            }
            return ret;
        }

        [WebMethod]
        public string GetUserNameByPassword(string Password)
        {
            BlUser user = new BlUser(Password);
            return user.UserId != null ? user.FirstName : "";
        }
        //[WebMethod]
        //public bool IsOrderIDExist(string OrderTableName, string OrderIdName, string Id)
        //{
        //    return DalHelper.RowExists($"SELECT * FROM {OrderTableName} WHERE {OrderIdName}={Id}");
        //}
        //[WebMethod]
        //public DataTable OrderProductsByOrderID(string OrderDetialsTableName, string OrderIdName, string Id)
        //{
        //    return DalHelper.Select($"SELECT * FROM {OrderDetialsTableName} WHERE {OrderIdName}={Id}");
        //}


        ///// <summary>
        ///// get a the  products table from shop and add to my DB the products
        ///// </summary>
        ///// <param name="productsTableName"></param>
        ///// <param name="pass"></param>
        ///// <returns></returns>
        //[WebMethod]
        //public bool UpdateAllProductFromShop(string productsTableName, string ShopIDColumn, string OrderIDColumn, string DescriptionColumn, string ShopProductCodeColumn, string PriceColumn, string ImageColumn, string pass)
        //{
        //    BlShopManager shopManager=new BlShopManager(pass);
        //    int shopId = shopManager.ShopId;
        //    DataTable productsTable = BLshopDB.GetAllProductFromShopDB( productsTableName,  ShopIDColumn,  OrderIDColumn,  DescriptionColumn,  ShopProductCodeColumn,  PriceColumn,  ImageColumn);//get data table from shop DB
        //    return BLProduct.UpdateProduct(productsTable,shopId);
        //}
        ///// <summary>
        ///// update the amount of product in shop DB in case of order
        ///// </summary>
        ///// <param name="TotalAmountTableName"></param>
        ///// <param name="pass"></param>
        ///// <param name="orderId"></param>
        ///// <returns></returns>
        ///// ShopID,OrderID,Description,ShopProductCode,Price,Image
        //[WebMethod]
        //public  bool UpdateProductAmountAtShop(string TotalAmountTableName, string TotalAmountColumn,string idColumnName,string pass,int orderId)
        //{
        //    BlShopManager shopManager = new BlShopManager(pass);
        //    int shopId = shopManager.ShopId;
        //    List<BLOrderDetailsDB> orderDetailsDB = BLOrderDetailsDB.DetailsListOfOrder(orderId);
        //    List<BLOrderDetail> orderDetails = BLOrderDetail.GetOrderDetails(orderDetailsDB);
        //    return BLshopDB.UpdateProductAmountAtShop(TotalAmountTableName, TotalAmountColumn, idColumnName, orderDetails);//do a a update function that get List<BLOrderDetails> and update the amount
        //}
    }
}
