using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;
namespace BLFlyPack
{
   public class General
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="path"></param>
        public static void SetPath(string path)
        {
            Constants c = new Constants(path);
        }

    }
}
