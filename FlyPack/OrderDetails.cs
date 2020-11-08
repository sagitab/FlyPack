using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
   public class OrderDetails
    {
        public static int AddDetail(int orderId, int productId, int amount,double price)
        {
            return DalHelper.Insert($"INSERT INTO OrderDetails(OrderID,ProductID,Amount,Price) VALUES({orderId},{productId},{amount},{price},) ");
        }

        public static DataTable GetDetailsOfOrder(int orderId)
        {
            return DalHelper.AllWhere("OrderDetails", "OrderID",orderId);
        }
        public static bool UpdateAmount(int Id, int amount)
        {
            return DalHelper.UpdateColumnById(Id, "OrderDetails", "ID", "" + amount, "Amount");
        }
    }
}
