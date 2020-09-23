using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
namespace UIFlyPack
{
    public partial class OrderNow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DateTime time = new DateTime(2020, 9, 9, 1, 30, 0);
                times.Items[0].Value = time.ToString() ;
                //set data source
                ShopDropDownList.DataSource = BLFlyPack.BLShop.GetShops();
                ShopDropDownList.DataTextField = "ShopName";
                ShopDropDownList.DataValueField = "ID";
                // Bind the data to the control.
                ShopDropDownList.DataBind();

                // Set the default selected item, if desired.
                ShopDropDownList.SelectedIndex = 0;
            }
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
          

        }

        protected void OrderB_Click(object sender, EventArgs e)
        {
            int shopOrderID = int.Parse(ShopOrderID.Text);//ShopDropDownList
            string address = Adress.Text;
            string arriveTime = times.Items[times.SelectedIndex].Value.ToString();
            int shopID = int.Parse(ShopDropDownList.SelectedValue);
            //int shopID = int.Parse(ShopDropDownList.Items[index].ToString());
            DateTime AriveDateTime = DateTime.Parse(arriveTime);
            int numOfFloor = int.Parse(NumOfFloor.Text);
            BLUser user = (BLUser)Session["user"];
            BLOrder order = new BLOrder(user.UserID,"111111111",shopID,AriveDateTime, DateTime.Parse("01/01/1 1:11:11"),1, numOfFloor,1,1);
            MSG.Text = "order successes!!";
        }
    }
}