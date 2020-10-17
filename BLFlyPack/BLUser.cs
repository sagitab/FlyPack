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
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public BlUser(string userId, int type, string email, string phone, string firstName, string lastName, string password, double lat, double lng)
        {
            try
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
            catch
            {
                UserId = "-1";
            }
         
        }

        //public BLUser(string userId)
        //{

        //}
        /// <summary>
        /// constructor by data row
        /// </summary>
        /// <param name="pass"></param>
        public BlUser(string pass)
        {
            DataTable t = null;
            try
            {
                t = DalUser.IsExist(pass);
            }
            catch
            {
                return;
            }

            if (t == null || t.Rows.Count <= 0) return;
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
        /// <summary>
        /// return a user object by user name and password
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="UserName"></param>
        public BlUser(string pass,string UserName)
        {
            DataTable t = null;
            try
            {
                t = DalUser.IsExist(pass);
            }
            catch 
            {
               return;
            }

            if (t == null || t.Rows.Count <= 0) return;
            DataRow row = t.Rows[0];
            if (UserName!= row["FirstName"].ToString())
            {
                return;
            }
            UserId = row["ID"].ToString();
            Type = int.Parse(row["UserType"].ToString());
            Email = row["Email"].ToString();
            Phone = row["PhoneNumber"].ToString();
            FirstName = row["FirstName"].ToString();
            LastName = row["LastName"].ToString();
            Password = row["Password"].ToString();
            Location = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));


        }
        /// <summary>
        /// get user object by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user object</returns>
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

        /// <summary>
        /// get user object by data row
        /// </summary>
        /// <param name="row"></param>
        /// <returns>user object</returns>
        public static BlUser UserByRow (DataRow row)
        {
            string userId = row["ID"].ToString();
            int type = int.Parse(row["UserType"].ToString());
            string email = row["Email"].ToString();
            string phone = row["PhoneNumber"].ToString();
            string firstName = row["FirstName"].ToString();
            string lastName = row["LastName"].ToString();
            string password = row["Password"].ToString();
            Point position = new Point(double.Parse(row["Lng"].ToString()), double.Parse(row["Lng"].ToString()));
            return  new BlUser(userId, type, email, phone, firstName, lastName, password, position.Lat, position.Lng);
        }
        /// <summary>
        /// check if password already exist
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool PasswordCheck(string pass)
        {
            DataTable t = DalUser.IsExist(pass);
            return t.Rows.Count <= 0;
        }
        /// <summary>
        /// describe the user object
        /// </summary>
        /// <returns>string that describe the user object</returns>
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
        /// <summary>
        /// Get Shop Id
        /// </summary>
        /// <returns>Shop Id</returns>
        public int GetShopId()
        {
            return DalUser.GetShopId(this.UserId);
        }
        /// <summary>
        /// Get Num Of Orders
        /// </summary>
        /// <returns>Num Of Orders</returns>
        public int GetNumOfOrders()
        {
            return DalOrder.NumOfOrders(Type == 1 ? $"WHERE([Orders].[ShopID] = {GetShopId()})" : "");
        }
        //customer
        /// <summary>
        /// get Customers Table
        /// </summary>
        /// <returns>Customers Table</returns>
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
        /// <summary>
        /// get customer data table by search value
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>customer data table</returns>
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
            return DalOrder.NumOfActiveCustomers(Type == 1 ? $"WHERE([Orders].[ShopID] = {GetShopId()})" : "");
        }

        public  string GetName()
        {
            return DalUser.GetName(this.UserId);
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
        /// <summary>
        /// get point list of all deliveries location
        /// </summary>
        /// <returns> list of all deliveries location</returns>
        public static List<Point> GetDeliveriesLocations()
        {
            DataTable deliverersLocations = DalUser.GetDeliverersLocations();

            return (from DataRow row in deliverersLocations.Rows select new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()))).ToList();
        }
        /// <summary>
        /// get Delivery Id By Point
        /// </summary>
        /// <param name="point"></param>
        /// <returns>Delivery Id</returns>
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
        /// <summary>
        /// return the closest delivery that available
        /// </summary>
        /// <param name="points"></param>
        /// <returns>delivery id</returns>
        public static string GetMatchDeliveryIdByPoints(List<Point> points)
        {
            int index = 0;
            while (index + 1 <= points.Count && GetNumOfDeliveryOrders(GetDeliveryIdByPoint(points[index])) >= 6)//continue loop if delivery orders is full
            {
                index++;
            }

            return index + 1 > points.Count ? "" : GetDeliveryIdByPoint(points[index]);
        }
        /// <summary>
        /// return the closest delivery that available by shop point
        /// </summary>
        /// <param name="points"></param>
        /// <returns>delivery id</returns>
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
        /// <summary>
        /// return the total distance between the delivery to the customer home  
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="customersAddresses"></param>
        /// <returns></returns>
        public double GetDistanceToCustomerHome(List<BlShop> shops, List<BlCustomersAddress> customersAddresses)
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
