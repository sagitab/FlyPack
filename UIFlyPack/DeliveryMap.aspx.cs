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
            Session["user"] = new BLUser("gigi1234");
            BLUser user = (BLUser)Session["user"]; /*(BLUser)Session["user"];*/
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
            for (int i = 0; i < shops.Count; i++)
            {
                minIndex = LocationNow.MinimumDistanceShops(CopyShops, 0);
                OrderShopIndex = LocationNow.MinimumDistanceShopsList(CopyShops, 0);
                int lateTimes = 0;
                //to add shop and customer
                OrderShops[i] = CopyShops[minIndex];
                OrderCustomersAddresses[i] = CopyCustomersAddresses[minIndex];
                //calculate Times
                DateTime ArriveTimeCustomer = DateTime.Now.AddMinutes(Point.ArriveTimeCustomer(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                DateTime ArriveTimeShop = DateTime.Now.AddMinutes(Point.ArriveTimeShop(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                DateTime ArriveTime = CopyOrders[minIndex].AriveTime;
                DateTime ReadyTime = CopyOrders[minIndex].ReadyTime;
                if (IsLate(ArriveTimeCustomer, ArriveTime, ReadyTime, ArriveTimeShop))
                {
                    foreach (var index in OrderShopIndex)
                    {
                        //calculate Times
                        ArriveTimeCustomer = DateTime.Now.AddMinutes(Point.ArriveTimeCustomer(LocationNow, CopyCustomersAddresses, CopyShops, index));
                        ArriveTimeShop = DateTime.Now.AddMinutes(Point.ArriveTimeShop(LocationNow, CopyCustomersAddresses, CopyShops, minIndex));
                        ArriveTime = CopyOrders[index].AriveTime;
                        ReadyTime = CopyOrders[index].ReadyTime;
                        if (!IsLate(ArriveTimeCustomer, ArriveTime, ReadyTime, ArriveTimeShop))
                        {
                            break;
                        }
                        else
                        {
                            //remove because index not good
                            CopyShops.RemoveAt(minIndex);
                            CopyOrders.RemoveAt(minIndex);
                            CopyCustomersAddresses.RemoveAt(minIndex);

                            lateTimes++;
                        }
                    }
                }

                if (lateTimes == OrderShopIndex.Count)
                {

                }
                //update location
                LocationNow = new Point(OrderCustomersAddresses[i].location);
            }
            Shops = OrderShops;
            CustomersAddresses = OrderCustomersAddresses;
        }

        public static bool IsLate(DateTime ArriveTimeCustomer, DateTime ArriveTime, DateTime ReadyTime, DateTime OriginalReadyTime)
        {
            int temp = DateTime.Compare(ArriveTimeCustomer, ArriveTime);
            int temp2 = DateTime.Compare(ReadyTime, OriginalReadyTime);
            return temp > 0 && temp2 < 0;
        }
        //public  static  MatchIndex()
        //public static void AddPoint(Point startPoint, List<BLCustomersAddress> CopyCustomersAddress,)
        //{

        //}
    }
}