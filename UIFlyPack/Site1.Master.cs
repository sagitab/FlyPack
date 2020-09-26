using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int type = -1;


            BLUser user = (BLUser)Session["user"];
            string des = "";
            if (user != null)
            {
                type = user.Type;
                des = $"Ahoy! {user.ToString()}!";
            }

            switch (type)
            {
                case 1:
                    //sh
                    ShopMenager.Visible = true;
                    UserString2.Text = des;
                    break;
                case 2:
                    //system
                    SystemMenager.Visible = true;
                    UserString3.Text = des;
                    break;
                case 3:
                    //delivery
                    Delivery.Visible = true;
                    UserString4.Text = des;
                    break;
                case 4:
                    //customer
                    Customer.Visible = true;
                    UserString.Text = des;
                    break;
            }
            if (type != -1)
            {
                UnConected.Visible = false;
            }

        }

        protected void LogIn1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }

        protected void LogOut1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("HomePage.aspx");
        }
    }
}