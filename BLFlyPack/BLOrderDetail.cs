using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
   public class BLOrderDetail
    {
        public BLProduct Product { get; set; }
        public  int Amount { get; set; }
        public BLOrderDetail()
        {

        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="product"></param>
        /// <param name="amount"></param>
        public BLOrderDetail(BLProduct product, int amount)
        {
            Product = product;
            Amount = amount;
        }
        /// <summary>
        /// constructor by BLOrderDetail
        /// </summary>
        /// <param name="orderDetailsDb"></param>
        public BLOrderDetail(BLOrderDetailsDB orderDetailsDb)
        {
            Product = BLProduct.GetProductById(orderDetailsDb.productId);
            Amount = orderDetailsDb.amount;
        }
        /// <summary>
        /// constructor by data row
        /// </summary>
        /// <param name="row"></param>
        public BLOrderDetail(DataRow row)
        {
            Amount = int.Parse(row["Amount"].ToString());
            Product = new BLProduct(row) {Price = (double.Parse(row["DetailPrice"].ToString()))/Amount};
          
        }
        /// <summary>
        /// Get Order Details list By OrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>List of BLOrderDetail</returns>
        public static List<BLOrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            DataTable orderDetailsTable = OrderDetails.GetProductListByOrderId(orderId);
            return (from DataRow row in orderDetailsTable.Rows select new BLOrderDetail(row)).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns>Total Amount at list</returns>
        public static int TotalAmount(List<BLOrderDetail> orderDetails)
        {
            return orderDetails.Sum(detail => detail.Amount);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns>Total Price at list</returns>
        public static double TotalPrice(List<BLOrderDetail> orderDetails)
        {
            return orderDetails.Sum(detail => detail.Amount*detail.Product.Price);
        }

        public override string ToString()
        {
            return this.Product.ToString()+ "Amount-" + Amount;
        }

        public static List<BLOrderDetail> GetOrderDetails(List<BLOrderDetailsDB> orderDetails)
        {
            return (from blOrderDetailsDb in orderDetails select new BLOrderDetail(blOrderDetailsDb)).ToList();
        }
    }
}
