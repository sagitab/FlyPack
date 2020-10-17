using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BlCustomersAddress
    {
      
      public  Point Location { get; }
        public  int NumOfFloor { get; }
        public  string CustomerName { get; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param  name="point"></param>
        /// <param name="numOfFloor"></param>
        /// <param name="customerName"></param>
        public BlCustomersAddress(Point point, int numOfFloor, string customerName)
        {
            Location= new Point(point);
              NumOfFloor = numOfFloor;
            CustomerName = customerName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>description of theCustomersAddress</returns>
        public override string ToString()
        {
            return $"Floor-{NumOfFloor} Customer name-{CustomerName}";
        }

        //public BLCustomers(string email, string phone, string fname, string lname, string adress, string password, int type, int NumOfFloor):base(email,phone,fname,lname,adress,password,type)
        //{
        //    this.NumOfFloor = NumOfFloor;
        //    //call dal
        //}
    }
}
