using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class DeliveryMap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLUser user = (BLUser) Session["user"];
            List<BLOrder> orders = BLOrder.GetOrdersListByTime(user.UserID);
            List<BLShop> shops=new List<BLShop>();
            List<BLCustomersAddress> customersAddresses=new List<BLCustomersAddress>();
            foreach (BLOrder order in orders)
            {
                shops.Add( BLShop.GetShopById(order.ShopID));
                Point CustomerAddress = null;
                CustomerAddress = order.Possision != null ? new Point(order.Possision) : new Point(user.Possision);
                customersAddresses.Add(new BLCustomersAddress(CustomerAddress, order.NumOfFloor,BLUser.GetName(order.CustomerID)));
            }
            
        }
    }
}