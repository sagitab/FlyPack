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
        public static DataTable GetOrders(int type, string Userid,string newOrOld)
        {
            //Dictionary<int, string> types =new Dictionary<int, string>() { { 4,"CustomerID"},{3,"DeliverID"},{1,"ShopID"}};
            if(type==2)
            {
                return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Users.FirstName, Shops.ShopName, Users_1.FirstName AS DeliveryName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN(Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(((Users.ID) =[Orders].[DeliverID] Or(Users.ID) =[Orders].[CustomerID]){newOrOld}) ORDER BY Orders.ArrivalTime;");
            }
            else if(type==4)
            {
                return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Shops.ShopName, Users_1.FirstName AS DeliveryName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN(Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(((Users.ID) =[Orders].[CustomerID]){newOrOld}) ORDER BY Orders.ArrivalTime;");
            }
            else if(type==1)
            {
                return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Users_1.FirstName AS DeliveryName, Users.FirstName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN (Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(([Shops].[ID] =[Orders].[ShopID]){newOrOld}) ORDER BY Orders.ArrivalTime;");
            }
            else
            {
                return DalHelper.Select($"SELECT Orders.ID,Orders.ArrivalTime, Orders.OrderStutus, Shops.ShopName, Users.FirstName FROM Users AS Users_1 INNER JOIN(Shops INNER JOIN (Users INNER JOIN Orders ON Users.ID = Orders.[CustomerID]) ON Shops.ID = Orders.ShopID) ON Users_1.ID = Orders.DeliverID WHERE(([Users_1].[ID] =[Orders].[DeliverID]){newOrOld}) ORDER BY Orders.ArrivalTime;");
            }
          
        }
    }
}
