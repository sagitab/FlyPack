using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyPack
{
    public class Constants
    {
        public static string Provider = @"Microsoft.ACE.OLEDB.12.0";
        public static string Path = "";/*@"C:\Users\User\Desktop\FlyPack\DataBaseFlyPack (2).accdb*/
        public Constants()
        {

        }
        public Constants(string path)
        {
            Path = path;
        }
    }
}
