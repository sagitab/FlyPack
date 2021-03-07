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
            return DalHelper.Insert($"INSERT INTO OrderDetails(OrderID,ProductID,Amount,Price) VALUES({orderId},{productId},{amount},{price}) ");
        }

        public static DataTable GetDetailsOfOrder(int orderId)
        {
            return DalHelper.AllWhere("OrderDetails", "OrderID",orderId);
        }
        public static bool UpdateAmount(int id, int amount)
        {
            return DalHelper.UpdateColumnById(id, "OrderDetails", "ID", "" + amount, "Amount");
        }
        public static DataTable GetProductListByOrderId(int orderId)
        {
            return DalHelper.Select("SELECT Products.*, OrderDetails.Amount, OrderDetails.Price AS DetailPrice FROM Products INNER JOIN OrderDetails ON Products.ID = OrderDetails.ProductID WHERE [OrderDetails].[OrderID]=" + orderId);
        }

        public static bool DeleteOrderDetails(int orderId)
        {
            return DalHelper.DeleteRowById(orderId, "OrderDetails", "OrderID");
        }
    }
}
