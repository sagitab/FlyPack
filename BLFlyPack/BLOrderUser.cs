using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLFlyPack
{
   public class BLOrderUser:BLUser
    {
        public List<BLOrder> Orders { get; }
        public BLOrderUser(string pass):base(pass)
        {
            //Orders= call dal
        }
        public List<BLOrder> GetOrders()
        {
            DataTable t = null;//call d
            //datat>>>List
            List<BLOrder> orders = null;
            return orders;
        }
    }
}
