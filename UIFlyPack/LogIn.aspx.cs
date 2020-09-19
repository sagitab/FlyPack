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
                BLUser user = null;
                if (user.Type == 1)
                {
                    user = new BLShopMenager(pass);
                }
                else
                {
                    user = new BLUser(pass);
                }
               
                bool IsExsist = NameValidator.IsValid && passValidator.IsValid && user != null;
                if (IsExsist)
                {
                    
                    Session["user"] = user;
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    massage.Text = "User name or password uncorect";
                }
            }
            else
            {
                massage.Text = "User name or password uncorect";
            }
        }
    }
}