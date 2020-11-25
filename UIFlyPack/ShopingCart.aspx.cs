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
            }
            OrderNow.Visible = productsCart?.Count > 0;
        }

        public void UpdateSumCart(List<BLProduct> productsCart)
        {
            int[] productAmounts = (int[])Session["productAmount"];
            int numOfProducts = BLProduct.SumArr(productAmounts);
            NumOfProducts.Text = "Num Of Products-" + numOfProducts;
            double totalPrice = BLProduct.TotalPrice(productsCart, productAmounts);
            TotalPrice.Text = "Total Price-" + totalPrice;
        }
        public void Update(List<BLProduct> products)
        {
            ProductsCart.DataSource = products;
            Session["productsCart"] = products;
            ProductsCart.DataBind();
            UpdateSumCart(products);
            if (products == null || products.Count == 0)
            {
                MSG.Text = "there is no products"; //error msg
                NumOfProducts.Text = "";
                TotalPrice.Text = "";
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
                //update num of products
                int numOfProducts = (int)Session["numOfProducts"];
                Session["numOfProducts"] = numOfProducts -1;
                //update amounts arr
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

        protected void OrderNow_OnClick(object sender, EventArgs e)
        {
            List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
            BLProduct product = productsCart[0];
            int shopId = BLProduct.GetShopIdByProductId(product.Id);
            Response.Redirect("OrderNow.aspx?shopId=" + shopId);
        }

        protected void XButton_OnClick(object sender, ImageClickEventArgs e)
        {
            shoppingCartPanel.Visible = false;
        }
    }
}