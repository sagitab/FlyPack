using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
    public class BLProduct
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int ShopID { get; set; }
        public int OrderID { get; set; }
        public int ShopProductCode { get; set; }

        public BLProduct(int id, double price, string description, int shopId, int orderId, int shopProductCode)
        {
            Id = id;//update DB?
            Price = price;
            Description = description;
            ShopID = shopId;
            OrderID = orderId;
            ShopProductCode = shopProductCode;
        }

        public BLProduct(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            Price = double.Parse(row["Price"].ToString());
            Description = row["Description"].ToString();
            ShopID = int.Parse(row["ShopID"].ToString());
            OrderID = int.Parse(row["OrderID"].ToString());
            ShopProductCode = int.Parse(row["ShopProductCode"].ToString());
        }
        public static List<BLProduct> GetAllProducts()
        {
            DataTable products = ProductDal.GetAllProducts();
            return (from object rowProduct in products.Rows select new BLProduct((DataRow) rowProduct)).ToList();
        }
        public static List<BLProduct> GetAllProductsByPrice(bool isUpOrDown, string condition)
        {
            DataTable products = ProductDal.GetAllProducts();
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> GetAllProductsByName(bool isUpOrDown, string condition)
        {
            DataTable products = ProductDal.GetAllProducts();
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> ProductsSearch(string condition)
        {
            DataTable products = ProductDal.GetAllProducts();
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }

    }
}
