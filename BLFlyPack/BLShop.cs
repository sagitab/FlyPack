using FlyPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BLShop
    {
        
       
        public int ID { get; }
        public int ShopManegerID { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string ShopName { get; set; }
      

        public BLShop( int shopManegerId, double lat, double lng, string shopName)
        {
            try
            {
                ID = DalShop.AddShop(shopManegerId, lat, lng, shopName);
            }
            catch
            {
                ID = -1;
            }
            ShopManegerID = shopManegerId;
            Lat = lat;
            Lng = lng;
            ShopName = shopName;
        }
        public BLShop(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Lat =double.Parse( row["Lat"].ToString());
            Lng = double.Parse(row["Lng"].ToString());
            ShopName = row["ShopName"].ToString();
        }
        public static List<BLShop> GetShops()
        {
            DataTable shops = DalShop.GetShopTable();
            return (from DataRow row in shops.Rows select new BLShop(row)).ToList();
        }

        public static BLShop GetShopById(int ID)
        {
            return new BLShop(DalShop.GetShop(ID));
        }

    }
}
