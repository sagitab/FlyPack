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

        public Point(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
        public Point(Point point)
        {
            Lat = point.Lat;
            Lng = point.Lng;
            point = null;

        }

        public double Distance(Point point)
        {
            return Math.Sqrt(Math.Pow(this.Lat - point.Lat, 2) + Math.Pow(this.Lng - point.Lng, 2));
        }

        public double Time(Point point)
        {
            const int speed = 10;
            return Distance(point) / speed;
        }

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
        
        public List<int> MinimumDistanceShopsList(List<BlShop> points, int startIndex)
        {
            List<BlShop> copy = new List<BlShop>(points);
            List<int> orderShops = new List<int>();
            int minIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                minIndex = MinimumDistanceShops(copy, 0);
                orderShops.Add(minIndex);
                copy.RemoveAt(minIndex);
            }

            return orderShops;
        }
    public int MinimumDistanceCustomers(List<BlCustomersAddress> points, int startIndex)
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
        public static double ArriveTime(Point startPoint, BlCustomersAddress customersAddress, BlShop shop)
        {
            return startPoint.Time(shop.Location) + shop.Location.Time(customersAddress.Location);
        }
        public static double ArriveTimeShop(Point startPoint, BlCustomersAddress customersAddress, BlShop shop)
        {
            return startPoint.Time(customersAddress.Location) + customersAddress.Location.Time(shop.Location);
        }
        public static double ArriveTimeCustomer(Point startPoint,List<BlCustomersAddress> customersAddresses,List<BlShop> shops,int maxIndex)
        {
            Point startCopy=new Point(startPoint);
            double arriveTime =0.0;
            for (int i = 0; i < maxIndex+1; i++)
            {
                arriveTime += Point.ArriveTime(startCopy, customersAddresses[i], shops[i]);
                startCopy = customersAddresses[i].Location;
            }

            return arriveTime;
        }
        public static double ArriveTimeShop(Point startPoint, List<BlCustomersAddress> customersAddresses, List<BlShop> shops, int maxIndex)
        {
            Point locationNow = new Point(startPoint);
            double arriveTime = 0.0;
            for (int i = 0; i < maxIndex + 1; i++)
            {
                if (i==0)
                {
                    arriveTime += startPoint.Time(shops[i].Location);
                    locationNow = shops[i].Location;
                }
                else
                {
                    arriveTime += Point.ArriveTimeShop(locationNow, customersAddresses[i], shops[i]);
                    locationNow = shops[i].Location;
                }
                
            }
            return arriveTime;
        }
    }
}
