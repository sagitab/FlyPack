using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void LogInB_Click(object sender, EventArgs e)
        {
            //get input values
            string name = Name.Text;
            string pass = Pass.Value.ToString();
            if(name!=""&&pass!="")
            {
                BlUser user = null;
                try
                {
                    user = new BlUser(pass,name);//create new user by pass
                }
                catch (Exception exception)
                {
                    massage.Text = "Fail to log in "+exception.Message;
                    return;
                }
                bool isExist = Page.IsValid && user.Type!=0;//check if all validators are valid and user is exist
                if (isExist)
                {
                    user = user.Type == 1 ? (BlUser) new BlShopManager(pass) : new BlOrderUser(pass);//choose the right constructor by type
                    Session["user"] = user;
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    massage.Text = "User name or password incorrect";//fail massage
                }
            }
            else
            {
                massage.Text = "password field or name field is empty ";//fail massage
            }
        }
    }
}