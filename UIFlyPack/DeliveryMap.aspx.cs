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
        public List<BLShop> Shops = null;
        public List<BLCustomersAddress> CustomersAddresses = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["user"] = new BLUser("gigi1234");
            BLUser user = (BLUser)Session["user"]; /*(BLUser)Session["user"];*/
            if (user != null)
            {
                List<BLOrder> orders = BLOrder.GetOrdersListByTime(user.UserID);
                List<BLShop> shops = new List<BLShop>();
                List<BLCustomersAddress> customersAddresses = new List<BLCustomersAddress>();
                foreach (BLOrder order in orders)
                {
                    shops.Add(BLShop.GetShopById(order.ShopID));
                    Point CustomerAddress = null;
                    CustomerAddress = order.location != null ? new Point(order.location) : new Point(BLUser.UserByID(order.CustomerID).location);
                    customersAddresses.Add(new BLCustomersAddress(CustomerAddress, order.NumOfFloor, BLUser.GetName(order.CustomerID)));
                }
                //calculate shorter way to deliver
                List<BLShop> CopyShops = new List<BLShop>(shops);
                List<BLCustomersAddress> CopyCustomersAddresses = new List<BLCustomersAddress>(customersAddresses);
                List<BLOrder> CopyOrders = new List<BLOrder>(orders);
                List<BLShop> OrderShops = new List<BLShop>();
                List<BLCustomersAddress> OrderCustomersAddresses = new List<BLCustomersAddress>();
                int minIndex = 0;
                Point LocationNow = user.location;
                List<int> OrderShopIndex;
                List<double> ReadyTimes = new List<double>();
                List<double> ArriveTimes = new List<double>();
                int lateTimes = 0;
                for (int i = 0; i < shops.Count; i++)
                {
                    minIndex = LocationNow.MinimumDistanceShops(CopyShops, 0);
                    OrderShopIndex = LocationNow.MinimumDistanceShopsList(CopyShops, 0);

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
                    foreach (var index in OrderShopIndex)
                    {
                        //to add shop and customer
                        OrderShops[i] = CopyShops[index];
                        OrderCustomersAddresses[i] = CopyCustomersAddresses[index];
                        //calculate Times
                        DateTime ArriveTimeCustomer = DateTime.Now.AddMinutes(Point.ArriveTimeCustomer(LocationNow, CopyCustomersAddresses, CopyShops, index));
                        DateTime ArriveTimeShop = DateTime.Now.AddMinutes(Point.ArriveTimeShop(LocationNow, CopyCustomersAddresses, CopyShops, index));
                        DateTime ArriveTime = CopyOrders[index].AriveTime;
                        DateTime ReadyTime = CopyOrders[index].ReadyTime;
                        if (!IsLate(ArriveTimeCustomer, ArriveTime, ReadyTime, ArriveTimeShop, ReadyTimes, ArriveTimes))
                        {
                            break;
                        }
                        else
                        {
                            //remove because index not good
                            CopyShops.RemoveAt(index);
                            CopyOrders.RemoveAt(index);
                            CopyCustomersAddresses.RemoveAt(index);
                            lateTimes++;
                        }
                    }
                    //}GetBestShopIndex

                    //update location
                    LocationNow = new Point(OrderCustomersAddresses[i].location);
                }
                if (lateTimes == shops.Count)
                {
                    List<BLShop> BestWayShops = new List<BLShop>();
                    List<BLCustomersAddress> BestWayCustomers = new List<BLCustomersAddress>();
                    for (int i = 0; i < shops.Count; i++)
                    {
                        List<BLShop> CShops = new List<BLShop>(shops);
                        OrderShopIndex = LocationNow.MinimumDistanceShopsList(CShops, 0);
                        int BestIndex = GetBestShopIndex(ReadyTimes, ArriveTimes, OrderShopIndex);
                        BestWayShops[i] = CopyShops[BestIndex];
                        BestWayCustomers[i] = CopyCustomersAddresses[BestIndex];
                    }
                    Shops = BestWayShops;
                    CustomersAddresses = BestWayCustomers;

                }
                else
                {
                    Shops = OrderShops;
                    CustomersAddresses = OrderCustomersAddresses;
                }
            }


        }

        public static bool IsLate(DateTime ArriveTimeCustomer, DateTime ArriveTime, DateTime ReadyTime, DateTime OriginalReadyTime, List<double> ReadyTimes, List<double> ArriveTimes)
        {
            double temp = (ArriveTimeCustomer - ArriveTime).TotalMinutes;
            ArriveTimes.Add(temp);
            double temp2 = (ReadyTime - OriginalReadyTime).TotalMinutes;

            ReadyTimes.Add(temp2);
            return temp > 0 && temp2 < 0;
        }

        public static int GetBestShopIndex(List<double> ReadyTimes, List<double> ArriveTimes, List<int> OrderByDistance)
        {
            int Lenght = OrderByDistance.Count;
            List<int> OrderReadyTimes = new List<int>(TimesOrderByBestTime(ReadyTimes, false));
            List<int> OrderArriveTimes = new List<int>(TimesOrderByBestTime(ArriveTimes, true));

            int[] ShopScore = new int[Lenght];
            for (int j = 0; j < Lenght; j++)
            {
                ShopScore[OrderReadyTimes[j]] += Lenght - j;
                ShopScore[OrderArriveTimes[j]] += ((Lenght + 2) - j) * 10;
                ShopScore[OrderByDistance[j]] += (Lenght - j) * 10;
            }
            List<int> CopyArr = new List<int>(ShopScore);

            return BestTimeIndex(CopyArr, false);
        }


        public static List<int> TimesOrderByBestTime(List<double> Times, bool IsArriveTime)//to do list of ArriveTimes ordered list[howMatchGood]=indexOfArriveTimes;
        {
            List<double> Copy = new List<double>(Times);
            List<int> OrderShops = new List<int>();
            int MinIndex = 0;
            for (var index = 0; index < Times.Count; index++)
            {
                MinIndex = BestTimeIndex(Copy, IsArriveTime);

                OrderShops.Add(MinIndex);
                Copy.RemoveAt(MinIndex);
            }
            return OrderShops;
        }
        public static int BestTimeIndex(List<int> ArriveTimes, bool IsArriveTime)
        {
            int BestIndex = 0;
            for (int i = 1; i < ArriveTimes.Count; i++)
            {
                if (IsArriveTime)
                {
                    if (ArriveTimes[BestIndex] > ArriveTimes[i])
                    {
                        BestIndex = i;
                    }
                }
                else
                {
                    if (ArriveTimes[BestIndex] < ArriveTimes[i])
                    {
                        BestIndex = i;
                    }
                }

            }

            return BestIndex;
        }
        public static int BestTimeIndex(List<double> ArriveTimes, bool IsArriveTime)
        {
            int BestIndex = 0;
            for (int i = 1; i < ArriveTimes.Count; i++)
            {
                if (IsArriveTime)
                {
                    if (ArriveTimes[BestIndex] > ArriveTimes[i])
                    {
                        BestIndex = i;
                    }
                }
                else
                {
                    if (ArriveTimes[BestIndex] < ArriveTimes[i])
                    {
                        BestIndex = i;
                    }
                }

            }

            return BestIndex;
        }

        //public  static  MatchIndex()
        //public static void AddPoint(Point startPoint, List<BLCustomersAddress> CopyCustomersAddress,)
        //{

        //}
    }
}