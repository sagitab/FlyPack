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
        public DateTime ArriveTime { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReadyTime { get; set; }
        public int Status { get; set; }
        public Point Location { get; set; }
        public int NumOfFloor { get; set; }
       
        /// <summary>
        /// add a new order to database
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="shopId"></param>
        /// <param name="arriveTime"></param>
        /// <param name="readyTime"></param>
        /// <param name="status"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="numOfFloor"></param>
        public BlOrder( string customerId, string deliveryId, int shopId, DateTime arriveTime, DateTime readyTime, int status, double lat, double lng, int numOfFloor)
        {
            int id;
            try
            {
                id = FlyPack.DalOrder.AddOrder(customerId, deliveryId, shopId, arriveTime, status, numOfFloor, readyTime, lat,lng);
            }
            catch
            {
                throw new Exception("fail");
            }

            if (id == -1) return;
            OrderId = id;
            CustomerId = customerId;
            DeliveryId = deliveryId;
            ShopId = shopId;
            ArriveTime = arriveTime;
            Time = DateTime.Now;
            ReadyTime = readyTime;
            Status = status;
            Location = new Point(lat, lng);
            NumOfFloor = numOfFloor;

        }
        /// <summary>
        /// 
        /// </summary>
        /// create a new order with data row
        /// <param name="row"></param>
        public BlOrder(DataRow row)
        {
            this.OrderId =int.Parse(row["ID"].ToString()) ;
            this.CustomerId = row["CustomerID"].ToString();
            this.DeliveryId = row["DeliverID"].ToString();
            this.ShopId = int.Parse(row["ShopID"].ToString());
            this.ArriveTime =DateTime.Parse(row["ArrivalTime"].ToString());
            this.Time = DateTime.Parse(row["Time"].ToString()); ;//what to do?
            this.Status = int.Parse(row["OrderStutus"].ToString());
            Location = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            this.NumOfFloor = int.Parse(row["NumOfFloor"].ToString());
            this.ReadyTime = DateTime.Parse(row["ReadyTime"].ToString());
        }
        /// <summary>
        /// get order by id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>order object</returns>
        public  BlOrder GetBlOrderById()
        {
            DataRow orderRow = GetOrderById(OrderId);
            return new  BlOrder(orderRow);
        }
        /// <summary>
        /// delete order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if delete success</returns>
        public  bool DeleteOrder()
        {
            return DalOrder.DeleteOrder(OrderId);
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
        //public static string NumOfOrdersFromShop(int shopId)
        //{
        //    return  DalOrder.NumOfOrders($"WHERE([Orders].[ShopID] = {shopId})").ToString();
        //}
        /// <summary>
        /// update arrive time of order with order id
        /// </summary>
        /// <param name="arrivalTime"></param>
        /// <returns>true if update success</returns>
        public  bool UpdateArrivalTime(DateTime arrivalTime)
        {
            var success = true;
            try
            {
                success = DalOrder.UpdateArrivalTime(arrivalTime,OrderId);
            }
            catch
            {
                return false;
            }
            return success;
        }
        /// <summary>
        /// update ready time of order by order id
        /// </summary>
        /// <param name="readyTime"></param>
        /// <returns>true if update success</returns>
        public  bool UpdateReadyTime(DateTime readyTime)
        {
            bool success = true;
            try
            {
                success = DalOrder.UpdateReadyTime(readyTime, OrderId);
            }
            catch
            {
                return false;
            }
            return success;
        }
        /// <summary>
        /// update the status of order by order id
        /// </summary>
        /// <param name="status"></param>
        /// <returns>true if update success</returns>
        public  bool UpdateStatus(int status)
        {
            bool success = true;
            try
            {
                success = DalOrder.UpdateStatus(status, OrderId);
            }
            catch
            {
                return false;
            }
            return success;
        }
        /// <summary>
        /// get the status of order by order id
        /// </summary>
        /// <returns>order status</returns>
        public  int GetOrderStatus()
        {
            try
            {
                return DalOrder.GetOrderStatus(OrderId);
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// update the delivery of order by order id
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns>true if update success</returns>
        public  bool UpdateDelivery( string deliveryId)
        {
           
            try
            {
                return DalOrder.UpdateDelivery(OrderId, deliveryId);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// get a data row of order  by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>data row of order</returns>
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
