using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;
namespace BLFlyPack
{
    public class BlOrder
    {
        public int OrderId { get; }
        public string CustomerId { get; set; }
        public string DeliveryId { get; set; }
        public int ShopId { get; set; }
        public DateTime AriveTime { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReadyTime { get; set; }
        public int Status { get; set; }
        public Point Location { get; set; }
        public int NumOfFloor { get; set; }
       

        public BlOrder( string customerId, string deliveryId, int shopId, DateTime ariveTime, DateTime readyTime, int status, double lat, double lng, int numOfFloor)
        {
            int id;
            try
            {
                id = FlyPack.DalOrder.AddOrder(customerId, deliveryId, shopId, ariveTime, status, numOfFloor, readyTime, lat,lng);
            }
            catch
            {
                throw new Exception("fail");
            }

            if (id!=-1)
            {
                OrderId = id;
                CustomerId = customerId;
                DeliveryId = deliveryId;
                ShopId = shopId;
                AriveTime = ariveTime;
                Time = DateTime.Now;
                ReadyTime = readyTime;
                Status = status;
                Location = new Point(lat, lng);
                NumOfFloor = numOfFloor;
            }
          
        }
        public BlOrder(DataRow row)
        {
            this.OrderId =int.Parse(row["ID"].ToString()) ;
            this.CustomerId = row["CustomerID"].ToString();
            this.DeliveryId = row["DeliverID"].ToString();
            this.ShopId = int.Parse(row["ShopID"].ToString());
            this.AriveTime =DateTime.Parse(row["ArrivalTime"].ToString());
            this.Time = DateTime.Parse(row["Time"].ToString()); ;//what to do?
            this.Status = int.Parse(row["OrderStutus"].ToString());
            Location = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            this.NumOfFloor = int.Parse(row["NumOfFloor"].ToString());
            this.ReadyTime = DateTime.Parse(row["ReadyTime"].ToString());
        }

        public static BlOrder GetBlOrderById(int orderId)
        {
            DataRow orderRow = GetOrderById(orderId);
            return new  BlOrder(orderRow);
        }
        public static bool DeleteOrder(int id)
        {
            return DalOrder.DeleteOrder(id);
        }
        //public BLOrder(DataRow row)
        //{ret
        //    this.OrderID = id;
        //    this.CustomerID = CustomerID;
        //    this.DeliveryID = DeliveryID;
        //    this.ShopID = ShopID;
        //    this.AriveTime = AriveTime;
        //    this.Time = Time;
        //    this.Status = Status;
        //}
        public static string NumOfOrdersFromShop(int shopId)
        {
            return  DalOrder.NumOfOrders($"WHERE([Orders].[ShopID] = {shopId})").ToString();
        }
        public static bool UpdateArrivalTime(DateTime arrivalTime,int orderId)
        {
            bool seccess = true;
            try
            {
                seccess = DalOrder.UpdateArrivalTime(arrivalTime,orderId);
            }
            catch
            {
                return false;
            }
            return seccess;
        }
        public static bool UpdateReadyTime(DateTime arrivalTime, int orderId)
        {
            bool seccsess = true;
            try
            {
                seccsess = DalOrder.UpdateReadyTime(arrivalTime, orderId);
            }
            catch
            {
                return false;
            }
            return seccsess;
        }
        public static bool UpdateStatus(int status, int orderId)
        {
            bool seccsess = true;
            try
            {
                seccsess = DalOrder.UpdateStatus(status, orderId);
            }
            catch
            {
                return false;
            }
            return seccsess;
        }
        public static int GetOrderStatus(int orderId)
        {
            int stastus = -1;
            try
            {
                return DalOrder.GetOrderStatus(orderId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return stastus;
        }

        public static List<BlOrder> GetOrdersListByTime(string deliveryId)
        {
            DataTable orderTable = DalOrder.GetOrdersListByTime(deliveryId);

            return (from object row in orderTable.Rows select new BlOrder((DataRow) row)).ToList();
        }

        public static bool UpdateDelivery(int orderId, string deliveryId)
        {
           
            try
            {
                return DalOrder.UpdateDelivery(orderId, deliveryId);
            }
            catch
            {
                return false;
            }
        }
        public static DataRow GetOrderById(int orderId)
        {
            try
            {
                return DalOrder.GetOrderById(orderId);
            }
            catch
            {
                return null;
            }
        }
        //public static  BLOrder 
    }
}
