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
        public static int AddOrder(string CustomerID, string DeliveryID, int ShopID, DateTime AriveTime,  int Status,int NumOfFloor, DateTime ReadyTime,double Lat, double Lng)
        {
            return DalHelper.Insert($"INSERT INTO Orders([CustomerID],[DeliverID],[ShopID],[ArrivalTime],[OrderStutus],ReadyTime,,NumOfFloor,Lat,Lng) VALUES ({CustomerID},{DeliveryID},{ShopID},#{AriveTime}#,{Status},#{ReadyTime}#,{NumOfFloor},{Lat},{Lng})");
        }
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
        public static string NumOfActiveCustomers(string condition)
        {
            return DalHelper.Select($"SELECT Count(Orders.CustomerID) AS NumOfActiveCustomers FROM Orders {condition}").Rows[0]["NumOfActiveCustomers"].ToString();
        }
       public static bool UpdateArrivalTime(DateTime ArrivalTime,int OrderID)
        {
            string StringArrivalTime = $"#{ArrivalTime.ToString()}#";
            return DalHelper.UpdateColumnById(OrderID, "Orders", "ID", StringArrivalTime, "ArrivalTime");
        }
        public static bool UpdateStatus(int status, int OrderID)
        {
            string StringStatus = (status.ToString());
            return DalHelper.UpdateColumnById(OrderID, "Orders", "ID", StringStatus, "OrderStutus");
        }

        public static bool UpdateReadyTime(DateTime ReadyTime, int OrderID)
        {
            string StringReadyTime = $"#{ReadyTime.ToString()}#";
            return DalHelper.UpdateColumnById(OrderID, "Orders", "ID", StringReadyTime, "ReadyTime");
        }
        public static int GetOrderStatus(int OrderID)
        {
            return int.Parse( DalHelper.Select($"SELECT OrderStutus FROM Orders WHERE ID={OrderID}").Rows[0]["OrderStutus"].ToString());
        }

        public static DataTable GetOrdersListByTime(string deliveryId)
        {
            return DalHelper.Select($"SELECT * FROM Orders WHERE DeliverID='{deliveryId}' AND OrderStutus=4 ORDER BY Time");
        }

        public static bool UpdateDelivery(int OrderID,string DeliveryID)
        {
            return DalHelper.UpdateColumnById(OrderID, "Orders", "ID", DeliveryID, "DeliverID");
        }
        public static DataRow GetOrderByID(int OrderID)
        {
            return DalHelper.Select($"SELECT * FROM Orders WHERE ID={OrderID}").Rows[0];
        }
    }
}
