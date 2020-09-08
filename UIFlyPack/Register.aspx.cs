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
            ////////
        }

        protected void regB_Click(object sender, EventArgs e)
        {
            bool validetors = NameValidator.IsValid && LNameValidator.IsValid && passValidator.IsValid && EmailValidator.IsValid &&PhoneValidator.IsValid;
            if (validetors && Name.Text != "" && LName.Text != "" && Email.Text != "" && pass.Value != "")
            {
                string name = Name.Text;
                string password = pass.Value ;
                string phoneNum = Phone.Text;

                string email = Email.Text;
                //BLUser user = new BLUser();
                MSG.Text = "ההרשמה בוצעה בהצלחה";
            }
            else
            {
                MSG.Text = "יש להזין את כל הפרטים";
            }
        }
    }
}