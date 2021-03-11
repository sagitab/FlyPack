using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyPack;

namespace BLFlyPack
{
    public class Deliver : BlOrderUser
    {
        public Deliver(string pass) : base(pass)
        {
        }
        /// <summary>
        /// return Deliver from data row
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Deliver</returns>
        public static  Deliver GetDeliver(DataRow row)
        {
            string ID = row["ID"].ToString();
            string FirstName = row["FirstName"].ToString();
           string LastName = row["LastName"].ToString();
           string Email = row["Email"].ToString();
           return new Deliver(FirstName,LastName,Email, ID);
        }
        /// <summary>
        /// return Deliver by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Deliver</returns>
        public static Deliver GetDeliverById(string Id)
        {
           DataRow row= DalUser.GetUserById(Id);
           return GetDeliver(row);
        }
        /// <summary>
        /// create Deliver by first name ,last name ,email amd id.
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="ID"></param>
        public Deliver(string FirstName, string LastName, string Email, string ID) : base("111111111")
        {
            UserId = ID;
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

     

        public int GetNumOfDeliveryOrders()
        {
            return DalOrder.NumOfOrders($"WHERE Orders.DeliverID ='{UserId}' AND Orders.OrderStutus<5 ");
        }

        /// <summary>
        /// return the total distance between the delivery to the customer home  
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="customersAddresses"></param>
        /// <returns></returns>
        public double GetDistanceToCustomerHome(List<BlShop> shops, List<BlCustomersAddress> customersAddresses)
        {
            Point startPoint = new Point(Location);
            double totalDistance = 0.0;
            for (int index = 0; index < shops.Count; index++)
            {
                Point shop = shops[index].Location;
                Point customer = customersAddresses[index].Location;
                totalDistance += startPoint.Distance(shop) + shop.Distance(customer);
                startPoint = new Point(customer);
            }
            return totalDistance;
        }
        /// <summary>
        /// get the number of available  deliveries
        /// </summary>
        /// <returns> Num Of Available Deliveries</returns>
        public static int NumOfAvailableDeliveries()
        {
            int Cnt = 0;
            DataTable Delivers = DalUser.DeliveriesList();
            List<Deliver> delivers= (from DataRow row in Delivers.Rows select GetDeliver(row)).ToList();
            foreach (Deliver deliver in delivers)
            {
                if (deliver.GetNumOfDeliveryOrders()<GlobalVariable.MaxOrderForDeliver)
                {
                    Cnt++;
                }
            }
            return Cnt;
        }
        //public void CalculateBestWay(List<BlShop> shops, List<BlCustomersAddress> customersAddresses,int i,int nextIndex)
        //{
        //    if ()
        //    {

        //    }
        //}
        /////////////////////////to find the closest delivery
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

        /// <summary>
        /// return the closest delivery that available
        /// </summary>
        /// <param name="points"></param>
        /// <returns>delivery id</returns>
        public static string GetMatchDeliveryIdByPoints(List<Point> points)
        {
            int index = 0;
            while (index + 1 <= points.Count && GetDeliverById(GetDeliveryIdByPoint(points[index])).GetNumOfDeliveryOrders() >= GlobalVariable.MaxOrderForDeliver) //continue loop if delivery orders is full
            {
                index++;
            }
            return index + 1 > points.Count ? "" : GetDeliveryIdByPoint(points[index]);
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
        /// get point list of all deliveries location
        /// </summary>
        /// <returns> list of all deliveries location</returns>
        public static List<Point> GetDeliveriesLocations()
        {
            DataTable deliverersLocations = DalUser.GetDeliverersLocations();

            return (from DataRow row in deliverersLocations.Rows
                    select new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()))).ToList();
        }
    }
}
