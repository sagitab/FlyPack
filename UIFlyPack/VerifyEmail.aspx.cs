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
                MSG.Text = "invalid code please try again";
                return;
            }
            BlOrderUser user = (BlOrderUser)Session["user"];
            if (user == null)
            {
                Response.Redirect("HomePage.aspx");
                return;
            }

            string RealVerifyCode;
            try
            {
                RealVerifyCode = ((Dictionary<string, string>)Application["UnVerifyEmail"])[user.UserId];
            }
            catch 
            {
                Response.Redirect("HomePage.aspx");
                return;
            }
           
            string TryVerifyCode = verifyCode.Text;
            if (RealVerifyCode == TryVerifyCode)
            {
                GlobalVariable.UnVerifyEmail.Remove(user.UserId);
                MSG.Text = "register completed";
            }
            else
            {
                MSG.Text = "invalid code please try again";
            }
        }
    }
}