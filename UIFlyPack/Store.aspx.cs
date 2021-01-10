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
        public string[] OrderByArr = { "expansive first", "cheep first", "z-a", "a-z", "ID" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Products.Visible = false;
                searchPanel.Visible = false;
                //set data source

                productOrder.DataSource = OrderByArr;
                //// Bind the data to the control.
                productOrder.DataBind();

                // Set the default selected item, if desired.
                productOrder.SelectedIndex = 0;

                //set data source
                List<BlShop> shops= BLFlyPack.BlShop.GetShops();
                shops.Add(new BlShop("-1","all products"));
                Shops.DataSource = shops;
                Shops.DataTextField = "ShopName";
                Shops.DataValueField = "ID";
                
                // Bind the data to the control.
                Shops.DataBind();
                foreach (System.Web.UI.WebControls.ListItem Item in Shops.Items)
                {
                    Item.Attributes["OnClientClick"] = "ShopDropDownListB_OnClick";
                }
                // Set the default selected item, if desired.
                Shops.SelectedIndex = shops.Count-1;
                Session["products"] = null;
                //Products.ProductsCollections = products;
                List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                if (productsCart == null || productsCart.Count == 0)
                {
                    MSG.Text = "";
                    return;
                }
            }
        }

        protected void SearchProductB_OnClick(object sender, EventArgs e)
        {
            string searchValue = serchedValue.Text;//get search value
            if (searchValue != "")
            {
                //set values and take care of unexpected value or null error
                int orderBy, shopId;
                try
                {
                    orderBy = int.Parse(productOrder.SelectedValue);
                }
                catch
                {
                    orderBy = 4;
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
                    string ch = SearchBy.Items[SearchBy.SelectedIndex].ToString();//get by what to search
                    bool IsSearchName = (ch == "Product name");
                    if (!IsSearchName && !double.TryParse(searchValue, out double result))
                    {
                        //if searching by price and search val is not a value error msg
                        MSG.Text = "please a valid price '" + searchValue + "' is not a number";
                    }
                    else
                    {
                        //get the list by the parameters
                        List<BLProduct> products = BLProduct.Search(searchValue, shopId, IsSearchName, orderBy);
                        Products.ProductsCollections = products;//set data source
                        MSG.Text = "";//set text to no error
                    }
                }
                catch (Exception exception)
                {
                    MSG.Text = "please select by what you want to search " + exception.Message;// error massage
                }

            }
            else
            {
                MSG.Text = "please type the value you want to search";// error massage
            }

        }

        public int TurnOrderByToInt(string stringOrderBy)
        {
            for (var index = 0; index < OrderByArr.Length; index++)
            {
                var value = OrderByArr[index];
                if (stringOrderBy == value.ToString())
                {
                    return index;
                }
            }

            return 4;
        }
        protected void productOrder_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string stringOrderBy = productOrder.SelectedValue;//get stringOrderBy
            int orderBy = TurnOrderByToInt(stringOrderBy);//turn to int
            int shopId = int.Parse(Shops.SelectedValue);//get shop id
            string condition = BLProduct.setOrderBy(orderBy);//get condition by order by int
            List<BLProduct> products = null;
            if (shopId == -1)
            {
                //set data source if no shop selected
                products = BLProduct.GetAllProducts(condition);
                Products.ProductsCollections = products;
            }
            else
            {
                //set data source
                products = BLProduct.GetAllProductsByShopId(shopId, condition);
                Products.ProductsCollections = products;
            }


        }

        protected void Shops_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            searchPanel.Visible = true;
            Products.Visible = true;
            shopSelectDiv.Visible = false;
            List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
            bool toChangeShop = true;
            if (productsCart != null && productsCart.Count > 0)
            {
                //massage to delete all products in cart
                /* bool ToDelete= */
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Confirm()", true);
                string isConfirmString = isConfirm.Value;
                
                if (isConfirmString == "1")
                {
                    Session["productsCart"] = null;
                    toChangeShop = false;
                }
                isConfirm.Value = "0";
                //DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
                //if (dialogResult == DialogResult.Yes)
                //{
                //    //do something
                //}
                //else if (dialogResult == DialogResult.No)
                //{
                //    //do something else
                //}
            }

            if (toChangeShop)
            {
                int shopId = int.Parse(Shops.SelectedValue);//get shop id
                if (shopId == -1)
                {
                    Products.ProductsCollections = BLProduct.GetAllProducts("");
                }
                else
                {
                    //set data source                                         
                    List<BLProduct> products = BLProduct.GetAllProductsByShopId(shopId, "");
                    if (products != null && products.Count > 0)
                    {
                        Products.ProductsCollections = products;
                    }
                    else
                    {
                        Products.ProductsCollections = null;
                    }
                }
                
            }
        }

        protected void ChangeShop_OnClick(object sender, EventArgs e)
        {
            searchPanel.Visible = false;
            Products.Visible = false;
            shopSelectDiv.Visible = true;
        }
    }
}