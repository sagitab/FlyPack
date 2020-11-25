using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
   public class BLshopDB
    {
        public static DataTable GetAllProductFromShopDB(string productsTableName, string ShopIDColumn, string OrderIDColumn, string DescriptionColumn, string ShopProductCodeColumn, string PriceColumn, string ImageColumn)
        {
            try
            {
                return DalShopDB.GetAllProductFromShopDB(productsTableName,ShopIDColumn,OrderIDColumn,DescriptionColumn,ShopProductCodeColumn,PriceColumn,ImageColumn);
            }
            catch
            {
                return null;
            }
        }

        public static int GetAmountByProductId(int productId, string TotalAmountTableName, string TotalAmountColumn,
            string idColumnName)
        {
            try
            {
               return DalShopDB.GetAmountByProductId(productId, TotalAmountTableName, TotalAmountColumn, idColumnName);
            }
            catch
            {
                return -1;
            }
        }
        public static bool UpdateProductAmountAtShop(string TotalAmountTableName, string TotalAmountColumn, string idColumnName, List<BLOrderDetail> orderDetails)
        {
            try
            {
                foreach (var orderDetail in orderDetails)
                {
                    int productId = orderDetail.Product.Id;
                    int currentAmount = GetAmountByProductId(productId, TotalAmountTableName, TotalAmountColumn, idColumnName);
                    int newAmount = currentAmount - orderDetail.Amount;
                    bool isUpdated = DalShopDB.UpdateProductAmountAtShop(productId, newAmount, TotalAmountTableName,TotalAmountColumn, idColumnName);
                    if (!isUpdated)//do something else because what heppend if jast part of the amount is updated?
                    {
                        return false;
                    } 
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
