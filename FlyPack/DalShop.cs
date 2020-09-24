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
        public static int AddShop(string shopManegerId,string shopName)
        {
            return DalHelper.Insert($"INSERT INTO Shops(ShopName,ShopManagerID) VALUES('{shopName}','{shopManegerId}')");
        }

        public static DataRow GetShop(int Id)
        {
            return DalHelper.AllWhere("Shops", "ID", Id).Rows[0];
        }

        public static DataRow GetPosission(int shopID)
        {
            return DalHelper.Select("SELECT Users.Lat, Users.Lng FROM Users INNER JOIN Shops ON Users.ID = Shops.ShopManagerID WHERE(([Shops].[ID] = 1));").Rows[0];
        }
    }
}
