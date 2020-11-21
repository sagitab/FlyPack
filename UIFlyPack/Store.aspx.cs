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
            try
            {
                string SisAddedNew = Request.QueryString.Get("errorMSG");
                if (SisAddedNew!=null)
                {
                    bool isAddedNew = bool.Parse(SisAddedNew);
                    MSG.Text = isAddedNew ? "product added to cart!!" : "product amount updated!!";
                    Request.QueryString.Clear();
                    return;
                }
              
            }
            catch 
            {
                //do nothing
            }
       
            if (!Page.IsPostBack)
            {
                //set data source
              
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
                List<BLProduct> products = BLProduct.GetAllProducts("");
                Session["products"] = products;
                //Products.ProductsCollections = products;
             
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
                    string ch = SearchBy.Items[SearchBy.SelectedIndex].ToString();
                    bool IsSearchName = (ch == "Product name");
                    List<BLProduct> products = BLProduct.Search(searchValue, shopId, IsSearchName, orderBy);
                    Products.ProductsCollections = products;
                    MSG.Text = "";
                }
                catch (Exception exception)
                {
                    MSG.Text = "please select by what you want to search " + exception.Message;
                }

            }
            else
            {
                MSG.Text = "please type the value you want to search";
            }

        }

        public  int TurnOrderByToInt(string stringOrderBy)
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
            string stringOrderBy = productOrder.SelectedValue;
            int orderBy = TurnOrderByToInt(stringOrderBy);
            int shopId = int.Parse(Shops.SelectedValue);
            string condition = BLProduct.setOrderBy(orderBy);
            List<BLProduct> products = null;
            if (shopId == -1)
            {
                products = BLProduct.GetAllProducts(condition);
                Products.ProductsCollections = products;
            }
            else
            {
                products = BLProduct.GetAllProductsByShopId(shopId, condition);
                Products.ProductsCollections = products;
            }


        }

        protected void Shops_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int shopId = int.Parse(Shops.SelectedValue);
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