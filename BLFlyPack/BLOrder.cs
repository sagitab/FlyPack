using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;
namespace BLFlyPack
{
    public class BLOrder
    {
        public int OrderID { get; }
        public string CustomerID { get; set; }
        public string DeliveryID { get; set; }
        public int ShopID { get; set; }
        public DateTime AriveTime { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReadyTime { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public int NumOfFloor { get; set; }
        public BLOrder(string CustomerID, string DeliveryID, int ShopID, DateTime AriveTime,  int Status, DateTime ReadyTime, string Address, int Floor)
        {
            int id;
            try
            {
                id= FlyPack.DalOrder.AddOrder(CustomerID, DeliveryID, ShopID, AriveTime, Status,Address, Floor, ReadyTime);
            }
            catch
            {
                throw new Exception("fail");
            }
            this.OrderID = id;
            this.CustomerID = CustomerID;
            this.DeliveryID = DeliveryID;
            this.ShopID = ShopID;
            this.AriveTime = AriveTime;
            //this.Time = Time;//what to do?
            this.Status = Status;
            this.Address = Address;
            this.NumOfFloor = Floor;
            this.ReadyTime = ReadyTime;
        }

        public BLOrder(DataRow row)
        {
            this.OrderID =int.Parse(row["ID"].ToString()) ;
            this.CustomerID = row["CustomerID"].ToString();
            this.DeliveryID = row["DeliverID"].ToString();
            this.ShopID = int.Parse(row["ShopID"].ToString());
            this.AriveTime =DateTime.Parse(row["ArrivalTime"].ToString());
            this.Time = DateTime.Parse(row["Time"].ToString()); ;//what to do?
            this.Status = int.Parse(row["OrderStutus"].ToString());
            this.Address = row["Address"].ToString();
            this.NumOfFloor = int.Parse(row["NumOfFloor"].ToString());
            this.ReadyTime = DateTime.Parse(row["ReadyTime"].ToString());
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
        public static string NumOfOrdersFromShop(int ShopID)
        {
            return  DalOrder.NumOfOrders($"WHERE([Orders].[ShopID] = {ShopID})").ToString();
        }
        public static bool UpdateArrivalTime(DateTime ArrivalTime,int OrderID)
        {
            bool seccsess = true;
            try
            {
                seccsess = DalOrder.UpdateArrivalTime(ArrivalTime,OrderID);
            }
            catch
            {
                return false;
            }
            return seccsess;
        }
        public static bool UpdateReadyTime(DateTime ArrivalTime, int OrderID)
        {
            bool seccsess = true;
            try
            {
                seccsess = DalOrder.UpdateReadyTime(ArrivalTime, OrderID);
            }
            catch
            {
                return false;
            }
            return seccsess;
        }
        public static bool UpdateStatus(int Status, int OrderID)
        {
            bool seccsess = true;
            try
            {
                seccsess = DalOrder.UpdateStatus(Status, OrderID);
            }
            catch
            {
                return false;
            }
            return seccsess;
        }
        public static int GetOrderStatus(int OrderID)
        {
            int stastus = -1;
            try
            {
                return DalOrder.GetOrderStatus(OrderID);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return stastus;
        }

        public static List<BLOrder> GetOrdersListByTime(string deliveryId)
        {
            DataTable orderTable = DalOrder.GetOrdersListByTime(deliveryId);

            return (from object row in orderTable.Rows select new BLOrder((DataRow) row)).ToList();
        }
    }
}
