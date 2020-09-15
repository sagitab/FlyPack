using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
using System.Runtime.InteropServices;

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
            //change stusus to string
            Dictionary<int, string> stautus = new Dictionary<int, string> { {1,"order sent" }, { 2, "shop take care your order"}, {3,"shiping time selected"}, {4, "delivery take care your order" }, { 5, "order shiped" } };
            DataTable copy = t.Clone();
            copy.Columns["OrderStutus"].DataType = typeof(string);
            foreach (DataRow row in t.Rows)
            {
                int key = int.Parse(row["OrderStutus"].ToString());
                int id = int.Parse(row[0].ToString());
                DateTime date = DateTime.Parse(row[1].ToString());
                DataRow NewRow = copy.NewRow();
                NewRow[0] = id;
                NewRow[1] = date;
                NewRow[2] = stautus[key];
                NewRow[3] = row[3].ToString();
                NewRow[4] = row[4].ToString();
                copy.Rows.Add(NewRow);
            }
            return copy;
        }
    }
}
