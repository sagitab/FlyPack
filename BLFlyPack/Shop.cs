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
        public int MaxOrders { get; set; }
        public int ShopManegerID { get; set; }
        public DateTime WorkingHourStart { get; set; }
        public DateTime WorkingHourEnd { get; set; }
        public string Adress { get; set; }
        public string ShopName { get; set; }
        public string WebLink { get; set; }
        public Shop(int maxOrders, int shopm,DateTime workstart, DateTime workend,string adress,string shopname,string weblink)
        {
            ID = -1;//dal
            MaxOrders = maxOrders;
            ShopManegerID = shopm;
            WorkingHourStart = workstart;
            WorkingHourEnd = workend;
            Adress = adress;
            ShopName = shopname;
            WebLink = weblink;
        }
        public Shop(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            MaxOrders = int.Parse(row["MaxOrder"].ToString());
            ShopManegerID = int.Parse(row["ShopManagerID"].ToString());
            WorkingHourStart =DateTime.Parse( row["WorkingHourStart"].ToString());
            WorkingHourEnd = DateTime.Parse(row["WorkingHourEnd"].ToString());
            Adress = row["Adress"].ToString();
            ShopName = row["ShopName"].ToString();
            WebLink = row["WebLink"].ToString();
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
