using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace FlyPack
{
   public class DalShop
    {
        public static DataTable GetShopTable()
        {
            return DalHelper.AllFromTable("Shops");
        }
        public static int AddShop(string shopName,string adress,int shopM)
        {
            return DalHelper.Insert($"INSERT INTO Shops(ShopName,Address,ShopManagerID) VALUES('{shopName}','{adress}',{shopM})");
        }
    }
}
