using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
namespace BLFlyPack
{
   public class BLUser
    {
        public int UserID { get; }
        public int Type { get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Password { get; set; }
        public BLUser(string email, string phone, string fname, string lname, string adress, string password,int type,int numOfFloor)
        {
            UserID = FlyPack.DalUser.AddUser(email, phone, fname, lname, adress, password, type,numOfFloor);
            Type = type;
            Email = email;
            Phone = phone;
            FirstName = fname;
            LastName = lname;
            Adress = adress;
            Password = password;
            
        }
        public BLUser(string pass)
        {
            DataTable t = DalUser.IsExsist(pass);
            if(t!=null)
            {
                DataRow row = t.Rows[0];
                UserID = int.Parse(row["ID"].ToString());
                Type = int.Parse(row["Type"].ToString());
                Email = row["Email"].ToString();
                Phone = row["PhoneNumber"].ToString();
                FirstName = row["FirstName"].ToString();
                LastName = row["LastName"].ToString();
                Adress = row["Adress"].ToString();
                Password = row["Password"].ToString();
            }
            
           
        }
        public static bool PasswordCheck(string pass)
        {
            DataTable t = DalUser.IsExsist(pass);
            if(t!=null)
            {
                return false;
            }
            return true;
        }
    }
}
