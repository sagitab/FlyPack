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

        public static int MaxOrderForDeliver //The maximum number of orders a deliver can make in one round
        {
            get { return 6; }
        }
        public  static Dictionary<string,string> UnVerifyEmail { get; set; }
        public static int ShopManager
        {
            get;
            set;
        }
        public static int SystemManager
        {
            get
            {
                return 2;
            }
        }
        public static int Customer
        {
            get
            {
                return 4;
            }
        }
        public static int Delivery
        {
            get
            {
                return 3;
            }
        }
    }
}
