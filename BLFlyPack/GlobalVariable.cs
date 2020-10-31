using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
   public class GlobalVariable
    {
        public static double Distance { get; set; }//vriable to save the distance between customer home to deliver
        public static  int Speed//the speed of the drone
        {
            get { return 10; }
        }

        public static int MaxOrderForDeliver//The maximum number of orders a deliver can make in one round
        {
            get { return 6; }
        }
    }
}
