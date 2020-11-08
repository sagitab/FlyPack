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
        public static DataTable GetAllProducts()
        {
            return DalHelper.AllFromTable("Products");
        }
        public static DataTable GetAllProductsOfShop(int shopId)
        {
            return DalHelper.AllWhere("Products", "ShopID", shopId);
        }
        public static DataTable GetAllProductsOrderByPrice()
        {
            return DalHelper.Select("SELECT * FROM Products GROUP BY Price");
        }
        public static DataTable SearchProductsByPrice(double price)
        {
            return DalHelper.Select($"SELECT * FROM Products WHERE Price={price}");
        }
        public static DataTable SearchProductsByName(string name)
        {
            return DalHelper.Select($"SELECT * FROM Products WHERE  Description='{name}'");
        }
        public static DataTable GetAllProductsOrderByName()
        {
            return DalHelper.Select("SELECT * FROM Products GROUP BY Description");
        }
    }
}
