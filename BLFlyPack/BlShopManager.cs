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
            try
            {
                return DalOrder.NumOfActiveCustomers($"WHERE([Orders].[ShopID] = {GetShopId()})");
            }
            catch
            {
                return "";
            }
           
        }
        /// <summary>
        /// get  Deliveries Table that order from the shop manager shop
        /// </summary>
        /// <returns> Deliveries Table</returns>
        public override DataTable DeliveriesTable()
        {
            try
            {
                return DalUser.DeliveriesTableByShop(ShopId);
            }
            catch
            {
                return null;
            }
          
        }
        /// <summary>
        /// get Customers Table that order from the shop manager shop
        /// </summary>
        /// <returns>Customers Table</returns>
        public override DataTable CustomersTable()
        {
            DataTable CustomersTable = null;
            try
            {
               return DalUser.CustomersTableByShop(ShopId);
            }
            catch
            {
                return null;
            }
        }
        public List<BlUser> CustomersList()
        {
            DataTable CustomersTable = null;
            try
            {
                CustomersTable= DalUser.CustomersTableByShop(ShopId);
            }
            catch
            {
                return null;
            }
            if (CustomersTable.Rows.Count == 0) return null;
            List<BlUser> customers = new List<BlUser>();
            foreach (DataRow row in CustomersTable.Rows)
            {
                customers.Add(BlUser.UserByRow(row));
            }
            return customers;
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
        /// <summary>
        /// Get Shop Id
        /// </summary>
        /// <returns>Shop Id</returns>
        public int GetShopId()
        {
            return DalUser.GetShopId(this.UserId);
        }
        /// <summary>
        /// Get Num Of Orders
        /// </summary>
        /// <returns>Num Of Orders</returns>
        public int GetNumOfOrders()
        {
            try
            {
                return DalOrder.NumOfOrders(Type == 1 ? $"WHERE([Orders].[ShopID] = {GetShopId()})" : "");
            }
            catch
            {
                return -1;
            }
          
        }
    }
}
