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
        public static DataTable ShopManegerTable()
        {
            return DalHelper.Select("SELECT Users.FirstName, Users.ID FROM Users WHERE(Users.UserType = 1)");
        }
        public static DataTable DeliveriesTable()
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber FROM Users WHERE((Users.UserType = 3) AND (Users.ID<>'111111111'))");
        }
        public static DataTable DeliveriesTableByShop(int ShopID)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber FROM Users INNER JOIN Orders ON(Users.ID = Orders.DeliverID) WHERE(((Users.UserType)= 3) AND([Orders].[ShopID]= {ShopID})  );");
        }
        
        public static DataTable CustomersTable()
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber FROM Users WHERE(Users.UserType = 4)");
        }
        public static DataTable CustomersTableByShop(int ShopID)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber FROM Users INNER JOIN Orders ON(Users.ID = Orders.CustomerID) WHERE(((Users.UserType)= 4) AND([Orders].[ShopID]= {ShopID}));");
        }
        public static DataTable CustomersSearch(string conditon)
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber FROM Users WHERE((Users.UserType = 4) AND{conditon})");
        }
        public static DataTable CustomersSearchByShop(int ShopID, string conditon)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber FROM Users INNER JOIN Orders ON(Users.ID = Orders.CustomerID) WHERE(((Users.UserType)= 4) AND([Orders].[ShopID]= {ShopID})AND{conditon});");
        }
        public static int GetshopID(string ManegerID)
        {
            return int.Parse( DalHelper.Select($"SELECT Shops.ID FROM Shops WHERE(((Shops.ShopManagerID) = '{ManegerID}'));").Rows[0]["ID"].ToString());

        }
        
    }
}
