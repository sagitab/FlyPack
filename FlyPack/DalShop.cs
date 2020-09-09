using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace FlyPack
{
   public class DalShop
    {
        public static DataTable GetShopTable()
        {
            return DalHelper.AllFromTable("Shops");
        }
    }
}
