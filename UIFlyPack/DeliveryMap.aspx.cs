using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
using static BLFlyPack.Point;
using System.Web.Helpers.AntiXsrf;
using System.Web.Helpers.Claims;
using System.Web.Helpers.Resources;

namespace UIFlyPack
{
    public partial class DeliveryMap : System.Web.UI.Page
    {
        //ScriptingJsonSerializationSection
        public string Shops = "";
        public string Customers = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    OrderStartedSucces.InnerHtml = Request.QueryString.Get("text");
                }
                catch 
                {
                    OrderStartedSucces.InnerHtml = "";
                }
              
            }
            Session["user"] = new Deliver("shlakot1");
            BlOrderUser user = (BlOrderUser)Session["user"]; /*(BLUser)Session["user"];*/
            if (!(user is Deliver)) return;
            //get the new orders of the deliver
            List<BlOrder> orders = user.GetOrdersListByTime();
            //set the road lists
            List<BlShop> shops = new List<BlShop>();
            List<BlCustomersAddress> customersAddresses = new List<BlCustomersAddress>();
            //full road lists

            foreach (BlOrder order in orders)
            {
                shops.Add(BlShop.GetShopById(order.ShopId));
                Point customerAddress = order.Location != null ? new Point(order.Location) : new Point(BlUser.UserById(order.CustomerId).Location);
                customersAddresses.Add(new BlCustomersAddress(customerAddress, order.NumOfFloor, user.GetName()));
            }

            if (orders.Count == 0) { errorMSG.Text = "there is no orders"; return; }
            else
            {
                errorMSG.Text = "";
            }
            if (orders.Count > 1)//to change to orders.Count>1!!!!!!!!!!!!!####$$$$$$$$########@@@@@@@*********#########&&&&&&&&&&$$$$$$$$$$^^^^^^^^^^%%%%%%%%%**********
            {
                //List<BlShop> sameShops = BlShop.isHaveSameShop(shops);
                //bool isHaveSameShop = sameShops.Count > 0;
                //if (isHaveSameShop)
                //{
                    
                //}
                bool isAllSameShop = BlShop.isAllSameShop(shops);
                //calculate shorter way to deliver 
                GetBestWayLists(isAllSameShop, shops, customersAddresses, orders,(Deliver) user);
            }
            else
            {
                //update the Page vars
                Shops = Json.Encode(shops);
                Customers = Json.Encode(customersAddresses);
                //update the global vars
                Deliver deliver = (Deliver) user;
                double minutes = deliver.GetDistanceToCustomerHome(shops,customersAddresses);
                GlobalVariable.Distance = minutes;
               
            }

        }

        //public static List<T> CastList<T>(List<object> list)
        //{
        //    List<T> retList=new List<T>();
        //    foreach (var obj in list)
        //    {
        //        if (obj is T Tvalue)
        //        {
        //            retList.Add(Tvalue);
        //        }
        //    }
        //    return retList;
        //}
        public   void GetBestWayLists(bool isAllSameShop,List<BlShop> shops,List<BlCustomersAddress> customersAddresses, List<BlOrder> orders, Deliver deliver)
        {
            //set vars
            List<BlCustomersAddress> copyCustomersAddresses = new List<BlCustomersAddress>(customersAddresses);
            List<BlOrder> copyOrders = new List<BlOrder>(orders);
            List<BlShop> orderShops = new List<BlShop>();
            List<BlCustomersAddress> orderCustomersAddresses = new List<BlCustomersAddress>();
            int minIndex = 0;
            List<int> IndexsOrderByDistance;
            List<double> readyTimes = new List<double>();
            List<double> arriveTimes = new List<double>();
            int lateTimes = 0;
            List<BlShop> copyShops = new List<BlShop>(shops);
            Point locationNow=deliver.Location;
            for (int i = 0; i < shops.Count; i++)
            {
                //minIndex = locationNow.MinimumDistanceShops(copyShops, 0);
                IndexsOrderByDistance = isAllSameShop ? locationNow.MinimumDistanceCustomerList(copyShops, copyCustomersAddresses ,0) : locationNow.MinimumDistanceList(copyShops, copyCustomersAddresses,0);
              //set a list of the index of the shop order by distance ( the first is the closest)
                //to add shop and customer
                //OrderShops[i] = CopyShops[minIndex];
                //OrderCustomersAddresses[i] = CopyCustomersAddresses[minIndex];
                ////calculate Times
                //DateTime ArriveTimeCustomer = DateTime.Now.AddMinutes(Point.ArriveTimeCustomer(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                //DateTime ArriveTimeShop = DateTime.Now.AddMinutes(Point.ArriveTimeShop(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                //DateTime ArriveTime = CopyOrders[minIndex].AriveTime;
                //DateTime ReadyTime = CopyOrders[minIndex].ReadyTime;
                //if (IsLate(ArriveTimeCustomer, ArriveTime, ReadyTime, ArriveTimeShop, ReadyTimes, ArriveTimes))
                //

                // for orderShopIndex to select the shortest distance shop that the deliver deliver the pack on time

                foreach (var index in IndexsOrderByDistance)
                {
                    //to add shop and customer
                    int realIndex = index - lateTimes;
                    orderShops.Add(copyShops[realIndex]);
                    orderCustomersAddresses.Add(copyCustomersAddresses[realIndex]);
                    //orderShops[i] = copyShops[index];
                    //orderCustomersAddresses[i] = copyCustomersAddresses[index];
                    //calculate Times
                    DateTime arriveTimeCustomer = DateTime.Now.AddMinutes(locationNow.ArriveTimeCustomer(copyCustomersAddresses, copyShops, realIndex));
                    DateTime arriveTimeShop = DateTime.Now.AddMinutes(locationNow.ArriveTimeShop(copyCustomersAddresses, copyShops, realIndex));
                    DateTime arriveTime = copyOrders[realIndex].ArriveTime;
                    DateTime readyTime = copyOrders[realIndex].ReadyTime;
                    if (!IsLate(arriveTimeCustomer, arriveTime, readyTime, arriveTimeShop, readyTimes, arriveTimes))
                    {
                        break;//out the loop (the shop and the match customer address already added
                    }
                    else
                    {
                        //remove because index not good
                        copyShops.RemoveAt(realIndex);
                        copyOrders.RemoveAt(realIndex);
                        copyCustomersAddresses.RemoveAt(realIndex);
                        //update late times
                        lateTimes++;
                    }
                }
                //update locationNow
                locationNow = new Point(orderCustomersAddresses[i].Location);
            }
            if (lateTimes == shops.Count)
            {
                //if all late the best way is the shortest way
                List<BlShop> bestWayShops = new List<BlShop>();
                List<BlCustomersAddress> bestWayCustomers = new List<BlCustomersAddress>();
                List<BlShop> cShops = new List<BlShop>(shops);
                List<BlCustomersAddress> cCustomersAddresses = new List<BlCustomersAddress>(customersAddresses);
                for (int i = 0; i < shops.Count; i++)
                {
                    IndexsOrderByDistance = locationNow.MinimumDistanceList(cShops, cCustomersAddresses, 0);
                    int bestIndex = GetBestShopIndex(readyTimes, arriveTimes, IndexsOrderByDistance);//get the best index by all the parameters
                    bestWayShops.Add(cShops[bestIndex]);
                    bestWayCustomers.Add(cCustomersAddresses[bestIndex]);
                    cShops.RemoveAt(bestIndex);
                    cCustomersAddresses.RemoveAt(bestIndex);
                }
                //update the Page vars
                Shops = Json.Encode(bestWayShops);
                Customers = Json.Encode(bestWayCustomers);
                //update the global vars
                double minutes = deliver.GetDistanceToCustomerHome(shops, customersAddresses);
                GlobalVariable.Distance = minutes;

            }
            else
            {
                //update the Page vars
                Shops = Json.Encode(orderShops);
                Customers = Json.Encode(orderCustomersAddresses);
                //update the global vars
                double minutes = deliver.GetDistanceToCustomerHome(shops, customersAddresses);
                GlobalVariable.Distance = minutes;

            }
        }
        //public static void Calculate(List<BlCustomersAddress> CustomersAddresses, List<BlShop> shops)
        //{

        //}
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
        protected void updateMap_OnClick(object sender, EventArgs e)
        {
            IsUpdated.Value = "0";//update the hidden field value
        }
    }
}