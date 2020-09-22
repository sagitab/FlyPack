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
        }
    }
}