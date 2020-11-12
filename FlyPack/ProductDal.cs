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
    }
}
