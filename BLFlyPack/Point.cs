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
            int MinIndex = 0;
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
        public static double ArrivelTime(Point StartPoint, BLCustomersAddress customersAddress, BLShop shop)
        {
            return StartPoint.Time(shop.Possision) + shop.Possision.Time(customersAddress.Possision);
        }
        public static double ArrivelTime(Point StartPoint,List<BLCustomersAddress> customersAddresses,List<BLShop> shops)
        {
            Point startCopy=new Point(StartPoint);
            double ArriveTime =0.0;
            for (int i = 0; i < shops.Count; i++)
            {
                ArriveTime += ArrivelTime(StartPoint, customersAddresses[i], shops[i]);
                startCopy = customersAddresses[i].Possision;
            }

            return ArriveTime;
        }
    }
}
