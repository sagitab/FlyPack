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
        public static DataTable GetOrders(int type, int Userid)
        {
            Dictionary<int, string> types =new Dictionary<int, string>() { { 4,"CustomerID"},{3,"DeliverID"},{1,"ShopID"}};
            if(type==2)
            {
                return DalHelper.Select($"SELECT Orders.ArrivalTime, Orders.OrderStutus, Users.FirstName, Shops.ShopName FROM Users INNER JOIN(Shops INNER JOIN Orders ON Shops.ID = Orders.ShopID) ON(Users.ID = Shops.ShopManagerID) AND(Users.ID = Orders.DeliverID) AND(Users.ID = Orders.[CustomerID]) ORDER BY Orders.ArrivalTime DESC;");
            }
            return DalHelper.Select($"SELECT Orders.ArrivalTime, Orders.OrderStutus, Users.FirstName, Shops.ShopName FROM Users INNER JOIN(Shops INNER JOIN Orders ON Shops.ID = Orders.ShopID) ON(Users.ID = Shops.ShopManagerID) AND(Users.ID = Orders.DeliverID) AND(Users.ID = Orders.[CustomerID]) WHERE[{types[type]}] = '{Userid}' ORDER BY Orders.ArrivalTime DESC;");
        }
    }
}
