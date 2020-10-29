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
            //if (!Page.IsPostBack)
            //{
            //    WhichAnimation.Value = "0";
            //}
          
        }

        protected void LogInB_Click(object sender, EventArgs e)
        {
            //get input values
            string name = Name.Text;
            string pass = Pass.Text;
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
                    WhichAnimation.Value = "2";
                    ////ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:MoveDrone(); ", true);
                    //ScriptManager.RegisterStartupScript( Page,GetType(), "Javascript", "javascript:MoveDrone();", true);
                    Response.Redirect("LogIn.aspx");
                    System.Threading.Thread.Sleep(3000);
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    WhichAnimation.Value = "3";
                    massage.Text = "User name or password incorrect";//fail massage
                }
            }
            else
            {
                WhichAnimation.Value = "3";
                massage.Text = "password field or name field is empty ";//fail massage
            }

        }

        protected void CleanB_Click(object sender, ImageClickEventArgs e)
        {
            Name.Text = "";
            Pass.Text = "";
            WhichAnimation.Value = "1";
        }
    }
}