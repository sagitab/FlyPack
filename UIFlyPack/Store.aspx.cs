using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class Store : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //set data source
                string[] OrderByArr = { "expansive first", "cheep first", "z-a", "a-z", "ID" };
                productOrder.DataSource = OrderByArr;
                //// Bind the data to the control.
                //productOrder.DataTextField = "Key";
                //productOrder.DataValueField = "Value";
                productOrder.DataBind();

                // Set the default selected item, if desired.
                productOrder.SelectedIndex = 0;

                //set data source
                Shops.DataSource = BLFlyPack.BlShop.GetShops();
                Shops.DataTextField = "ShopName";
                Shops.DataValueField = "ID";
                // Bind the data to the control.
                Shops.DataBind();

                // Set the default selected item, if desired.
                Shops.SelectedIndex = 0;
            }
        }

        protected void SearchProductB_OnClick(object sender, EventArgs e)
        {

            string searchValue = serchedValue.Text;
            if (searchValue != "")
            {

                int orderBy, shopId;
                try
                {
                    orderBy = int.Parse(productOrder.SelectedValue);
                }
                catch
                {
                    orderBy = 5;
                }
                try
                {
                    shopId = int.Parse(Shops.SelectedValue);
                }
                catch
                {
                    shopId = -1;
                }


                try
                {
                    bool IsSearchName = (SearchBy.Items[SearchBy.SelectedIndex].ToString() == "Description");
                    List<BLProduct> products = Product.Search(searchValue, shopId, IsSearchName, orderBy);
                    Session["products"] = products;
                    MSG.Text = "";
                }
                catch (Exception exception)
                {
                    MSG.Text = "please select by what you want to search "+ exception.Message;
                }

            }
            else
            {
                MSG.Text = "please type the value you want to search";
            }

        }

        protected void productOrder_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int orderBy = int.Parse(productOrder.SelectedValue);
            int shopId = int.Parse(Shops.SelectedValue);
            string condition = "ORDER BY " + Product.setOrderBy(orderBy);
            List<BLProduct> products = null;
            if (shopId == -1)
            {
                products = BLProduct.GetAllProducts(condition);
                Session["products"] = products;
            }
            else
            {
                products = BLProduct.GetAllProductsByShopId(shopId, condition);
                Session["products"] = products;
            }


        }

        protected void Shops_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int shopId = int.Parse(Shops.SelectedValue);
            List<BLProduct> products = BLProduct.GetAllProductsByShopId(shopId, "");
            Session["products"] = products;
            //update!!!!!!
        }
    }
}