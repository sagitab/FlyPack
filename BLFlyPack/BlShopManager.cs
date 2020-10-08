using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
namespace BLFlyPack
{
   public class BlShopManager:BlUser
    {
        
        public int ShopId { get; set; }
        public BlShopManager(string pass):base(pass)
        {
            ShopId = this.GetshopId();
        }
        public static DataTable ShopManagerTable()
        {
            DataTable t = null;
            try
            {
                t = DalUser.ShopManagersWithNoShop();
            }
            catch
            {
                return null;
            }
            return t;
        }
        public override string GetNumOfActiveCustomers()
        {
             return DalOrder.NumOfActiveCustomers($"WHERE([Orders].[ShopID] = {GetshopId()})");
        }
        public override DataTable DeliveriesTable()
        {
            return DalUser.DeliveriesTableByShop(ShopId);
        }
        public override DataTable CustomersTable()
        {
            return DalUser.CustomersTableByShop(ShopId);
        }
        public override DataTable CustomersSearch(string condition)
        {
            return DalUser.CustomersSearchByShop(ShopId, condition);
        }

    }
}
