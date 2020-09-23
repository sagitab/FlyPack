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
        public static int AddShop(int shopManegerId, double lat, double lng, string shopName)
        {
            return DalHelper.Insert($"INSERT INTO Shops(ShopName,ShopManagerID,Lat,Lng) VALUES('{shopName}','{shopManegerId}',{lat},{lng})");
        }

        public static DataRow GetShop(int Id)
        {
            return DalHelper.AllWhere("Shops", "ID", Id).Rows[0];
        }
    }
}
