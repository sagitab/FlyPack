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
        /// <summary>
        /// add a new shop to DB
        /// </summary>
        /// <param name="shopManegerId"></param>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public static int AddShop(string shopManegerId,string shopName)
        {
            return DalHelper.Insert($"INSERT INTO Shops(ShopName,ShopManagerID) VALUES('{shopName}','{shopManegerId}')");
        }
        /// <summary>
        /// get shop data row by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataRow GetShop(int id)
        {
            
            return DalHelper.AllWhere("Shops", "ID", id).Rows[0];
        }
        /// <summary>
        /// get location data row by id
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static DataRow GetLocation(int shopId)
        {
            return DalHelper.Select($"SELECT Users.Lat, Users.Lng FROM Users INNER JOIN Shops ON Users.ID = Shops.ShopManagerID WHERE(([Shops].[ID] = {shopId}));").Rows[0];
        }
    }
}
