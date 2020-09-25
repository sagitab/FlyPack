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
        public string ShopManegerID { get; set; }
        public string ShopName { get; set; }
        public  Point Possision { get; }
        
        public BLShop( string shopManegerId, string shopName)
        {
            try
            {
                ID = DalShop.AddShop(shopManegerId,shopName);
                Possision = GetPosission(ID);
            }
            catch
            {
                ID = -1;
            }
            ShopManegerID = shopManegerId;
           
            ShopName = shopName;
          
        }
        public BLShop(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            ShopName = row["ShopName"].ToString();
            ShopManegerID= row["ShopManagerID"].ToString();
            Possision = GetPosission(ID);
        }
        public static List<BLShop> GetShops()
        {
            DataTable shops = DalShop.GetShopTable();
            return (from DataRow row in shops.Rows select new BLShop(row)).ToList();
        }

        public static BLShop GetShopById(int ID)
        {
            DataRow row = null;
            try
            {
                 row = DalShop.GetShop(ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                return null;
            }
            return new BLShop(row);
        }
        public static Point GetPosission(int shopID)
        {
            DataRow row = DalShop.GetPosission(shopID);
            return new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()));
        }
    }
}
