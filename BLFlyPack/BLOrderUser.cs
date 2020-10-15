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
    public class BlOrderUser : BlUser
    {
        public List<BlOrder> Orders { get; }
        public BlOrderUser(string pass) : base(pass)
        {
            //Orders= call dal
        }
        //public BLOrderUser(BLUser user)
        //{

        //}
        public  DataTable GetOrders( bool IsNew, string condition)
        {
            DataTable ordersTable = null;
            ordersTable = IsNew ? DalOrderUsers.GetOrders(Type, UserId, "AND ([Orders].[OrderStutus]<5)" + condition) : DalOrderUsers.GetOrders(Type, UserId, "AND ([Orders].[OrderStutus]=5)" + condition);

            //List<BLOrder> orders = new List<BLOrder>();
            //foreach (DataRow row in t.Rows)
            //{
            //    orders.Add();
            //}
            //change status to string
            Dictionary<int, string> status = new Dictionary<int, string> { { 1, "order sent" }, { 2, "shop take care your order" }, { 3, "shipping time selected" }, { 4, "delivery take care your order" }, { 5, "order shipped" } };
            DataTable copy = ordersTable.Clone();
            copy.Columns["OrderStutus"].DataType = typeof(string);
            DataColumnCollection columns = copy.Columns;
            foreach (DataRow row in ordersTable.Rows)
            {
                int key = int.Parse(row["OrderStutus"].ToString());
                string id = row["ID"].ToString();
                DateTime date = DateTime.Parse(row["ArrivalTime"].ToString());
                DataRow newRow = copy.NewRow();
                foreach (DataColumn column in columns)
                {
                    string colName = column.ColumnName.ToString();
                    if (colName == "OrderStutus")
                    {
                        newRow[colName] = status[key];
                    }
                    else if (colName == "ArrivalTime")
                    {
                        newRow[colName] = DateTime.Parse(row[colName].ToString());
                    }
                    else
                    {
                        newRow[colName] = row[colName].ToString();
                    }
                   
                }
                copy.Rows.Add(newRow);
            }
            return copy;
        }
        public  List<BlOrder> GetOrdersListByTime()
        {
            DataTable orderTable = DalOrder.GetOrdersListByTime(this.UserId);

            return (from object row in orderTable.Rows select new BlOrder((DataRow)row)).ToList();
        }
    }
}
