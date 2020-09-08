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
        public int OrderID { get; }
        public int MaxOrders { get; set; }
        public int ShopManegerID { get; set; }
        public DateTime WorkingHourStart { get; set; }
        public DateTime WorkingHourEnd { get; set; }
        public string Adress { get; set; }
        public string ShopName { get; set; }
        public string WebLink { get; set; }
        public Shop(int maxOrders, int shopm,DateTime workstart, DateTime workend,string adress,string shopname,string weblink)
        {
            OrderID = -1;//dal
            MaxOrders = maxOrders;
            ShopManegerID = shopm;
            WorkingHourStart = workstart;
            WorkingHourEnd = workend;
            Adress = adress;
            ShopName = shopname;
            WebLink = weblink;
        }
        public static DataTable GetShops()
        {
            DataTable t = null;//dal
            return t;
        }
    }
}
