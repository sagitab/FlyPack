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
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="pass"></param>
        public BlOrderUser(string pass) : base(pass)
        {
            //Orders= call dal
        }
        //public BLOrderUser(BLUser user)
        //{

        //}
        /// <summary>
        /// get the orders table
        /// </summary>
        /// <param name="IsNew"></param>
        /// <param name="condition"></param>
        /// <returns>orders table</returns>
        public DataTable GetOrders( bool IsNew, string condition)
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
                    switch (colName)
                    {
                        case "OrderStutus":
                            newRow[colName] = status[key];
                            break;
                        case "ArrivalTime":
                            newRow[colName] = DateTime.Parse(row[colName].ToString());
                            break;
                        default:
                            newRow[colName] = row[colName].ToString();
                            break;
                    }
                }
                copy.Rows.Add(newRow);
            }
            return copy;
        }
        /// <summary>
        /// get order list order by order time
        /// </summary>
        /// <returns>orders list</returns>
        public List<BlOrder> GetOrdersListByTime()
        {
            DataTable orderTable = DalOrder.GetOrdersListByTime(this.UserId);

            return (from object row in orderTable.Rows select new BlOrder((DataRow)row)).ToList();
        }
        public virtual DataTable DeliveriesTable()
        {
            DataTable t = null;
            try
            {
                t = DalUser.DeliveriesTable();
            }
            catch
            {
                return null;
            }
            return t;
        }
        public virtual string GetNumOfActiveCustomers()
        {
            try
            {
                return DalOrder.NumOfActiveCustomers("");
            }
            catch
            {
                return "";
            }

        }
        public virtual int GetNumOfOrders()
        {
            try
            {
                return DalOrder.NumOfOrders("");
            }
            catch
            {
                return -1;
            }

        }
       
    }
}
