using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
            if (!Page.IsPostBack)
            {
                Update(productsCart);
                //Update(BLProduct.GetAllProducts(""));//to del
            }
            OrderNow.Visible = productsCart?.Count > 0;
        }

        public void Update(List<BLProduct> products)
        {
            ProductsCart.DataSource = products;
            Session["productsCart"] = products;
            ProductsCart.DataBind();
            if (products == null || products.Count == 0)
            {
                MSG.Text = "there is no products"; //error msg
                return;
            }
            MSG.Text = "";
        }

        protected void ProductsCart_OnItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                int DeleteIndex = e.Item.ItemIndex;
                productsCart.RemoveAt(DeleteIndex);
                int[] amounts = (int[])Session["productAmount"];
                BLProduct.Delete(amounts, DeleteIndex);
                Session["productAmount"] = amounts;
                Update(productsCart);
                OrderNow.Visible = productsCart?.Count > 0;
            }
        }

        protected void ProductsCart_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            int[] productAmounts = (int[])Session["productAmount"];
            var amount = productAmounts == null ? 1 : productAmounts[e.Item.ItemIndex];
            if (amount == 0)
            {
                amount = 1;
            }
            Label l = (Label)e.Item.FindControl("amount");//ContentPlaceHolder1_ProductsCart_amount_0
            l.Text = "" + amount;
        }
    }
}