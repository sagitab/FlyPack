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
        public Point location { get; }
        //user
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
            location = new Point(lat, lng);
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
                location = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            }


        }

        public static BLUser UserByID(string UserID)
        {
            DataRow row = null;
            try
            {
                row = DalUser.GetUserByID(UserID);
            }
            catch
            {
                return null;
            }
            return UserByRow(row);

        }


        public static BLUser UserByRow (DataRow row)
        {
            string UserID = row["ID"].ToString();
            int Type = int.Parse(row["UserType"].ToString());
            string Email = row["Email"].ToString();
            string Phone = row["PhoneNumber"].ToString();
            string FirstName = row["FirstName"].ToString();
            string LastName = row["LastName"].ToString();
            string Password = row["Password"].ToString();
            Point Possision = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            return  new BLUser(UserID, Type, Email, Phone, FirstName, LastName, Password, Possision.Lat, Possision.Lng);
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

        public override string ToString()
        {
            Dictionary<int,string> UsersTypes=new Dictionary<int, string> {{1, "Shop Manager" }, { 2, "System Manager" } , { 3, "Deliver" } , { 4, "Customer" } };

            return $"{FirstName} {LastName} Deer {UsersTypes[Type]}";
        }

        //shop maneger
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
        //customer
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
        public virtual DataTable CustomersSearch(string condition)
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
        //delivery
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
        public static List<Point> GetDeliveryiesLocations()
        {
            DataTable deliverersLocations = DalUser.GetDeliverersLocations();

            return (from DataRow row in deliverersLocations.Rows select new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()))).ToList();
        }

        public static string GetDeliveryIDByPoint(Point point)
        {
            string ret = "";
            try
            {
                ret = DalUser.GetDeliveryIdByPoint(point.Lat, point.Lng);
            }
            catch
            {
                return ret;
            }
            return ret;
        }
        public static string GetMatchDeliveryIDByPoints(List<Point> points)
        {
            int index = 0;
            while (index + 1 <= points.Count && GetNumOfDeliveryOrders(GetDeliveryIDByPoint(points[index])) >= 6)//continue loop if delivery orders is full
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
            List<Point> points = GetDeliveryiesLocations();
            List<Point> SortedDeliveryPoints = shopPoint.SelectSort(points);

            //int index = shopPoint.MinimumDistance(points,0);
            //Point deliveryPoint = points[index];
            return GetMatchDeliveryIDByPoints(SortedDeliveryPoints);
        }

        public static int GetNumOfDeliveryOrders(string UserID)
        {
            return DalOrder.NumOfOrders($"WHERE Orders.DeliverID ='{UserID}' AND Orders.OrderStutus =4");
        }

        public  double GetDistanceToCustomerHome(List<BLShop> shops, List<BLCustomersAddress> customersAddresses)
        {
            Point startPoint=new Point(location);
            double TotalDistance = 0.0;

            for (int index = 0; index < shops.Count; index++)
            {
                Point shop = shops[index].location;
                Point customer = customersAddresses[index].location;
                TotalDistance += startPoint.Distance(shop) + shop.Distance(customer);
                startPoint=new Point(customer);
            }

            return TotalDistance;
        }
    }
}
