using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLFlyPack
{
    
    public class Point
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        public Point(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="point"></param>
        public Point(Point point)
        {
            Lat = point.Lat;
            Lng = point.Lng;
            point = null;

        }
        /// <summary>
        /// distance between two points
        /// </summary>
        /// <param name="point"></param>
        /// <returns>distance</returns>
        public double Distance(Point point)
        {
            return Math.Sqrt(Math.Pow(this.Lat - point.Lat, 2) + Math.Pow(this.Lng - point.Lng, 2));
        }
        /// <summary>
        /// return the time takes to the deliver to pass the distance to point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double Time(Point point)
        {
            const int speed = 10;
            return Distance(point) / speed;
        }
        /// <summary>
        /// return the point index that has the minimum distance from start point
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startIndex"></param>
        /// <returns>point index</returns>
        public int MinimumDistance(List<Point> points,int startIndex)
        {
            int minIndex = startIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                var point = points[index];
                if (this.Distance(point) < Distance(points[minIndex]))
                {
                    minIndex = index;
                }
            }

            return minIndex;
        }
        /// <summary>
        /// return the index of the closest shop from list
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startIndex"></param>
        /// <returns>the index of the closest shop</returns>
        public int MinimumDistanceShops(List<BlShop> points, int startIndex)
        {
            int minIndex = startIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                var point = points[index];
                if (this.Distance(point.Location) < Distance(points[minIndex].Location))
                {
                    minIndex = index;
                }
            }

            return minIndex;
        }
        /// <summary>
        /// return a shop list order by distance
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public List<int> MinimumDistanceShopsList(List<BlShop> points, int startIndex)
        {
            List<BlShop> copy = new List<BlShop>(points);
            List<int> orderShops = new List<int>();
            for (var index = startIndex; index < points.Count; index++)
            {
                var minIndex = MinimumDistanceShops(copy, 0);
                orderShops.Add(minIndex);
                copy.RemoveAt(minIndex);
            }

            return orderShops;
        }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="points"></param>
    //    /// <param name="startIndex"></param>
    //    /// <returns></returns>
    //public int MinimumDistanceCustomers(List<BlCustomersAddress> points, int startIndex)
    //    {
    //        int minIndex = startIndex;
    //        for (var index = startIndex; index < points.Count; index++)
    //        {
    //            var point = points[index];
    //            if (this.Distance(point.Location) < Distance(points[minIndex].Location))
    //            {
    //                minIndex = index;
    //            }
    //        }

    //        return minIndex;
    //    }


        /// <summary>
        /// sort the point list by distance
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<Point> SelectSort(List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                var minIndex = MinimumDistance(points, i);
                var temp = points[i];
                points[i] = points[minIndex];
                points[minIndex] = temp;
            }

            return points;
        }
        /// <summary>
        /// return the arrive time from delivery position to customer home
        /// </summary>
        /// <param name="customersAddress"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        public  double ArriveTime( BlCustomersAddress customersAddress, BlShop shop)
        {
            return this.Time(shop.Location) + shop.Location.Time(customersAddress.Location);
        }
        /// <summary>
        /// return the arrive time from delivery position to the  shop 
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="customersAddress"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        public double ArriveTimeShop( BlCustomersAddress customersAddress, BlShop shop)
        {
            return this.Time(customersAddress.Location) + customersAddress.Location.Time(shop.Location);
        }
        /// <summary>
        /// return the total arrive time from delivery position to customer
        /// </summary>
        /// <param name="customersAddresses"></param>
        /// <param name="shops"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public double ArriveTimeCustomer(List<BlCustomersAddress> customersAddresses,List<BlShop> shops,int maxIndex)
        {
            Point startCopy=new Point(this);
            double arriveTime =0.0;
            for (int i = 0; i < maxIndex+1; i++)
            {
                arriveTime += startCopy.ArriveTime( customersAddresses[i], shops[i]);
                startCopy = customersAddresses[i].Location;
            }

            return arriveTime;
        }
        /// <summary>
        /// return the total arrive time from delivery position to shop
        /// </summary>
        /// <param name="customersAddresses"></param>
        /// <param name="shops"></param>
        /// <param name="maxIndex"></param>
        /// <returns></returns>
        public double ArriveTimeShop( List<BlCustomersAddress> customersAddresses, List<BlShop> shops, int maxIndex)
        {
            Point locationNow = new Point(this);
            double arriveTime = 0.0;
            for (int i = 0; i < maxIndex + 1; i++)
            {
                if (i==0)
                {
                    arriveTime += this.Time(shops[i].Location);
                    locationNow = shops[i].Location;
                }
                else
                {
                    arriveTime += locationNow.ArriveTimeShop( customersAddresses[i], shops[i]);
                    locationNow = shops[i].Location;
                }
                
            }
            return arriveTime;
        }
    }
}
