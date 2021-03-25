using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.CompilerServices;

namespace FlyPack
{
   public class DalOrderUsers
    {
        /// <summary>
        /// get order data table by user type and if they new or old
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userid"></param>
        /// <param name="newOrOld"></param>
        /// <returns>order data table by user type</returns>
        public static DataTable GetOrders(int type, string userid,string newOrOld)
        {
            switch (type)
            {
                //Dictionary<int, string> types =new Dictionary<int, string>() { { 4,"CustomerID"},{3,"DeliverID"},{1,"ShopID"}};
                case 2:
                    return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Users.FirstName, Shops.ShopName, Users_1.FirstName AS DeliveryName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN(Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(((Users.ID) =[Orders].[DeliverID] Or(Users.ID) =[Orders].[CustomerID]){newOrOld}) ORDER BY Orders.ArrivalTime;");
                case 4:
                    return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Shops.ShopName, Users_1.FirstName AS DeliveryName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN(Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(((Users.ID) =[Orders].[CustomerID]) AND Users.ID ='{userid}' {newOrOld}) ORDER BY Orders.ArrivalTime;");
                case 1:
                    return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Users_1.FirstName AS DeliveryName, Users.FirstName,Orders.ReadyTime FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN (Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(([Shops].[ID] =[Orders].[ShopID]) {newOrOld}) ORDER BY Orders.ArrivalTime;");
                default:
                    return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Shops.ShopName, Users.FirstName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN (Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(([Users_1].[ID] =[Orders].[DeliverID]) AND [Users_1].[ID] ='{userid}' {newOrOld}) ORDER BY Orders.ArrivalTime;");
            }
        }
    }
}
