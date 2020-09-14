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
        public static DataTable GetOrders(int Type,  string UserID,bool Isnew,string condition)
        {
            DataTable t = null;
            if (Isnew)
            {
              t=DalOrderUsers.GetOrders(Type, UserID, "AND ([Orders].[OrderStutus]<5)"+condition);
            }
            else
            {
                t =DalOrderUsers.GetOrders(Type, UserID, "AND ([Orders].[OrderStutus]=5)" + condition);
            }
         
            //List<BLOrder> orders = new List<BLOrder>();
            //foreach (DataRow row in t.Rows)
            //{
            //    orders.Add();
            //}
           
            return t;
        }
    }
}
