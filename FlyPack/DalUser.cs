using System;
using System.Data;

namespace FlyPack
{
    public class DalUser
    {
        //User
        public static DataTable IsExsist(string pass)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            helper.OpenConnection();
            string sql = $"SELECT * FROM Users WHERE [Password]='{pass}'";
            DataTable table = null;
            try
            {
                table = helper.GetDataTable(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            helper.CloseConnection();
            return table;
        }
        public static void AddUser(string email, string phone, string fname, string lname, string password, int type, string id, double lat, double lng)
        {
            DBHelper helper = new DBHelper(Constants.PROVIDER, Constants.PATH);
            helper.OpenConnection();
            string sql = $"INSERT INTO Users([Email],[Password],[PhoneNumber],[FirstName],[LastName],[UserType],[ID],Lat,Lng) VALUES ('{email}','{password}','{phone}','{fname}','{lname}','{type}',{id},{lat},{lng})";

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

        public static DataRow GetUserByID(string UserID)
        {
            return DalHelper.GetRowById(UserID, "Users");
        }
        //shop Maneger
        public static int GetshopID(string ManegerID)
        {
            return int.Parse(DalHelper.Select($"SELECT Shops.ID FROM Shops WHERE(((Shops.ShopManagerID) = '{ManegerID}'));").Rows[0]["ID"].ToString());

        }
        public static DataTable ShopManegerTable()
        {
            return DalHelper.Select("SELECT Users.FirstName, Users.ID FROM Users WHERE(Users.UserType = 1)");
        }
     
        //Customer
        public static DataTable CustomersTable()
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber FROM Users WHERE(Users.UserType = 4)");
        }
        public static DataTable CustomersTableByShop(int ShopID)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber FROM Users INNER JOIN Orders ON(Users.ID = Orders.CustomerID) WHERE(((Users.UserType)= 4) AND([Orders].[ShopID]= {ShopID}));");
        }
        public static DataTable CustomersSearch(string condition)
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber FROM Users WHERE((Users.UserType = 4) AND{condition})");
        }
        public static DataTable CustomersSearchByShop(int ShopID, string condition)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber FROM Users INNER JOIN Orders ON(Users.ID = Orders.CustomerID) WHERE(((Users.UserType)= 4) AND([Orders].[ShopID]= {ShopID})AND{condition});");
        }
        public static string GetName(string CustomerID)
        {
            return DalHelper.Select($"SELECT Users.FirstName FROM Users WHERE  Users.ID='{CustomerID}'").Rows[0]["FirstName"].ToString();
        }
        //delivery
        public static DataTable DeliveriesTable()
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber FROM Users WHERE((Users.UserType = 3) AND (Users.ID<>'111111111'))");
        }
        public static DataTable DeliveriesTableByShop(int ShopID)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber FROM Users INNER JOIN Orders ON(Users.ID = Orders.DeliverID) WHERE(((Users.UserType)= 3) AND([Orders].[ShopID]= {ShopID}) AND (Users.ID<>'111111111'));");
        }
        public  static DataTable GetDeliverersLocations()
        {
            return DalHelper.Select("SELECT Users.Lat ,Users.Lng FROM Users WHERE Users.UserType=3 AND Users.ID<>'111111111'");
        }
        public static string GetDeliveryIdByPoint(double lat,double lng)
        {
            return DalHelper.Select($"SELECT Users.ID FROM Users WHERE Users.Lat={lat} AND Users.Lng={lng} AND Users.UserType=3").Rows[0]["ID"].ToString();
        }

        //public static int GetNumOfOrders(string DeliveryID)
        //{
        //    return int.Parse(DalHelper.Select($"SELECT COUNT(Orders.ID) AS NumOfOrders FROM Orders WHERE Orders.DeliverID={DeliveryID} AND Orders.OrderStutus=4").Rows[0]["NumOfOrders"].ToString());
        //}
    }
}
