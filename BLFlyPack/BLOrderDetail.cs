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
        public string ImageUrl => Product.ImageUrl;

        public BLOrderDetail(BLProduct product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        public BLOrderDetail(DataRow row)
        {
            Amount = int.Parse(row["Amount"].ToString());
            Product = new BLProduct(row) {Price = (double.Parse(row["DetailPrice"].ToString()))/Amount};
          
        }

        public static List<BLOrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            DataTable orderDetailsTable = OrderDetails.GetProductListByOrderId(orderId);
            return (from DataRow row in orderDetailsTable.Rows select new BLOrderDetail(row)).ToList();
        }

        public static int TotalAmount(List<BLOrderDetail> orderDetails)
        {
            return orderDetails.Sum(detail => detail.Amount);
        }
        public static double TotalPrice(List<BLOrderDetail> orderDetails)
        {
            return orderDetails.Sum(detail => detail.Amount*detail.Product.Price);
        }

        public override string ToString()
        {
            return this.Product.ToString();
        }
    }
}
