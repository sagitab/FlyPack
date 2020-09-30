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
            int minIndex=0;
            Point LocationNow = new Point(0, 0);
            for (int i = 0; i < shops.Count; i++)
            {
                if (i == 0)
                {
                    Point deliveryLocation = user.location;
                    minIndex = deliveryLocation.MinimumDistanceShops(CopyShops, 0);
                    List<int> OrderShopIndex = deliveryLocation.MinimumDistanceShopsList(CopyShops,0);
                    if (IsLate(ArriveTime(deliveryLocation, CopyCustomersAddresses, CopyShops, minIndex), CopyOrders[minIndex].AriveTime))
                    {
                        foreach (var index in OrderShopIndex)
                        {
                            if (!IsLate(ArriveTime(deliveryLocation, CopyCustomersAddresses, CopyShops, index), CopyOrders[index].AriveTime))
                            {
                                minIndex = index;
                                break;
                            }
                        }
                    }

                    OrderShops[i] = CopyShops[minIndex];
                    CopyShops.RemoveAt(minIndex);
                    CopyOrders.RemoveAt(minIndex);

                    //minIndex = Ordershops[minIndex].location.MinimumDistanceCustomers(CopyCustomersAddresses, 0);
                    OrderCustomersAddresses[i] = CopyCustomersAddresses[minIndex];
                    CopyCustomersAddresses.RemoveAt(minIndex);
                   

                    LocationNow = new Point(OrderCustomersAddresses[i].location);
                }
                else
                {
                    minIndex = LocationNow.MinimumDistanceShops(CopyShops, 0);
                    List<int> OrderShopIndex = LocationNow.MinimumDistanceShopsList(CopyShops, 0);
                    if (IsLate(ArriveTime(LocationNow, CopyCustomersAddresses, CopyShops, minIndex), CopyOrders[minIndex].AriveTime))
                    {
                        foreach (var index in OrderShopIndex)
                        {
                            if (!IsLate(ArriveTime(LocationNow, CopyCustomersAddresses, CopyShops, index), CopyOrders[index].AriveTime))
                            {
                                minIndex = index;
                                break;
                            }
                        }
                    }
                    OrderShops[i] = CopyShops[minIndex];
                    CopyShops.RemoveAt(minIndex);
                    CopyOrders.RemoveAt(minIndex);

                    //minIndex = Ordershops[minIndex].location.MinimumDistanceCustomers(CopyCustomersAddresses, 0);
                    OrderCustomersAddresses[i] = CopyCustomersAddresses[minIndex];
                    CopyCustomersAddresses.RemoveAt(minIndex);

                    LocationNow = new Point(OrderCustomersAddresses[i].location);
                }
            }
            Shops = OrderShops;
            CustomersAddresses = OrderCustomersAddresses;
        }

        public static bool IsLate(double minuteFromNow, DateTime OriginalArriveTime)
        {
            DateTime ArriveTime = DateTime.Now.AddMinutes(minuteFromNow);
            int temp = DateTime.Compare(ArriveTime, OriginalArriveTime);
            return temp > 0;
        }
        //public  static  MatchIndex()
        //public static void AddPoint(Point startPoint, List<BLCustomersAddress> CopyCustomersAddress,)
        //{

        //}
    }
}