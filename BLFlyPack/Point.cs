﻿using System;
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
        public Point()
        {

        }
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
            double distance = 0;
            double ToRudians = Math.PI / 180;
            double lat1 = Lat * ToRudians;
            double lat2 = point.Lat * ToRudians;
            double long1 = Lng;
            double long2 = point.Lng;
            double dLat = (lat1 - lat2) * ToRudians;
            double dLong = (long2 - long1) * ToRudians;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                       + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double radius = 6371e3;
            distance = radius * c;
            return distance;


            //return Math.Sqrt(Math.Pow(this.Lat - point.Lat, 2) + Math.Pow(this.Lng - point.Lng, 2));
        }
        /// <summary>
        /// return the time takes to the deliver to pass the distance to point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double Time(Point point)
        {

            return Distance(point) / GlobalVariable.Speed;
        }
        /// <summary>
        /// return the point index that has the minimum distance from start point
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startIndex"></param>
        /// <returns>point index</returns>
        public int MinimumDistance(List<Point> points, int startIndex)
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
        /// <param name="Shops"></param>
        /// <param name="startIndex"></param>
        /// <returns>the index of the closest shop</returns>
        public int MinimumDistance(List<BlShop> Shops, List<BlCustomersAddress> customersAddresses, int startIndex)
        {
            int minIndex = startIndex;
            for (var index = startIndex; index < Shops.Count && index < customersAddresses.Count; index++)
            {
                var Shop = Shops[index].Location;
                var customerAddress = customersAddresses[index].Location;


                var minIndexShop = Shops[minIndex].Location;
                var minIndexCustomerAddress = Shops[minIndex].Location;



                double distance = this.Distance(Shop) + Shop.Distance(customerAddress), minDistance = Distance(minIndexShop) + minIndexShop.Distance(minIndexCustomerAddress);

                if (distance < minDistance)
                {
                    minIndex = index;
                }
            }

            return minIndex;
        }
        /// <summary>
        /// return a shop list order by distance
        /// </summary>
        /// <param name="shops"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public List<int> MinimumDistanceList(List<BlShop> shops, List<BlCustomersAddress> customersAddresses, int startIndex)
        {
            //List<BlShop> shopsCopy= new List<BlShop>(shops); List<BlCustomersAddress> customersAddressesCopy=new List<BlCustomersAddress>(customersAddresses);
            List<int> orderShops = new List<int>();
            for (var index = startIndex; index < shops.Count && index < customersAddresses.Count; index++)
            {
                var minIndex = MinimumDistance(shops, customersAddresses, index);
                orderShops.Add(minIndex);

            }
            return orderShops;
        }
        public List<int> MinimumDistanceCustomerList(List<BlShop> shops, List<BlCustomersAddress> customersAddresses, int startIndex)
        {
            //List<BlCustomersAddress> customersAddressesCopy = new List<BlCustomersAddress>(customersAddresses);
            List<int> orderRetList = new List<int>();
            for (var i = startIndex; i < customersAddresses.Count; i++)
            {
                var minIndex = shops[i].Location.MinimumDistanceCustomer(customersAddresses, i);
                orderRetList.Add(minIndex);
            }
            return orderRetList;
        }
        public int MinimumDistanceCustomer(List<BlCustomersAddress> points, int startIndex)
        {
            int minIndex = startIndex;
            for (var index = startIndex; index < points.Count; index++)
            {
                var point = points[index].Location;
                if (this.Distance(point) < Distance(points[minIndex].Location))
                {
                    minIndex = index;
                }
            }

            return minIndex;
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
        public double ArriveTime(BlCustomersAddress customersAddress, BlShop shop)
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
        public double ArriveTimeShop(BlCustomersAddress customersAddress, BlShop shop)
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
        public double ArriveTimeCustomer(List<BlCustomersAddress> customersAddresses, List<BlShop> shops, int maxIndex)
        {
            Point startCopy = new Point(this);
            double arriveTime = 0.0;
            for (int i = 0; i < maxIndex + 1; i++)
            {
                arriveTime += startCopy.ArriveTime(customersAddresses[i], shops[i]);
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
        public double ArriveTimeShop(List<BlCustomersAddress> customersAddresses, List<BlShop> shops, int maxIndex)
        {
            Point locationNow = new Point(this);
            double arriveTime = 0.0;
            for (int i = 0; i < maxIndex + 1; i++)
            {
                if (i == 0)
                {
                    arriveTime += this.Time(shops[i].Location);
                    locationNow = shops[i].Location;
                }
                else
                {
                    arriveTime += locationNow.ArriveTimeShop(customersAddresses[i], shops[i]);
                    locationNow = shops[i].Location;
                }

            }
            return arriveTime;
        }
        //public List<int> MinimumDistanceCustomersList(List<BlCustomersAddress> points, int startIndex)
        //{
        //    List<BlCustomersAddress> copy = new List<BlCustomersAddress>(points);
        //    List<int> orderShops = new List<int>();
        //    for (var index = startIndex; index < points.Count; index++)
        //    {
        //        var minIndex = MinimumDistanceShops(copy, 0);
        //        orderShops.Add(minIndex);
        //        copy.RemoveAt(minIndex);
        //    }

        //    return orderShops;
        //}
        public bool IsSame(Point point)
        {
            return this.Lat == point.Lat && this.Lng == point.Lng;
        }
    }
}
