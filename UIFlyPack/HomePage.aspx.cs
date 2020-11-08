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
            //to del########################@@@@@@@@########%%%%%%%%%>>>>>>>>>>>>
            /* Session["user"] = new BlShopManager("12345678");*/ // BLShopMenager
            /* Session["user"] = new BlOrderUser("shlakot1");*/ //deliver
            /*  Session["user"] = new BlOrderUser("hoohoo12");*///customer
         /*   Session["user"] = new BlOrderUser("lucky123");*///system Maneger
            if (Page.IsPostBack) return;//if page already do page loud the headers is already changed

            //set all headers Visible to false
            shopManager.Visible = false;
            systemManager.Visible = false;
            delivery.Visible = false;
            customer.Visible = false;
            unconnected.Visible = false;
            BlUser user = (BlUser)Session["user"];
            //to check if user verify email!!!!!!!!!!!!!!!!!!!!!!!!!!
            try
            {
                if (GlobalVariable.UnVerifyEmail != null&& user!=null)
                {
                    string verifyCode = GlobalVariable.UnVerifyEmail[user.UserId];
                    Response.Redirect("VerifyEmail.aspx");
                }
            }
            catch
            {

            }
            if (user != null)
            {
                int type = user.Type;
                switch (type)//choose the right header
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
            else//hide unconnected headers if there is a connected user
            {
                unconnected.Visible = true;
            }
        }
    }
}