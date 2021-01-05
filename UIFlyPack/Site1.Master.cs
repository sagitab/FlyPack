using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        //public static List<BLProduct> shoppingCart {  set {  } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            if (user?.UserId == null) return;
            var type = user.Type;
            var des = $"Ahoy! {user.ToString()}!";
            //const  int ShopManager = GlobalVariable.ShopManager;
            //if (type==GlobalVariable.ShopManager)
            //{

            //}
            switch (type)//display the right nave bar
            {
                case 1:
                    //sh
                    ShopMenager.Visible = true;
                    UserString2.Text = des;
                    break;
                case 2:
                    //system
                    SystemMenager.Visible = true;
                    UserString4.Text = des;
                    break;
                case 3:
                    //delivery
                    Delivery.Visible = true;
                    UserString3.Text = des;
                    break;
                case 4:
                    //customer
                    Customer.Visible = true;
                    UserString.Text = des;
                    List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                    Update(productsCart);
                    OrderNow.Visible = productsCart?.Count > 0;
                    break;
            }
            UnConected.Visible = false;
        }

        //protected void LogIn1_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("LogIn.aspx");
        //}

        protected void LogOut1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("HomePage.aspx");
        }

        protected void LogoB_OnClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        //sdfdddddd!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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
            if (e.CommandName == "Remove")//removing an product
            {
                //get productsCart list
                List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                int DeleteIndex = e.Item.ItemIndex;//get delete index
                productsCart.RemoveAt(DeleteIndex);//remove at the list
                int[] amounts = (int[])Session["productAmount"];  //get amounts array
                //update num of products
                int numOfProducts = (int)Session["numOfProducts"];
                Session["numOfProducts"] = numOfProducts - 1;
                //update amounts arr
                BLProduct.Delete(amounts, DeleteIndex);
                Session["productAmount"] = amounts;
                Update(productsCart);//update data list
                OrderNow.Visible = productsCart?.Count > 0;
            }
        }

        protected void ProductsCart_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (IsGoodAmount())
            {
                int[] productAmounts = (int[])Session["productAmount"]; //get amounts array
                var amount = productAmounts?[e.Item.ItemIndex] ?? 1;//get amount from array
                if (amount == 0)
                {
                    amount = 1;//minimum amount value is 1
                }
                TextBox t = (TextBox)e.Item.FindControl("numOfProduct");//get amount
                t.Text = "" + amount;//update  text
            }
            else
            {
                //msg!!!!!!!!!!!!!!!!!!!!
            }
          
            //Label l = (Label)e.Item.FindControl("amount");//get amount label
            //l.Text = "" + amount;//update label text
        }

        protected void OrderNow_OnClick(object sender, EventArgs e)
        {
            if (IsGoodAmount())
            {
                List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                BLProduct product = productsCart[0];
                int shopId = BLProduct.GetShopIdByProductId(product.Id);//get shop id
                Response.Redirect("OrderNow.aspx?shopId=" + shopId);//pass shop id in qwaery string
            }
            else
            {
                //msg!!!!!!!!!!!!!!!!!!!!
            }

        }

        protected void XButton_OnClick(object sender, ImageClickEventArgs e)
        {
            if (IsGoodAmount())
            {
                shoppingCartPanel.Visible = false;//to 'remove' shoppingCartPanel
            }
            else
            {
                //msg!!!!!!!!!!!!!!!!!!!!
            }
        
          
           
        }

        protected void shoppingCartB_OnClick(object sender, ImageClickEventArgs e)
        {
            shoppingCartPanel.Visible = true;//to 'show' shoppingCartPanel
        }

        public bool IsGoodAmount()
        {
            int[] productAmounts = new int[6];
            int sumAmount = 0;
            foreach (DataListItem item in ProductsCart.Items)
            {
                TextBox t = (TextBox)item.FindControl("numOfProduct");
                int amount = int.Parse(t.Text);
                sumAmount += amount;
                productAmounts[item.ItemIndex] = amount;
            }
            if (sumAmount > 6)
            {
                return false;
            }
            Session["productAmount"] = productAmounts;
            return true;
        }
    }
}