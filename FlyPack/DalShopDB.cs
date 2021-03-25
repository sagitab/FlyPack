using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
   public class DalShopDB
    {
        public static DataTable GetAllProductFromShopDB(string productsTableName, string ShopIDColumn, string OrderIDColumn, string DescriptionColumn, string ShopProductCodeColumn, string PriceColumn, string ImageColumn)
        {
            return DalHelper.Select($"SELECT {ShopIDColumn},{OrderIDColumn},{PriceColumn},{DescriptionColumn},{ImageColumn},{ShopProductCodeColumn} FROM {productsTableName}");
        }

        public static bool UpdateProductAmountAtShop(int Id,int ReplaceAmount,string TotalAmountTableName, string TotalAmountColumn,string idColumnName)
        {
            return DalHelper.UpdateColumnById(Id, TotalAmountTableName, idColumnName, ReplaceAmount.ToString(),
                TotalAmountColumn);
        }
        public static int GetAmountByProductId(int productId,string TotalAmountTableName, string TotalAmountColumn, string idColumnName)
        {
            return int.Parse(DalHelper.Select($"SELECT {TotalAmountColumn} FROM {TotalAmountTableName} WHERE {idColumnName}={productId}").Rows[0][TotalAmountColumn].ToString());
        }
        public static DataTable ShopAndManagerTable()
        {
            return DalHelper.Select($"SELECT Shops.ShopName, Users.FirstName AS ManagerName FROM (Users INNER JOIN Shops ON Users.ID = Shops.ShopManagerID) GROUP BY Shops.ShopName, Users.FirstName  ;");
        }
    }
}
