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
        public string ImageUrl { get; set; } /*name in DB Image*/

        public BLProduct(int id, double price, string description, int shopId, int orderId, int shopProductCode, string imageUrl)
        {
            Id = id;//update DB?
            Price = price;
            Description = description;
            ShopID = shopId;
            OrderID = orderId;
            ShopProductCode = shopProductCode;
            ImageUrl = imageUrl;
        }

        public BLProduct(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            Price = double.Parse(row["Price"].ToString());
            Description = row["Description"].ToString();
            ShopID = int.Parse(row["ShopID"].ToString());
            OrderID = int.Parse(row["OrderID"].ToString());
            ShopProductCode = int.Parse(row["ShopProductCode"].ToString());
            ImageUrl = row["Image"].ToString();
        }
        public static List<BLProduct> GetAllProducts(string condition)
        {
            DataTable products = ProductDal.GetAllProducts(condition);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }

        public static List<BLProduct> GetAllProductsByShopId(int shopId,string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOfShop(shopId,condition);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> GetAllProductsByPrice(bool isUp, string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOrderByPrice(isUp ? "DESC" : "ASC");
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> GetAllProductsByName(bool isUp, string condition)
        {
            DataTable products = null;
            products = ProductDal.GetAllProductsOrderByName(isUp ? "DESC" : "ASC");
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }
        public static List<BLProduct> ProductsSearch(bool IsByPrice, string searchVal, string condition)
        {
            DataTable products = null;
            products = IsByPrice ? ProductDal.SearchProductsByPrice(double.Parse(searchVal)) : ProductDal.SearchProductsByName(searchVal);
            return (from object rowProduct in products.Rows select new BLProduct((DataRow)rowProduct)).ToList();
        }

    }
}
