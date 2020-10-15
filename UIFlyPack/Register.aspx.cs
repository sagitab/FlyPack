using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
namespace UIFlyPack
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
               
                //Session["user"]=new BLUser("12345678");
                BlUser user = (BlUser)Session["user"];
                if (user != null)
                {
                    int type = user.Type;
                    if (type == 1)
                    {
                        instractor.InnerHtml = "Type shop address or click on the map to add address";
                    }

                    int RegType = 0;
                    try
                    {
                        RegType= int.Parse(Request.QueryString.Get("Type"));
                    }
                    catch
                    { 
                        Response.Redirect("HomePage.aspx");
                    }

                    switch (RegType)
                    {
                        case 3:
                            PageHeader.InnerHtml = "Add delivery";
                            break;
                        case 1:
                            PageHeader.InnerHtml = "Add shop manager";
                            break;
                        case 4:
                            PageHeader.InnerHtml = "Register";
                            break;
                        default:
                            Response.Redirect("HomePage.aspx");
                            break;
                    }

                }
                //else
                //{
                //    //security 
                //}

            }
            Validate();
        }

        public double GetLat(string LatLng)
        {
            string lat = "";
            for (int i = 0; i < LatLng.Length && LatLng[i] != ','; i++)
            {
                lat += LatLng[i];
            }

            return double.Parse(lat);
        }
        public double GetLng(string LatLng)
        {
            string lng = "";
            int start = LatLng.IndexOf(',');
            for (int i = start + 1; i < LatLng.Length; i++)
            {
                lng += LatLng[i];
            }

            return double.Parse(lng);
        }
        protected void regB_Click(object sender, EventArgs e)
        {

            bool validetors = Page.IsValid;
            if (validetors && Name.Text != "" && LName.Text != "" && Email.Text != "" && pass.Text != "" && Phone.Text != "" && ID.Text != "")
            {
                //get input values
                string id = ID.Text;
                string name = Name.Text;
                string lname = LName.Text;
                string password = pass.Text;
                string phoneNum = Phone.Text;
                string email = Email.Text;
                bool passCheck = false;
                try
                {
                    passCheck = BlUser.PasswordCheck(password);
                }
                catch (Exception exception)
                {
                      MSG.Text = "fail register "+exception.Message;
                }
                string latLng = this.LatLng.Value.ToString();
                double lat = GetLat(latLng);
                double lng = GetLng(latLng);
                if (passCheck)
                {

                  
                    int type = 0;
                    try
                    {
                        type = int.Parse(Request.QueryString.Get("Type"));
                    }
                    catch
                    {
                        Response.Redirect("HomePage.aspx");
                        return;
                    }

                    //if (type!=1&& type != 3&&type != 4)
                    //{
                    //    Response.Redirect("HomePage.aspx");
                    //    return;
                    //}
                    BlUser user = null;
                    try
                    {
                         user = new BlUser(id, type, email, phoneNum, name, lname, password, lat, lng);
                    }
                    catch (Exception exception)
                    {
                        MSG.Text = "fail register "+exception.Message;
                    }

                    //log in when finish to register
                    //if (user!=null)
                    //{
                    //    string pass = user.Password;
                    //    user = user.Type == 1 ? (BlUser)new BlShopManager(pass) : new BlOrderUser(pass);
                    //    Session["user"] = user;
                    //}
                  
                    MSG.Text = "Register completed";
                }
                else
                {


                    MSG.Text = "Password caught please try again";
                }

            }
            else
            {
                Phone.CssClass = PhoneValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                pass.CssClass = passValidator.IsValid&& PassNotEmpty.IsValid ? "TextBox" : "TextBoxUnValidValue";
                Email.CssClass = EmailValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                Name.CssClass = NameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                LName.CssClass = LNameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                ID.CssClass = IDValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";


                MSG.Text = "Please type all the details";
            }
        }


    }
}