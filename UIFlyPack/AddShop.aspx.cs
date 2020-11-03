using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
namespace UIFlyPack
{
    public partial class AddShop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                //set data source
                ShopMSelect.DataSource = BLFlyPack.BlShopManager.ShopManagerTable();
                ShopMSelect.DataTextField = "FirstName";
                ShopMSelect.DataValueField = "ID";
                // Bind the data to the control.
                ShopMSelect.DataBind();

                // Set the default selected item, if desired.
                ShopMSelect.SelectedIndex = 0;
            }
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void AddShop_Click(object sender, EventArgs e)
        {
            //get input values
            string shopName = ShopName.Text;
            string shopManagerId = ShopMSelect.SelectedValue;
            if (Page.IsValid&& ShopName.Text!="")//if all validator is valid
            {
                BlShop shop = null;
                try
                {
                    shop = new BlShop(shopManagerId, shopName);//crate the new shop
                }
                catch (Exception exception)
                {
                    MSG.Text = "fail to add shop " + exception.Message;//ex massage
                }

                if (shop == null || shop.Id == -1)//if id=-1 of shop is null add shop fail
                {
                    MSG.Text = "fail to add shop";
                }
                else//if not  add shop success
                {
                    MSG.Text = "shop added seccsessfully!!!";
                }
            }
            else
            {
                MSG.Text = "Please type all details";
            }
            ShopName.CssClass = ShopNameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";//convert the the bob style by the validator


        }
    }
}