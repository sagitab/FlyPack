using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BLCustomersAddress
    {
        public  double Lat { get; }
        public double Lng { get; }
        public  int NumOfFloor { get; }
        public  string CustomerName { get; }

        public BLCustomersAddress(double lat, double lng, int numOfFloor, string customerName)
        {
            Lat = lat;
            Lng = lng;
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
