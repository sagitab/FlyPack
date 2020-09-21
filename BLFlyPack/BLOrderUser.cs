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
    public class BLOrderUser : BLUser
    {
        public List<BLOrder> Orders { get; }
        public BLOrderUser(string pass) : base(pass)
        {
            //Orders= call dal
        }
        //public BLOrderUser(BLUser user)
        //{

        //}
        public static DataTable GetOrders(int Type, string UserID, bool Isnew, string condition)
        {
            DataTable OrdersTable = null;
            if (Isnew)
            {
                OrdersTable = DalOrderUsers.GetOrders(Type, UserID, "AND ([Orders].[OrderStutus]<5)" + condition);
            }
            else
            {
                OrdersTable = DalOrderUsers.GetOrders(Type, UserID, "AND ([Orders].[OrderStutus]=5)" + condition);
            }

            //List<BLOrder> orders = new List<BLOrder>();
            //foreach (DataRow row in t.Rows)
            //{
            //    orders.Add();
            //}
            //change stusus to string
            Dictionary<int, string> stautus = new Dictionary<int, string> { { 1, "order sent" }, { 2, "shop take care your order" }, { 3, "shiping time selected" }, { 4, "delivery take care your order" }, { 5, "order shiped" } };
            DataTable copy = OrdersTable.Clone();
            copy.Columns["OrderStutus"].DataType = typeof(string);
            DataColumnCollection colums = copy.Columns;
            foreach (DataRow row in OrdersTable.Rows)
            {
                int key = int.Parse(row["OrderStutus"].ToString());
                string id = row["ID"].ToString();
                DateTime date = DateTime.Parse(row["ArrivalTime"].ToString());
                DataRow NewRow = copy.NewRow();
                foreach (DataColumn column in colums)
                {
                    string colName = column.ColumnName.ToString();
                    if (colName == "OrderStutus")
                    {
                        NewRow[colName] = stautus[key];
                    }
                    else if (colName == "ArrivalTime")
                    {
                        NewRow[colName] = DateTime.Parse(row[colName].ToString());
                    }
                    else
                    {
                        NewRow[colName] = row[colName].ToString();
                    }
                   
                }
                copy.Rows.Add(NewRow);
            }
            return copy;
        }
    }
}
