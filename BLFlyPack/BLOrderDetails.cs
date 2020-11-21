using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
    public class BLOrderDetails
    {
        public int Id { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
        public BLOrderDetails(int orderId, int productId, int amount, double price)
        {
            this.Id = OrderDetails.AddDetail(orderId, productId, amount, price);
            this.orderId = orderId;
            this.productId = productId;
            this.amount = amount;
            this.price = price;
        }

        public static bool UpdateAmount(int Id, int amount)
        {
            try
            {
                return OrderDetails.UpdateAmount(Id, amount);
            }
            catch
            {
                return false;
            }
        }

        public BLOrderDetails(DataRow row)
        {
            this.Id =int.Parse(row["ID"].ToString());
            this.orderId = int.Parse(row["OrderID"].ToString()); 
            this.productId = int.Parse(row["ProductID"].ToString());
            this.amount = int.Parse(row["Amount"].ToString());
            this.price = int.Parse(row["Price"].ToString());
        }
        public static List<BLOrderDetails> DetailsListOfOrder(int orderId)
        {
            DataTable details = OrderDetails.GetDetailsOfOrder(orderId);
            return (from DataRow row in details.Rows select new BLOrderDetails((DataRow) row)).ToList();
        }

        public static bool UpdateOrderDetails(List<BLProduct> products,int orderId,int[] amounts)
        {
            List<BLOrderDetails> OrderDetails=(from product in products select new BLOrderDetails(orderId, product.Id, amounts[(int) products.IndexOf(product)], product.Price)).ToList();
            return OrderDetails.All(OrderDetail => OrderDetail?.Id != -1);
        }

    }
}
