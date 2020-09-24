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
        public Point Possision { get; }

        public BLUser(string userId, int type, string email, string phone, string firstName, string lastName, string password, double lat, double lng)
        {
            FlyPack.DalUser.AddUser(email, phone, firstName, lastName, password, type, userId, lat, lng);
            UserID = userId; 
            Type = type;
            Email = email; 
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Possision = new Point(lat, lng);
        }

        //public BLUser(string userId)
        //{

        //}
        public BLUser(string pass)
        {
            DataTable t = DalUser.IsExsist(pass);
            if (t != null)
            {
                DataRow row = t.Rows[0];
                UserID = row["ID"].ToString();
                Type = int.Parse(row["UserType"].ToString());
                Email = row["Email"].ToString();
                Phone = row["PhoneNumber"].ToString();
                FirstName = row["FirstName"].ToString();
                LastName = row["LastName"].ToString();
                Password = row["Password"].ToString();
                Possision = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
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
            Possision = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
        }
        public static bool PasswordCheck(string pass)
        {
            DataTable t = DalUser.IsExsist(pass);
            if (t.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public virtual DataTable DeliveriesTable()
        {
            DataTable t = null;
            try
            {
                t = DalUser.DeliveriesTable();
            }
            catch
            {
                return null;
            }
            return t;
        }
        public virtual DataTable CustomersTable()
        {
            DataTable t = null;
            try
            {
                t = DalUser.CustomersTable();
            }
            catch
            {
                return null;
            }
            return t;
        }
        public virtual DataTable CustomersSerch(string condition)
        {
            DataTable t = null;
            try
            {
                t = DalUser.CustomersSearch(condition);
            }
            catch
            {
                return null;
            }
            return t;
        }
        public int GetshopID()
        {

            return DalUser.GetshopID(this.UserID);
        }
        public int GetNumOfOrders()
        {
            if (Type == 1)
            {
                return DalOrder.NumOfOrders($"WHERE([Orders].[ShopID] = {GetshopID()})");
            }
            else
            {
                return DalOrder.NumOfOrders("");
            }
        }
        public virtual string GetNumOfActiveCustomers()
        {
            if (Type == 1)
            {
                return DalOrder.NumOfActiveCustomers($"WHERE([Orders].[ShopID] = {GetshopID()})");
            }
            else
            {
                return DalOrder.NumOfActiveCustomers("");
            }
        }

        public static string GetName(string CustomerID)
        {
            return DalUser.GetName(CustomerID);
        }

        public static List<Point> GetDeliveryiesPossitions()
        {
            DataTable DeliveryiesPossitions = DalUser.GetDeliveryiesPossitions();

            return (from DataRow row in DeliveryiesPossitions.Rows select new Point(double.Parse(row["Lat"].ToString()), double.Parse((string) row["Lng"]))).ToList();
        }

        public static string GetDeliveryIDByPoint(Point point)
        {
            string ret = "";
            try
            {
                ret = DalUser.GetDeliveryIDByPoint(point.Lat, point.Lng);
            }
            catch 
            {
                return ret;
            }
            return ret;
        }
        public static string GetMatchDeliveryIDByPoints(List<Point> points)
        {
            int index=0;
            while (index+1<=points.Count&&GetNumOfDeliveryOrders(GetDeliveryIDByPoint(points[index]))>=6)
            {
                index++;
            }

            if (index + 1 > points.Count)
            {
                return "";
            }
            return GetDeliveryIDByPoint(points[index]);
        }
        public static string GetMatchesDeliveryID(Point shopPoint)
        {
            List<Point> points = GetDeliveryiesPossitions();
            List<Point> SortedDeliveryPoints = shopPoint.SelectSort(points);
            
            //int index = shopPoint.MinimumDistance(points,0);
            //Point deliveryPoint = points[index];
            return GetMatchDeliveryIDByPoints(SortedDeliveryPoints);
        }

        public static int GetNumOfDeliveryOrders(string UserID)
        {
            return DalOrder.NumOfOrders($"WHERE Orders.DeliverID ={UserID} AND Orders.OrderStutus = 4");
        }
    }
}
