using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FlyPack;
namespace BLFlyPack
{
    public class BlUser
    {

        public string UserId { get; }
        public int Type { get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public Point Location { get; }
        //user
        public BlUser(string userId, int type, string email, string phone, string firstName, string lastName, string password, double lat, double lng)
        {
            FlyPack.DalUser.AddUser(email, phone, firstName, lastName, password, type, userId, lat, lng);
            UserId = userId;
            Type = type;
            Email = email;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Location = new Point(lat, lng);
        }

        //public BLUser(string userId)
        //{

        //}
        public BlUser(string pass)
        {
            DataTable t = DalUser.IsExist(pass);
            if (t != null&&t.Rows.Count>0)
            {
                DataRow row = t.Rows[0];
                UserId = row["ID"].ToString();
                Type = int.Parse(row["UserType"].ToString());
                Email = row["Email"].ToString();
                Phone = row["PhoneNumber"].ToString();
                FirstName = row["FirstName"].ToString();
                LastName = row["LastName"].ToString();
                Password = row["Password"].ToString();
                Location = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            }


        }

        public static BlUser UserById(string userId)
        {
            DataRow row = null;
            try
            {
                row = DalUser.GetUserById(userId);
            }
            catch
            {
                return null;
            }
            return UserByRow(row);

        }


        public static BlUser UserByRow (DataRow row)
        {
            string userId = row["ID"].ToString();
            int type = int.Parse(row["UserType"].ToString());
            string email = row["Email"].ToString();
            string phone = row["PhoneNumber"].ToString();
            string firstName = row["FirstName"].ToString();
            string lastName = row["LastName"].ToString();
            string password = row["Password"].ToString();
            Point possision = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            return  new BlUser(userId, type, email, phone, firstName, lastName, password, possision.Lat, possision.Lng);
        }
        public static bool PasswordCheck(string pass)
        {
            DataTable t = DalUser.IsExist(pass);
            if (t.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            try
            {
                Dictionary<int, string> usersTypes = new Dictionary<int, string> { { 1, "Shop Manager" }, { 2, "System Manager" }, { 3, "Deliver" }, { 4, "Customer" } };

                return $"{FirstName} {LastName} Deer {usersTypes[Type]}";
            }
            catch 
            {
                return "";
            }
          
        }

        //shop maneger
        public int GetshopId()
        {

            return DalUser.GetshopId(this.UserId);
        }
        public int GetNumOfOrders()
        {
            return DalOrder.NumOfOrders(Type == 1 ? $"WHERE([Orders].[ShopID] = {GetshopId()})" : "");
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
                return DalOrder.NumOfActiveCustomers($"WHERE([Orders].[ShopID] = {GetshopId()})");
            }
            else
            {
                return DalOrder.NumOfActiveCustomers("");
            }
        }

        public static string GetName(string customerId)
        {
            return DalUser.GetName(customerId);
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
        public static List<Point> GetDeliveriesLocations()
        {
            DataTable deliverersLocations = DalUser.GetDeliverersLocations();

            return (from DataRow row in deliverersLocations.Rows select new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()))).ToList();
        }

        public static string GetDeliveryIdByPoint(Point point)
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
        public static string GetMatchDeliveryIdByPoints(List<Point> points)
        {
            int index = 0;
            while (index + 1 <= points.Count && GetNumOfDeliveryOrders(GetDeliveryIdByPoint(points[index])) >= 6)//continue loop if delivery orders is full
            {
                index++;
            }

            if (index + 1 > points.Count)
            {
                return "";
            }
            return GetDeliveryIdByPoint(points[index]);
        }
        public static string GetMatchesDeliveryId(Point shopPoint)
        {
            List<Point> points = GetDeliveriesLocations();
            List<Point> sortedDeliveryPoints = shopPoint.SelectSort(points);

            //int index = shopPoint.MinimumDistance(points,0);
            //Point deliveryPoint = points[index];
            return GetMatchDeliveryIdByPoints(sortedDeliveryPoints);
        }

        public static int GetNumOfDeliveryOrders(string userId)
        {
            return DalOrder.NumOfOrders($"WHERE Orders.DeliverID ='{userId}' AND Orders.OrderStutus =4");
        }

        public  double GetDistanceToCustomerHome(List<BlShop> shops, List<BlCustomersAddress> customersAddresses)
        {
            Point startPoint=new Point(Location);
            double totalDistance = 0.0;

            for (int index = 0; index < shops.Count; index++)
            {
                Point shop = shops[index].Location;
                Point customer = customersAddresses[index].Location;
                totalDistance += startPoint.Distance(shop) + shop.Distance(customer);
                startPoint=new Point(customer);
            }

            return totalDistance;
        }
    }
}
