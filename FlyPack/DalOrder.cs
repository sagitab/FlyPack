using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
    public class DalOrder
    {
        public static int AddOrder(int CustomerID, int DeliveryID, int ShopID, DateTime AriveTime,  string Status)
        {
            return DalHelper.Insert($"INSERT INTO Orders([CostomerID],[DeliverID],[ShopID],[ArrivalTime],[OrderStutus]) VALUES ({CustomerID},{DeliveryID},{ShopID},#{AriveTime}#,'{Status}')");
        }
        public static bool DeleteOrder(int id)
        {
            bool secess = true;
            try
            {
              secess=  DalHelper.DeleteRowById(id, "Orders","ID");
            }
            catch
            {
                secess = false;
            }
            return secess;
        }
    }
}
