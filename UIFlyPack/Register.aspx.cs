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
            bool validetors = NameValidator.IsValid && LNameValidator.IsValid && passValidator.IsValid && EmailValidator.IsValid &&PhoneValidator.IsValid;
            if (validetors && Name.Text != "" && LName.Text != "" && Email.Text != "" && pass.Value != "")
            {
                string name = Name.Text;
                string lname = LName.Text;
                string password = pass.Value ;
                string phoneNum = Phone.Text;
                string adress = Adress.Text;
                string email = Email.Text;
                int numOFFloor = int.Parse(NumOfFloor.Text);
                bool passCheck = BLUser.PasswordCheck(password);
                //if()
                BLUser user = new BLUser(email,phoneNum,name,lname,adress, password,4, numOFFloor);
                MSG.Text = "Register complited";
            }
            else
            {
                MSG.Text = "pleas type all the details";
            }
        }
    }
}