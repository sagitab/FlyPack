using System;
using System.Data;

namespace FlyPack
{
    public class DalUser
    {
        //User
        public static DataTable IsExist(string pass)
        {
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);
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
            DbHelper helper = new DbHelper(Constants.Provider, Constants.Path);
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

        public static DataRow GetUserById(string userId)
        {
            return DalHelper.GetRowById(userId, "Users");
        }
        //shop Manager
        public static int GetshopId(string managerId)
        {
            return int.Parse(DalHelper.Select($"SELECT Shops.ID FROM Shops WHERE(((Shops.ShopManagerID) = '{managerId}'));").Rows[0]["ID"].ToString());
        }
        public static DataTable ShopManagersWithNoShop()
        {
            return DalHelper.Select("SELECT Users.FirstName, Users.ID FROM Users INNER JOIN Shops ON Users.ID <> Shops.ShopManagerID WHERE(((Users.UserType) = 1)); ");
        }
     
        //Customer
        public static DataTable CustomersTable()
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber, Count(*) AS [Num of orders] FROM Users WHERE(Users.UserType = 4) GROUP BY Users.FirstName, Users.Email, Users.PhoneNumber; ");
        }
        public static DataTable CustomersTableByShop(int shopId)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber, Count(*) AS [Num of orders] FROM Users INNER JOIN Orders ON(Users.ID = Orders.CustomerID) WHERE(((Users.UserType)= 4) AND([Orders].[ShopID]= {shopId})) GROUP BY Users.FirstName, Users.Email, Users.PhoneNumber;");
        }
        public static DataTable CustomersSearch(string condition)
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber, Count(*) AS [Num of orders] FROM Users WHERE((Users.UserType = 4) AND{condition}) GROUP BY Users.FirstName, Users.Email, Users.PhoneNumber");
        }
        public static DataTable CustomersSearchByShop(int shopId, string condition)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber, Count(*) AS [Num of orders] FROM Users INNER JOIN Orders ON(Users.ID = Orders.CustomerID) WHERE(((Users.UserType)= 4) AND([Orders].[ShopID]= {shopId})AND{condition}) GROUP BY Users.FirstName, Users.Email, Users.PhoneNumber;");
        }
        public static string GetName(string customerId)
        {
            return DalHelper.Select($"SELECT Users.FirstName FROM Users WHERE  Users.ID='{customerId}'").Rows[0]["FirstName"].ToString();
        }
        //delivery
        public static DataTable DeliveriesTable()
        {
            return DalHelper.Select($"SELECT Users.FirstName,Users.Email,Users.PhoneNumber, Count(*) AS [Num of orders] FROM Users WHERE((Users.UserType = 3) AND (Users.ID<>'111111111')) GROUP BY Users.FirstName, Users.Email, Users.PhoneNumber");
        }
        public static DataTable DeliveriesTableByShop(int shopId)
        {
            return DalHelper.Select($"SELECT Users.FirstName, Users.Email, Users.PhoneNumber, Count(*) AS [Num of orders] FROM Users INNER JOIN Orders ON(Users.ID = Orders.DeliverID) WHERE(((Users.UserType)= 3) AND([Orders].[ShopID]= {shopId}) AND (Users.ID<>'111111111')) GROUP BY Users.FirstName, Users.Email, Users.PhoneNumber;");
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
