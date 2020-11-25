using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
    public class BLOrderDetailsDB
    {
        public int Id { get; set; }
        public int orderId { get; set; }
        public int productId { get; set; }
        public int amount { get; set; }
        public double price { get; set; }
        /// <summary>
        /// Add detail to DB
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        public BLOrderDetailsDB(int orderId, int productId, int amount, double price)
        {
            this.Id = OrderDetails.AddDetail(orderId, productId, amount, price);
            this.orderId = orderId;
            this.productId = productId;
            this.amount = amount;
            this.price = price;
        }

        //public static bool UpdateAmount(int Id, int amount)
        //{
        //    try
        //    {
        //        return OrderDetails.UpdateAmount(Id, amount);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        /// <summary>
        /// constructor by data row
        /// </summary>
        /// <param name="row"></param>
        public BLOrderDetailsDB(DataRow row)
        {
            this.Id =int.Parse(row["ID"].ToString());
            this.orderId = int.Parse(row["OrderID"].ToString()); 
            this.productId = int.Parse(row["ProductID"].ToString());
            this.amount = int.Parse(row["Amount"].ToString());
            this.price = int.Parse(row["Price"].ToString());
        }
        /// <summary>
        /// get List of details of order by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>List of details</returns>
        public static List<BLOrderDetailsDB> DetailsListOfOrder(int orderId)
        {
            DataTable details = OrderDetails.GetDetailsOfOrder(orderId);
            return (from DataRow row in details.Rows select new BLOrderDetailsDB((DataRow) row)).ToList();
        }
        /// <summary>
        /// update the order details in DB
        /// </summary>
        /// <param name="products"></param>
        /// <param name="orderId"></param>
        /// <param name="amounts"></param>
        /// <returns>success or failure</returns>
        public static bool UpdateOrderDetails(List<BLProduct> products,int orderId,int[] amounts)
        {
            List<BLOrderDetailsDB> OrderDetails=(from product in products select new BLOrderDetailsDB(orderId, product.Id, amounts[(int) products.IndexOf(product)], product.Price)).ToList();
            return OrderDetails.All(OrderDetail => OrderDetail?.Id != -1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>description of the product</returns>
        public override string ToString()
        {
            return $"{BLProduct.GetProductNameById(productId)}, price-{price},amount-{amount}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns>string that describe the order details list</returns>
        public static string GetProductString(List<BLOrderDetailsDB> orderDetails)
        {
            return orderDetails.Aggregate("", (current, detail) => current + ("<br/>" + detail.ToString()));
        }

    }
}
