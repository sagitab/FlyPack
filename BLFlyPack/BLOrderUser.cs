using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
namespace BLFlyPack
{
   public class BLOrderUser:BLUser
    {
        public List<BLOrder> Orders { get; }
        public BLOrderUser(string pass):base(pass)
        {
            //Orders= call dal
        }
        //public BLOrderUser(BLUser user)
        //{

        //}
        public static DataTable GetOrders(int Type, int UserID)
        {
            DataTable t = DalOrderUsers.GetOrders(Type, UserID);
            //List<BLOrder> orders = new List<BLOrder>();
            //foreach (DataRow row in t.Rows)
            //{
            //    orders.Add();
            //}
           
            return t;
        }
    }
}
