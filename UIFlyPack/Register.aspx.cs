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
                Session["user"]=new BLUser("12345678");
                BLUser user = (BLUser) Session["user"];
                int type = user.Type;
                if (type==1)
                {
                    instractor.InnerHtml = "Type shop address or click on the map to add address";
                }
            }
        }

        public double GetLat(string latlng)
        {
            string lat = "";
            for (int i = 0; i < latlng.Length&&latlng[i]!=','; i++)
            {
                lat += latlng[i];
            }

            return double.Parse(lat);
        }
        public double GetLng(string latlng)
        {
            string lng = "";
            int start = latlng.IndexOf(',');
            for (int i = start+1; i < latlng.Length; i++)
            {
                lng += latlng[i];
            }

            return double.Parse(lng);
        }
        protected void regB_Click(object sender, EventArgs e)
        {
            bool validetors = NameValidator.IsValid && LNameValidator.IsValid && passValidator.IsValid && EmailValidator.IsValid &&PhoneValidator.IsValid&& IDValidator.IsValid;
            if (validetors && Name.Text != "" && LName.Text != "" && Email.Text != "" && pass.Value != "" && Phone.Text != "" )
            {
                string id = ID.Text;
                string name = Name.Text;
                string lname = LName.Text;
                string password = pass.Value ;
                string phoneNum = Phone.Text;
                string email = Email.Text;
                bool passCheck = BLUser.PasswordCheck(password);
                string LatLng =this.LatLng.Value.ToString();
                double lat = GetLat(LatLng);
                double lng = GetLng(LatLng);
                if (passCheck)
                {
                    int type =int.Parse( Request.QueryString.Get("Type"));
                    BLUser user = new BLUser(id, type, email, phoneNum, name, lname, password, lat, lng);
                    MSG.Text = "Register completed";
                }
                else
                {
                    MSG.Text = "Password caught please try again";
                }
              
            }
            else
            {
                MSG.Text = "Please type all the details";
            }
        }
    }
}