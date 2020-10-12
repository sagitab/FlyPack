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
            
            string name = Name.Text;
            string pass = Pass.Value.ToString();
            if(name!=""&&pass!="")
            {
                BlUser user = null;
                user =  new BlUser(pass);
              
                bool isExsist = NameValidator.IsValid && passValidator.IsValid && user != null&&user.Type!=0;
                if (isExsist)
                {
                    if (user.Type == 1)
                    {
                        user = new BlShopManager(pass);
                    }
                    Session["user"] = user;
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    massage.Text = "User name or password incorrect";
                }
            }
            else
            {
                massage.Text = "User name or password incorrect";
            }
        }
    }
}