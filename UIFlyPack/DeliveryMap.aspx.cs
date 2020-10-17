using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
using static BLFlyPack.Point;

namespace UIFlyPack
{

    public partial class DeliveryMap : System.Web.UI.Page
    {
        //ScriptingJsonSerializationSection
        public List<BlShop> Shops = null;
        public List<BlCustomersAddress> CustomersAddresses = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["user"] = new BLUser("gigi1234");
            BlOrderUser user = (BlOrderUser)Session["user"]; /*(BLUser)Session["user"];*/
            if (user != null)
            {
                //get the new orders of the deliver
                List<BlOrder> orders = user.GetOrdersListByTime();
                //set the road lists
                List<BlShop> shops = new List<BlShop>();
                List<BlCustomersAddress> customersAddresses = new List<BlCustomersAddress>();
                //full road lists
                foreach (BlOrder order in orders)
                {
                    shops.Add(BlShop.GetShopById(order.ShopId));
                    Point customerAddress = null;
                    customerAddress = order.Location != null ? new Point(order.Location) : new Point(BlUser.UserById(order.CustomerId).Location);
                    customersAddresses.Add(new BlCustomersAddress(customerAddress, order.NumOfFloor, user.GetName()));
                }

                if (orders.Count>1)
                {
                    //calculate shorter way to deliver 
                    //set vars
                    List<BlShop> copyShops = new List<BlShop>(shops);
                    List<BlCustomersAddress> copyCustomersAddresses = new List<BlCustomersAddress>(customersAddresses);
                    List<BlOrder> copyOrders = new List<BlOrder>(orders);
                    List<BlShop> orderShops = new List<BlShop>();
                    List<BlCustomersAddress> orderCustomersAddresses = new List<BlCustomersAddress>();
                    int minIndex = 0;
                    Point locationNow = user.Location;
                    List<int> orderShopIndex;
                    List<double> readyTimes = new List<double>();
                    List<double> arriveTimes = new List<double>();
                    int lateTimes = 0;
                    //loop the shop and find the best shop 
                    for (int i = 0; i < shops.Count; i++)
                    {

                        minIndex = locationNow.MinimumDistanceShops(copyShops, 0);
                        orderShopIndex = locationNow.MinimumDistanceShopsList(copyShops, 0);//set a list of the index of the shop order by distance ( the first is the closest)

                        //to add shop and customer
                        //OrderShops[i] = CopyShops[minIndex];
                        //OrderCustomersAddresses[i] = CopyCustomersAddresses[minIndex];
                        ////calculate Times
                        //DateTime ArriveTimeCustomer = DateTime.Now.AddMinutes(Point.ArriveTimeCustomer(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                        //DateTime ArriveTimeShop = DateTime.Now.AddMinutes(Point.ArriveTimeShop(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                        //DateTime ArriveTime = CopyOrders[minIndex].AriveTime;
                        //DateTime ReadyTime = CopyOrders[minIndex].ReadyTime;
                        //if (IsLate(ArriveTimeCustomer, ArriveTime, ReadyTime, ArriveTimeShop, ReadyTimes, ArriveTimes))
                        //{
                        // for orderShopIndex to select the shortest distance shop that the deliver deliver the pack on time
                        foreach (var index in orderShopIndex)
                        {
                            //to add shop and customer
                            orderShops[i] = copyShops[index];
                            orderCustomersAddresses[i] = copyCustomersAddresses[index];
                            //calculate Times
                            DateTime arriveTimeCustomer = DateTime.Now.AddMinutes(locationNow.ArriveTimeCustomer(copyCustomersAddresses, copyShops, index));
                            DateTime arriveTimeShop = DateTime.Now.AddMinutes(locationNow.ArriveTimeShop(copyCustomersAddresses, copyShops, index));
                            DateTime arriveTime = copyOrders[index].ArriveTime;
                            DateTime readyTime = copyOrders[index].ReadyTime;
                            if (!IsLate(arriveTimeCustomer, arriveTime, readyTime, arriveTimeShop, readyTimes, arriveTimes))
                            {
                                break;//out the loop (the shop and the match customer address already added
                            }
                            else
                            {
                                //remove because index not good
                                copyShops.RemoveAt(index);
                                copyOrders.RemoveAt(index);
                                copyCustomersAddresses.RemoveAt(index);
                                //update late times
                                lateTimes++;
                            }
                        }


                        //update locationNow
                        locationNow = new Point(orderCustomersAddresses[i].Location);
                    }
                    if (lateTimes == shops.Count)
                    {
                        //if the best way is the shortest way
                        List<BlShop> bestWayShops = new List<BlShop>();
                        List<BlCustomersAddress> bestWayCustomers = new List<BlCustomersAddress>();
                        for (int i = 0; i < shops.Count; i++)
                        {
                            List<BlShop> cShops = new List<BlShop>(shops);
                            orderShopIndex = locationNow.MinimumDistanceShopsList(cShops, 0);
                            int bestIndex = GetBestShopIndex(readyTimes, arriveTimes, orderShopIndex);
                            bestWayShops[i] = copyShops[bestIndex];
                            bestWayCustomers[i] = copyCustomersAddresses[bestIndex];
                        }
                        Shops = bestWayShops;
                        CustomersAddresses = bestWayCustomers;

                    }
                    else
                    {
                        //update the global vars
                        Shops = orderShops;
                        CustomersAddresses = orderCustomersAddresses;
                    }
                }
                else
                {
                    //update the global vars
                    Shops = shops;
                    CustomersAddresses = customersAddresses;
                }
              
            }


        }

        public static bool IsLate(DateTime arriveTimeCustomer, DateTime arriveTime, DateTime readyTime, DateTime originalReadyTime, List<double> readyTimes, List<double> arriveTimes)
        {
            double temp = (arriveTimeCustomer - arriveTime).TotalMinutes;
            arriveTimes.Add(temp);
            double temp2 = (readyTime - originalReadyTime).TotalMinutes;

            readyTimes.Add(temp2);
            return temp > 0 && temp2 < 0;
        }

        public static int GetBestShopIndex(List<double> readyTimes, List<double> arriveTimes, List<int> orderByDistance)
        {
            int lenght = orderByDistance.Count;
            List<int> orderReadyTimes = new List<int>(TimesOrderByBestTime(readyTimes, false));
            List<int> orderArriveTimes = new List<int>(TimesOrderByBestTime(arriveTimes, true));

            int[] shopScore = new int[lenght];
            for (int j = 0; j < lenght; j++)
            {
                shopScore[orderReadyTimes[j]] += lenght - j;
                shopScore[orderArriveTimes[j]] += ((lenght + 2) - j) * 10;
                shopScore[orderByDistance[j]] += (lenght - j) * 10;
            }
            List<int> copyArr = new List<int>(shopScore);

            return BestTimeIndex(copyArr, false);
        }


        public static List<int> TimesOrderByBestTime(List<double> times, bool isArriveTime)//to do list of ArriveTimes ordered list[howMatchGood]=indexOfArriveTimes;
        {
            List<double> copy = new List<double>(times);
            List<int> orderShops = new List<int>();
            int minIndex = 0;
            for (var index = 0; index < times.Count; index++)
            {
                minIndex = BestTimeIndex(copy, isArriveTime);

                orderShops.Add(minIndex);
                copy.RemoveAt(minIndex);
            }
            return orderShops;
        }
        public static int BestTimeIndex(List<int> arriveTimes, bool isArriveTime)
        {
            int bestIndex = 0;
            for (int i = 1; i < arriveTimes.Count; i++)
            {
                if (isArriveTime)
                {
                    if (arriveTimes[bestIndex] > arriveTimes[i])
                    {
                        bestIndex = i;
                    }
                }
                else
                {
                    if (arriveTimes[bestIndex] < arriveTimes[i])
                    {
                        bestIndex = i;
                    }
                }

            }

            return bestIndex;
        }
        public static int BestTimeIndex(List<double> arriveTimes, bool isArriveTime)
        {
            int bestIndex = 0;
            for (int i = 1; i < arriveTimes.Count; i++)
            {
                if (isArriveTime)
                {
                    if (arriveTimes[bestIndex] > arriveTimes[i])
                    {
                        bestIndex = i;
                    }
                }
                else
                {
                    if (arriveTimes[bestIndex] < arriveTimes[i])
                    {
                        bestIndex = i;
                    }
                }

            }

            return bestIndex;
        }

        //public  static  MatchIndex()
        //public static void AddPoint(Point startPoint, List<BLCustomersAddress> CopyCustomersAddress,)
        //{

        //}
    }
}