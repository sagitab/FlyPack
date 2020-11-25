using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            //Session["user"] = new BlOrderUser("shlakot1");
            //try
            //{
            //    BlOrderUser user = (BlOrderUser)Session["user"];
            //    GlobalVariable.UnVerifyEmail.Add(user.UserId, "111111");
            //}
            //catch 
            //{
                
            //}
            
        }

        protected void verifyB_OnClick(object sender, EventArgs e)
        {

            if (!IsValid) return;
            if (verifyCode.Text=="")
            {
                MSG.Text = "invalid code please try again"; //error massage 
                return;
            }
            BlOrderUser user = (BlOrderUser)Session["user"];
            if (user == null)
            {
                Response.Redirect("HomePage.aspx");//if unconnected cant find the Verify Code
                return;
            }

            string RealVerifyCode;
            try
            {
                //get Verify Code
                RealVerifyCode = ((Dictionary<string, string>)Application["UnVerifyEmail"])[user.UserId];
            }
            catch 
            {
                Response.Redirect("HomePage.aspx");//if customer not have verify code he doesn't need to verify email
                return;
            }
           
            string TryVerifyCode = verifyCode.Text;
            if (RealVerifyCode == TryVerifyCode)
            {
                //remove from customer that unverified their email list
                GlobalVariable.UnVerifyEmail.Remove(user.UserId);
                MSG.Text = "register completed";//success massage 
            }
            else
            {
                MSG.Text = "invalid code please try again";//error massage 
            }
        }
    }
}