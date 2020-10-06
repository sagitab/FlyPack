using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
namespace UIFlyPack
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*  Session["user"] = new BLShopMenager("12345678");*/ //to del BLUser BLShopMenager
            if (!Page.IsPostBack)
            {
                shopManager.Visible = false;
                systemManager.Visible = false;
                delivery.Visible = false;
                customer.Visible = false;
                unconnected.Visible = false;
                BLUser user = (BLUser)Session["user"];
                if (user != null)
                {
                    int type = user.Type;
                    switch (type)
                    {
                        case 1:
                            shopManager.Visible = true;
                            break;
                        case 2:
                            systemManager.Visible = true;
                            break;
                        case 3:
                            delivery.Visible = true;
                            break;
                        default:
                            customer.Visible = true;
                            break;
                    }
                }
                else
                {
                    unconnected.Visible = true;
                }


            }
        }
    }
}