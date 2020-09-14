using FlyPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
   public class Shop
    {
        public int ID { get; }
        public int ShopManegerID { get; set; }
        public string Adress { get; set; }
        public string ShopName { get; set; }
        public Shop( int shopm,string adress,string shopname)
        {
            ID = -1;//dal
            Adress = adress;
            ShopName = shopname;
            ShopManegerID = shopm;
        }
        public Shop(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Adress = row["Address"].ToString();
            ShopName = row["ShopName"].ToString();
        }
        public static List<Shop> GetShops()
        {
            DataTable shops= DalShop.GetShopTable();
            List<Shop> shops1 = new List<Shop>();
            foreach(DataRow row in shops.Rows)
            {
                shops1.Add(new Shop(row));
            }
            return shops1;
        }
        
             
    }
}
