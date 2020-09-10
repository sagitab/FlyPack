using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FlyPack
{
   public class DalUser
    {
        public static DataTable IsExsist(string pass)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER,Constants.PATH);
            helper.OpenConnection();
            string sql = $"SELECT * FROM Users WHERE [Password]='{pass}'";
            DataTable t = null;
            try
            {
                 t = helper.GetDataTable(sql);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            helper.CloseConnection();
            return t;
        }
        public static void AddUser(string email, string phone, string fname, string lname,  string password, int type,string id)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            helper.OpenConnection();
            string sql = $"INSERT INTO Users([Email],[Password],[PhoneNumber],[FirstName],[LastName],[UserType],[ID]) VALUES ('{email}','{password}','{phone}','{fname}','{lname}','{type}',{id})";
            
            try
            {
                helper.WriteData(sql);
            }
            catch
            {
                throw new Exception("sql ex");
            }
            helper.CloseConnection();
            
        }

    }
}
