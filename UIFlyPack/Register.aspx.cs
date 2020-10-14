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
                }

            }

            Validate();
            Phone.CssClass = PhoneValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            pass.CssClass = passValidator.IsValid && PassNotEmpty.IsValid ? "TextBox" : "TextBoxUnValidValue";
            Email.CssClass = EmailValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            Name.CssClass = NameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            LName.CssClass = LNameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            ID.CssClass = IDValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //if (!PhoneValidator.IsValid)
            //{
            //    Phone.CssClass = PhoneValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //}
            //if (!passValidator.IsValid)
            //{
            //    pass.CssClass = passValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //}
            //if (!EmailValidator.IsValid)
            //{
            //    pass.CssClass = EmailValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //}
            //if (!NameValidator.IsValid)
            //{
            //    pass.CssClass = NameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //}
            //if (!LNameValidator.IsValid)
            //{
            //    pass.CssClass = LNameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //}
            //if (!IDValidator.IsValid)
            //{
            //    pass.CssClass = IDValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
            //}
        }

        public double GetLat(string latlng)
        {
            string lat = "";
            for (int i = 0; i < latlng.Length && latlng[i] != ','; i++)
            {
                lat += latlng[i];
            }

            return double.Parse(lat);
        }
        public double GetLng(string latlng)
        {
            string lng = "";
            int start = latlng.IndexOf(',');
            for (int i = start + 1; i < latlng.Length; i++)
            {
                lng += latlng[i];
            }

            return double.Parse(lng);
        }
        protected void regB_Click(object sender, EventArgs e)
        {

            bool validetors = Page.IsValid;
            if (validetors && Name.Text != "" && LName.Text != "" && Email.Text != "" && pass.Text != "" && Phone.Text != "" && ID.Text != "")
            {
                string id = ID.Text;
                string name = Name.Text;
                string lname = LName.Text;
                string password = pass.Text;
                string phoneNum = Phone.Text;
                string email = Email.Text;
                bool passCheck = BlUser.PasswordCheck(password);
                string latLng = this.LatLng.Value.ToString();
                double lat = GetLat(latLng);
                double lng = GetLng(latLng);
                if (passCheck)
                {
                    int type = int.Parse(Request.QueryString.Get("Type"));
                    BlUser user = new BlUser(id, type, email, phoneNum, name, lname, password, lat, lng);
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