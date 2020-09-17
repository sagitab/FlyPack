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
        public string Adress { get; set; }
        public string ShopName { get; set; }
        public BLShop( int shopm,string adress,string shopname)
        {
            
            try
            {
                ID = DalShop.AddShop(shopname, adress, shopm);
            }
            catch
            {
                ID = -1;
            }
            Adress = adress;
            ShopName = shopname;
            ShopManegerID = shopm;
        }
        public BLShop(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Adress = row["Address"].ToString();
            ShopName = row["ShopName"].ToString();
        }
        public static List<BLShop> GetShops()
        {
            DataTable shops= DalShop.GetShopTable();
            List<BLShop> shops1 = new List<BLShop>();
            foreach(DataRow row in shops.Rows)
            {
                shops1.Add(new BLShop(row));
            }
            return shops1;
        }
        
             
    }
}
