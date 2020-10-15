using FlyPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BlShop
    {
        public int Id { get; }
        public string ShopManagerId { get; set; }
        public string ShopName { get; set; }
        public  Point Location { get; }
        public BlShop( string shopManagerId, string shopName)
        {
            try
            {
                Id = DalShop.AddShop(shopManagerId,shopName);
                Location = GetPosission(Id);
            }
            catch
            {
                Id = -1;
            }
            ShopManagerId = shopManagerId;
           
            ShopName = shopName;
          
        }
        public BlShop(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            ShopName = row["ShopName"].ToString();
            ShopManagerId= row["ShopManagerID"].ToString();
            Location = GetPosission(Id);
        }
        public static List<BlShop> GetShops()
        {
            DataTable shops = DalShop.GetShopTable();
            return (from DataRow row in shops.Rows select new BlShop(row)).ToList();
        }
        public static BlShop GetShopById(int id)
        {
            DataRow row = null;
            try
            {
                 row = DalShop.GetShop(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return null;
            }
            return new BlShop(row);
        }
        public static Point GetPosission(int shopId)
        {
            DataRow row = DalShop.GetLocation(shopId);
            return new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()));
        }
    }
}
