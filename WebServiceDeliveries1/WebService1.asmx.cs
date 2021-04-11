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
        [WebMethod]
        public List<BlShop> GetAllShops()
        {
            List<BlShop> shops= BlShop.GetShops();
            return shops;
        }//get all shop in DB
        [WebMethod]
        public List<BLProduct> GetAllProductsByShopId(int ShopId)
        {
            List<BLProduct> products = BLProduct.GetAllProductsByShopId(ShopId,"");
            return products;
        }//get all product of shop 
        [WebMethod]
        public bool AddOrder(string customerId,  int shopId,  int status, double lat, double lng,
            int numOfFloor,List<BLOrderDetailsDB> orderDetails)
        {
            int id;
            BlOrder order = null;
            try
            {
                order=new BlOrder(customerId, "111111111", shopId, new DateTime(2000, 1, 1, 1, 1, 1), new DateTime(2000, 1, 1, 1, 1, 1), 1, lat, lng, numOfFloor, orderDetails);///add order
                 id = order.OrderId;
            }
            catch
            {
                return false;
            }
            bool isUpdateDetails = BLOrderDetailsDB.UpdateOrderDetails(orderDetails, id);
            return (id == -1)&& isUpdateDetails;
        }//add order to BD
     
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Num Of Available Deliveries</returns>
        [WebMethod]
        public int GetNumOfAvailableDeliveries()
        {
            return Deliver.NumOfAvailableDeliveries();
        }//get number of avilable deliveries
        [WebMethod]
        public BlUser GetUserNameById(string Id)
        {
            BlUser user = BlUser.UserById(Id);
            return user;
        }
        [WebMethod]
        public int GetStatus(int orderId)
        {
            BlOrder order = BlOrder.GetBlOrderById(orderId);
            return order.Status;
        }//get status of order
        [WebMethod]
        public DataTable GetUserNewOrderList(string Password)
        {
            BlOrderUser user = new BlOrderUser(Password);
            return user.GetOrders(true,"");
        }
        [WebMethod]
        public List<BlOrder> GetUserOldOrderList(string Password)
        {
            BlOrderUser user = new BlOrderUser(Password);
            DataTable orders= user.GetOrders(false, "");
            List<BlOrder> ordersList = new List<BlOrder>();
            foreach(DataRow row in orders.Rows)
            {
                ordersList.Add(new BlOrder(row));
            }
            return ordersList;
        }
        [WebMethod]
        public bool Register(string userId, int type, string email, string phoneNum, string firstName, string lastName, string password, double lat, double lng)
        {
            BlUser user = null;
            try
            {
                user = new BlUser(userId, type, email, phoneNum, firstName, lastName, password, lat, lng);//add new user to DB
                return user.UserId != "-1";
            }
            catch
            {
                return false;
            }
        }
        [WebMethod]
        public bool Order(int shopId, int numOfFloor, List<BLOrderDetailsDB> orderDetails, string password, double lat, double lng)
        {
            BlOrderUser user = new BlOrderUser(password);
            BlOrder order = null;
            try
            {
                order = new BlOrder(user.UserId, "111111111", shopId, new DateTime(2000, 1, 1, 1, 1, 1), new DateTime(2000, 1, 1, 1, 1, 1), 1, lat, lng, numOfFloor, orderDetails);//add order
                return user.UserId != "-1";
            }
            catch
            {
                return false;
            }
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
