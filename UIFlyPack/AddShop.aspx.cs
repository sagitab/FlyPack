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
            string shopName = ShopName.Text;
            string shopManegerId = ShopMSelect.SelectedValue;
            BlShop shop = new BlShop(shopManegerId,shopName);
            if(shop!=null||shop.Id==-1)
            {
                MSG.Text = "fail to add shop";
            }
            else
            {
                MSG.Text = "shop added seccsessfully!!!";
            }
        }
    }
}