using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    
    public partial class DeliveryMap : System.Web.UI.Page
    {
        //ScriptingJsonSerializationSection
        public List<BLShop> Shops = null;
        public List<BLCustomersAddress> CustomersAddresses = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"]= new BLUser("gigi1234");
            BLUser user = (BLUser)Session["user"]; /*(BLUser)Session["user"];*/
            List<BLOrder> orders = BLOrder.GetOrdersListByTime(user.UserID);
            List<BLShop> shops=new List<BLShop>();
            List<BLCustomersAddress> customersAddresses=new List<BLCustomersAddress>();
            foreach (BLOrder order in orders)
            {
                shops.Add( BLShop.GetShopById(order.ShopID));
                Point CustomerAddress = null;
                CustomerAddress = order.location != null ? new Point(order.location) : new Point(user.location);
                customersAddresses.Add(new BLCustomersAddress(CustomerAddress, order.NumOfFloor,BLUser.GetName(order.CustomerID)));
            }
            //calculate better way
            Shops = shops;
            CustomersAddresses = customersAddresses;
        }
    }
}