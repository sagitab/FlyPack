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
            const int Speed = 10;
            return Distance(point) / Speed;
        }

        public int MinimumDistance(List<Point> points,int startIndex)
        {
            int MinIndex = startIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                var point = points[index];
                if (this.Distance(point) < Distance(points[MinIndex]))
                {
                    MinIndex = index;
                }
            }

            return MinIndex;
        }
        public int MinimumDistanceShops(List<BLShop> points, int startIndex)
        {
            int MinIndex = startIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                var point = points[index];
                if (this.Distance(point.location) < Distance(points[MinIndex].location))
                {
                    MinIndex = index;
                }
            }

            return MinIndex;
        }
        
        public List<int> MinimumDistanceShopsList(List<BLShop> points, int startIndex)
        {
            List<BLShop> Copy = new List<BLShop>(points);
            List<int> OrderShops = new List<int>();
            int MinIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                MinIndex = MinimumDistanceShops(Copy, 0);
                OrderShops.Add(MinIndex);
                Copy.RemoveAt(MinIndex);
            }

            return OrderShops;
        }
    public int MinimumDistanceCustomers(List<BLCustomersAddress> points, int startIndex)
        {
            int MinIndex = startIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                var point = points[index];
                if (this.Distance(point.location) < Distance(points[MinIndex].location))
                {
                    MinIndex = index;
                }
            }

            return MinIndex;
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
        public static double ArriveTime(Point StartPoint, BLCustomersAddress customersAddress, BLShop shop)
        {
            return StartPoint.Time(shop.location) + shop.location.Time(customersAddress.location);
        }
        public static double ArriveTimeShop(Point StartPoint, BLCustomersAddress customersAddress, BLShop shop)
        {
            return StartPoint.Time(customersAddress.location) + customersAddress.location.Time(shop.location);
        }
        public static double ArriveTimeCustomer(Point StartPoint,List<BLCustomersAddress> customersAddresses,List<BLShop> shops,int MaxIndex)
        {
            Point startCopy=new Point(StartPoint);
            double ArriveTime =0.0;
            for (int i = 0; i < MaxIndex+1; i++)
            {
                ArriveTime += Point.ArriveTime(startCopy, customersAddresses[i], shops[i]);
                startCopy = customersAddresses[i].location;
            }

            return ArriveTime;
        }
        public static double ArriveTimeShop(Point StartPoint, List<BLCustomersAddress> customersAddresses, List<BLShop> shops, int MaxIndex)
        {
            Point locationNow = new Point(StartPoint);
            double ArriveTime = 0.0;
            for (int i = 0; i < MaxIndex + 1; i++)
            {
                if (i==0)
                {
                    ArriveTime += StartPoint.Time(shops[i].location);
                    locationNow = shops[i].location;
                }
                else
                {
                    ArriveTime += Point.ArriveTimeShop(locationNow, customersAddresses[i], shops[i]);
                    locationNow = shops[i].location;
                }
                
            }
            return ArriveTime;
        }
    }
}
