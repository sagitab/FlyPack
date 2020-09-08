using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    public class BLCustomers:BLUser
    {
        public int NumOfFloor { get; set; }
        public BLCustomers(string pass):base(pass)
        {
           
        }
        //public BLCustomers(string email, string phone, string fname, string lname, string adress, string password, int type, int NumOfFloor):base(email,phone,fname,lname,adress,password,type)
        //{
        //    this.NumOfFloor = NumOfFloor;
        //    //call dal
        //}
    }
}
