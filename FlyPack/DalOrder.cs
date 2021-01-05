using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
    public class DalOrder
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="shopId"></param>
        /// <param name="ariveTime"></param>
        /// <param name="status"></param>
        /// <param name="numOfFloor"></param>
        /// <param name="readyTime"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static int AddOrder(string customerId, string deliveryId, int shopId, DateTime ariveTime,  int status,int numOfFloor, DateTime readyTime,double lat, double lng)
        {
            return DalHelper.Insert($"INSERT INTO Orders([CustomerID],[DeliverID],[ShopID],[ArrivalTime],[OrderStutus],ReadyTime,NumOfFloor,Lat,Lng) VALUES ('{customerId}','{deliveryId}',{shopId},#{ariveTime}#,{status},#{readyTime}#,{numOfFloor},{lat},{lng})");
        }
        /// <summary>
        /// delete order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if delete success</returns>
        public static bool DeleteOrder(int id)
        {
            bool successes = true;
            try
            {
              successes=  DalHelper.DeleteRowById(id, "Orders","ID");
            }
            catch
            {
                successes = false;
            }
            return successes;
        }
        //public static int NumOfOrdersFromShop(int ShopID)
        //{
        //    return int.Parse( DalHelper.Select($"SELECT Count(Orders.ID) AS NumOfOrders FROM Orders WHERE(([Orders].[ShopID] = {ShopID})); ").Rows[0]["NumOfOrders"].ToString());
        //}
        public static int NumOfOrders(string condition)
        {
            return int.Parse(DalHelper.Select($"SELECT Count(Orders.ID) AS NumOfOrders FROM Orders {condition}").Rows[0]["NumOfOrders"].ToString());
        }
        //public static string NumOfActiveCustomers(string condition)
        //{
        //    return DalHelper.Select($"SELECT Count(Orders.CustomerID) AS NumOfActiveCustomers FROM Orders {condition}").Rows[0]["NumOfActiveCustomers"].ToString();
        //}
        public static string NumOfCustomers(int type,string condition)
        {
            return DalHelper.Select($"SELECT Count(Users.ID) AS numOfUsers FROM Users WHERE(((Users.UserType) = {type})) {condition};").Rows[0]["numOfUsers"].ToString();
        }
        public static bool UpdateArrivalTime(DateTime arrivalTime,int orderId)
        {
            string stringArrivalTime = $"#{arrivalTime.ToString()}#";
            return DalHelper.UpdateColumnById(orderId, "Orders", "ID", stringArrivalTime, "ArrivalTime");
        }
        public static bool UpdateStatus(int status, int orderId)
        {
            string stringStatus = (status.ToString());
            return DalHelper.UpdateColumnById(orderId, "Orders", "ID", stringStatus, "OrderStutus");
        }

        public static bool UpdateReadyTime(DateTime readyTime, int orderId)
        {
            string stringReadyTime = $"#{readyTime.ToString()}#";
            return DalHelper.UpdateColumnById(orderId, "Orders", "ID", stringReadyTime, "ReadyTime");
        }
        public static int GetOrderStatus(int orderId)
        {
            return int.Parse( DalHelper.Select($"SELECT OrderStutus FROM Orders WHERE ID={orderId}").Rows[0]["OrderStutus"].ToString());
        }
        /// <summary>
        /// get a data table order by order time
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static DataTable GetOrdersListByTime(string deliveryId)
        {
            return DalHelper.Select($"SELECT * FROM Orders WHERE DeliverID='{deliveryId}' AND OrderStutus=4 ORDER BY Time");
        }

        public static bool UpdateDelivery(int orderId,string deliveryId)
        {
            return DalHelper.UpdateColumnById(orderId, "Orders", "ID", deliveryId, "DeliverID");
        }
        public static DataRow GetOrderById(int orderId)
        {
            return DalHelper.Select($"SELECT * FROM Orders WHERE ID={orderId}").Rows[0];
        }
    }
}
