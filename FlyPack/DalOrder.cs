using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
    public class DalOrder
    {
        public static int AddOrder(string CustomerID, string DeliveryID, int ShopID, DateTime AriveTime,  int Status,string Address,int NumOfFloor, DateTime ReadyTime)
        {
            return DalHelper.Insert($"INSERT INTO Orders([CustomerID],[DeliverID],[ShopID],[ArrivalTime],[OrderStutus],ReadyTime,Address,NumOfFloor) VALUES ({CustomerID},{DeliveryID},{ShopID},#{AriveTime}#,'{Status}',#{ReadyTime}#,'{Address}',{NumOfFloor})");
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
