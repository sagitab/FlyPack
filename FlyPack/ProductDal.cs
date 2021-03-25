using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
   public class ProductDal
    {
        public static DataTable GetAllProducts(string condition)
        {
            return DalHelper.Select("SELECT * FROM Products "+condition);
        }
        public static DataTable GetAllProductsOfShop(int shopId,string condition)
        {
            return DalHelper.Select($"SELECT * FROM Products WHERE ShopID = {shopId} "+ condition);
        }
        public static DataTable GetAllProductsOrderByPrice(string condition)
        {
            return DalHelper.Select("SELECT * FROM Products GROUP BY Price "+condition);
        }
        public static DataTable SearchProductsByPrice(double price)
        {
            return DalHelper.Select($"SELECT * FROM Products WHERE Price={price}");
        }
        public static DataTable SearchProductsByName(string name)
        {
            return DalHelper.Select($"SELECT * FROM Products WHERE  Description='{name}'");
        }
        public static DataTable GetAllProductsOrderByName(string condition)
        {
            return DalHelper.Select("SELECT * FROM Products GROUP BY Description "+condition);
        }

        public static DataTable GetShopIdByProductId(int productId)
        {
            return DalHelper.Select("SELECT ShopID FROM Products WHERE ID="+productId);
        }

        public static string GetProductNameById(int productId)
        {
            return DalHelper.Select("SELECT Description FROM Products WHERE ID=" + productId).Rows[0]["Description"].ToString();
        }
        public static DataRow GetProductById(int productId)
        {
            return DalHelper.Select("SELECT * FROM Products WHERE ID=" + productId).Rows[0];
        }

        public static int AddProduct(double price, string description, int shopId,int shopProductCode, string imageUrl)
        {
            return DalHelper.Insert($"INSERT INTO Products(ShopID,Description,ShopProductCode,Price,Image) VALUES({shopId},'{description}',{shopProductCode},{price},'{imageUrl}')");
        }

        public static bool RemoveProduct(int Id)
        {
            return DalHelper.DeleteRowById(Id, "Products", "ID");
        }
    }
}
