using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BLOrder
    {
        public int OrderID { get; }
        public int CustomerID { get; set; }
        public int DeliveryID { get; set; }
        public int ShopID { get; set; }
        public DateTime AriveTime { get; set; }
        public DateTime Time { get; set; }
        public string Status { get; set; }
        public BLOrder( int CustomerID, int DeliveryID, int ShopID, DateTime AriveTime,  string Status)
        {
            int id;
            try
            {
                id= FlyPack.DalOrder.AddOrder(CustomerID, DeliveryID, ShopID, AriveTime, Status);
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
            this.Time = Time;
            this.Status = Status;

        }
        
    }
}
