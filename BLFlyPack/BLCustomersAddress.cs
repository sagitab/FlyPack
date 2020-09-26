using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BLCustomersAddress
    {
      public  Point location { get; }
        public  int NumOfFloor { get; }
        public  string CustomerName { get; }

        public BLCustomersAddress(Point point, int numOfFloor, string customerName)
        {
            location= new Point(point);
              NumOfFloor = numOfFloor;
            CustomerName = customerName;
        }
      
        
        //public BLCustomers(string email, string phone, string fname, string lname, string adress, string password, int type, int NumOfFloor):base(email,phone,fname,lname,adress,password,type)
        //{
        //    this.NumOfFloor = NumOfFloor;
        //    //call dal
        //}
    }
}
