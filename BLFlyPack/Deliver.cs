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
        /// get point list of all deliveries location
        /// </summary>
        /// <returns> list of all deliveries location</returns>
        public static List<Point> GetDeliveriesLocations()
        {
            DataTable deliverersLocations = DalUser.GetDeliverersLocations();

            return (from DataRow row in deliverersLocations.Rows
                    select new Point(double.Parse(row["Lat"].ToString()), double.Parse(row["Lng"].ToString()))).ToList();
        }

        public static  Deliver GetDeliver(DataRow row)
        {
            string ID = row["ID"].ToString();
            string FirstName = row["FirstName"].ToString();
           string LastName = row["LastName"].ToString();
           string Email = row["Email"].ToString();
           return new Deliver(FirstName,LastName,Email, ID);
        }

        public Deliver(string FirstName, string LastName, string Email, string ID) : base("111111111")
        {
            UserId = ID;
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;
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
        public string GetMatchDeliveryIdByPoints(List<Point> points)
        {
            int index = 0;
            while (index + 1 <= points.Count && GetNumOfDeliveryOrders() >= 6
            ) //continue loop if delivery orders is full
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
        public string GetMatchesDeliveryId(Point shopPoint)
        {
            List<Point> points = GetDeliveriesLocations();
            List<Point> sortedDeliveryPoints = shopPoint.SelectSort(points);

            //int index = shopPoint.MinimumDistance(points,0);
            //Point deliveryPoint = points[index];
            return GetMatchDeliveryIdByPoints(sortedDeliveryPoints);
        }

        public int GetNumOfDeliveryOrders()
        {
            return DalOrder.NumOfOrders($"WHERE Orders.DeliverID ='{UserId}' AND Orders.OrderStutus =4");
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

        public static int NumOfAvailableDeliveries()
        {
            int Cnt = 0;
            DataTable Delivers = DalUser.DeliveriesList();
            List<Deliver> delivers= (from DataRow row in Delivers.Rows select GetDeliver(row)).ToList();
            foreach (Deliver deliver in delivers)
            {
                if (deliver.GetNumOfDeliveryOrders()<6)
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
    }
}
