using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
    public class Constants
    {
        public static string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        public static string PATH= @"..\..\..\DataBaseFlyPack (2).accdb";
        public Constants()
        {

        }
        public Constants(string path)
        {
            PATH = path;
        }
    }
}
