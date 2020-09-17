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
        public string UserID { get; }
        public int Type { get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public BLUser(string email, string phone, string fname, string lname, string password,int type,string id)
        {
            FlyPack.DalUser.AddUser(email, phone, fname, lname, password, type, id);
            Type = type;
            Email = email;
            Phone = phone;
            FirstName = fname;
            LastName = lname;
            Password = password;
            
        }
        public BLUser(string pass)
        {
            DataTable t = DalUser.IsExsist(pass);
            if(t!=null)
            {
                DataRow row = t.Rows[0];
                UserID = row["ID"].ToString();
                Type = int.Parse(row["UserType"].ToString());
                Email = row["Email"].ToString();
                Phone = row["PhoneNumber"].ToString();
                FirstName = row["FirstName"].ToString();
                LastName = row["LastName"].ToString();
                Password = row["Password"].ToString();
            }
            
           
        }
        public BLUser(DataRow row)
        {
            UserID = row["ID"].ToString();
            Type = int.Parse(row["UserType"].ToString());
            Email = row["Email"].ToString();
            Phone = row["PhoneNumber"].ToString();
            FirstName = row["FirstName"].ToString();
            LastName = row["LastName"].ToString();
            Password = row["Password"].ToString();
        }
        public static bool PasswordCheck(string pass)
        {
            DataTable t = DalUser.IsExsist(pass);
            if(t.Rows.Count>0)
            {
                return false;
            }
            return true;
        }
        public static DataTable ShopManegerTable()
        {
            DataTable t =null;
            try
            {
                t = DalUser.ShopManegerTable();
            }
            catch
            {
                return null;
            }
            return t;
        }
    }
}
