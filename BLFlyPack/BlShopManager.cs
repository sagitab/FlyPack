using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
namespace BLFlyPack
{
   public class BlShopManager:BlOrderUser
    {
        
        public int ShopId { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="pass"></param>
        public BlShopManager(string pass):base(pass)
        {
            ShopId = this.GetShopId();
        }
        /// <summary>
        /// get shop manager table with no shop   
        /// </summary>
        /// <returns>shop manager table</returns>
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
        /// <summary>
        /// Get Num Of Active Customers
        /// </summary>
        /// <returns></returns>
        public override string GetNumOfActiveCustomers()
        {
             return DalOrder.NumOfActiveCustomers($"WHERE([Orders].[ShopID] = {GetShopId()})");
        }
        /// <summary>
        /// get  Deliveries Table that order from the shop manager shop
        /// </summary>
        /// <returns> Deliveries Table</returns>
        public override DataTable DeliveriesTable()
        {
            return DalUser.DeliveriesTableByShop(ShopId);
        }
        /// <summary>
        /// get Customers Table that order from the shop manager shop
        /// </summary>
        /// <returns>Customers Table</returns>
        public override DataTable CustomersTable()
        {
            return DalUser.CustomersTableByShop(ShopId);
        }
        /// <summary>
        /// get Customers Table that order from the shop manager shop and search value
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>Customers Table</returns>
        public override DataTable CustomersSearch(string condition)
        {
            return DalUser.CustomersSearchByShop(ShopId, condition);
        }

    }
}
