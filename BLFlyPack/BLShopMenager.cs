using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
namespace BLFlyPack
{
   public class BLShopMenager:BLUser
    {
        
        public int ShopID { get; set; }
        public BLShopMenager(string pass):base(pass)
        {
            ShopID = this.GetshopID();
        }
        public static DataTable ShopManegerTable()
        {
            DataTable t = null;
            try
            {
                t = DalUser.ShopManegerTable();
            }
            catch
            {
                return null;
            }
            return t;
        }
        public override string GetNumOfActiveCustomers()
        {
             return DalOrder.NumOfActiveCustomers($"WHERE([Orders].[ShopID] = {GetshopID()})");
        }
        public override DataTable DeliveriesTable()
        {
            return DalUser.DeliveriesTableByShop(ShopID);
        }
        public override DataTable CustomersTable()
        {
            return DalUser.CustomersTableByShop(ShopID);
        }
        public override DataTable CustomersSerch(string condition)
        {
            return DalUser.CustomersSearchByShop(ShopID, condition);
        }

    }
}
