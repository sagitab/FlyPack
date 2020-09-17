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
                if(passCheck)
                {
                    int type =int.Parse( Request.QueryString.Get("Type"));
                    BLUser user = new BLUser(email, phoneNum, name, lname, password, type, id);
                    MSG.Text = "Register complited";
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