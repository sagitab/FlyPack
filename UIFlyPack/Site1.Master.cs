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
                    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                    Update(orderDetails);
                    OrderNow.Visible = orderDetails?.Count > 0;
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
        public void UpdateSumCart(List<BLOrderDetailsDB> orderDetails)
        {
            if (orderDetails == null) return;
            int numOfProducts = BLOrderDetailsDB.numOfProducts(orderDetails);
            NumOfProducts.Text = "Num Of Products-" + numOfProducts;
            double totalPrice = BLOrderDetailsDB.TotalPrice(orderDetails);
            TotalPrice.Text = "Total Price-" + totalPrice;
        }
        public void Update(List<BLOrderDetailsDB> orderDetails)
        {
            List<BLProduct> products = new List<BLProduct>();
            if (orderDetails != null)
            {
                products.AddRange(orderDetails.Select(detail => BLProduct.GetProductById(detail.productId)));
                EmptyCart.Visible = true;
            }
            ProductsCart.DataSource = products;
            Session["orderDetails"] = orderDetails;
            ProductsCart.DataBind();
            UpdateSumCart(orderDetails);
            if (orderDetails == null || orderDetails.Count == 0)
            {
                MSG.Text = "there is no products"; //error msg
                NumOfProducts.Text = "";
                TotalPrice.Text = "";
                OrderNow.Visible = false;
                return;
            }
            MSG.Text = "";
        }

        protected void ProductsCart_OnItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "Remove")//removing an product
            {

                //get orderDetails list 
                List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                int DeleteIndex = e.Item.ItemIndex;//get delete index
                orderDetails.RemoveAt(DeleteIndex);//remove at the list
                Update(orderDetails);//update data list
                OrderNow.Visible = orderDetails?.Count > 0;
            }
            if (e.CommandName == "plus")
            {
                if (IsGoodAmount())
                {
                    int productIndex = e.Item.ItemIndex;//get delete index
                    TextBox t = (TextBox)e.Item.FindControl("numOfProduct");//get amount 
                    //DropDownList productAmount = (DropDownList)e.Item.FindControl("productAmount");
                    //int numOfProduct = int.Parse(productAmount.Items[productAmount.SelectedIndex].ToString());
                    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                    int newAmount = int.Parse(t.Text)+1;
                    orderDetails[productIndex].amount = newAmount ;
                    t.Text =""+ newAmount;
                    UpdateSumCart(orderDetails);
                    Session["orderDetails"] = orderDetails;
                }
                else
                {
                    erMSG.Text = "can't add more items cart is full";
                }
            }
            if (e.CommandName == "minus")
            {

                int productIndex = e.Item.ItemIndex;//get delete index
                TextBox t = (TextBox)e.Item.FindControl("numOfProduct");//get amount 
                //DropDownList productAmount = (DropDownList)e.Item.FindControl("productAmount");
                //int numOfProduct = int.Parse(productAmount.Items[productAmount.SelectedIndex].ToString());
                int newAmount = int.Parse(t.Text)-1;
                if (newAmount>0)
                {
                    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                    orderDetails[productIndex].amount = newAmount;
                    t.Text = "" + newAmount;
                    UpdateSumCart(orderDetails);
                    Session["orderDetails"] = orderDetails;
                    erMSG.Text = "";
                }
                else
                {
                    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                    int DeleteIndex = e.Item.ItemIndex;//get delete index
                    orderDetails.RemoveAt(DeleteIndex);//remove at the list
                    Update(orderDetails);//update data list
                    OrderNow.Visible = orderDetails?.Count > 0;
                }
              

            }

        }

        protected void ProductsCart_OnItemDataBound(object sender, DataListItemEventArgs e)
        {

            //DropDownList productAmount = (DropDownList)e.Item.FindControl("productAmount");
            //productAmount.Attributes.Add("Item.index", e.Item.ItemIndex.ToString());
            //int[] arr = new int[5];
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = i + 1;
            //}
            //productAmount.DataSource = arr;
            //int selectedIndex = productAmount.SelectedIndex;
            //productAmount.DataBind();
            //if (selectedIndex!=-1)
            //{
            //    productAmount.SelectedIndex = selectedIndex;
            //} 
           


            List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
            if (orderDetails == null)
            {
                return;
            }
            int amount = 0;
            if (orderDetails.Count > 0)
            {
                amount = orderDetails?[e.Item.ItemIndex].amount ?? 1;//get amount from array
            }

            if (amount == 0)
            {
                amount = 1;//minimum amount value is 1
            }
            TextBox t = (TextBox)e.Item.FindControl("numOfProduct");//get amount 
            t.Attributes.Add("onchange", "return UpdateLabTestValue('" + t.ClientID.ToString() + "');");
            string StringCurrentAmount = t.Text;
            if (StringCurrentAmount != "")
            {
                int CurrentAmount = int.Parse(StringCurrentAmount);
                if (CurrentAmount != amount)
                {
                    orderDetails[e.Item.ItemIndex].amount = CurrentAmount;
                }
                t.Text = "" + CurrentAmount;//update  text
            }
            else
            {
                t.Text = "" + amount;//update  text
            }

            Session["orderDetails"] = orderDetails;

            //Label l = (Label)e.Item.FindControl("amount");//get amount label
            //l.Text = "" + amount;//update label text
        }

        protected void OrderNow_OnClick(object sender, EventArgs e)
        {
            if (IsGoodAmount())
            {
                List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                BLProduct product = BLProduct.GetProductById(orderDetails[0].productId);
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
            //if (IsGoodAmount())
            //{
            shoppingCartPanel.Visible = false;//to 'remove' shoppingCartPanel
            //}
            //else
            //{
            //    //msg!!!!!!!!!!!!!!!!!!!!
            //}



        }

        protected void shoppingCartB_OnClick(object sender, ImageClickEventArgs e)
        {
            shoppingCartPanel.Visible = true;//to 'show' shoppingCartPanel
        }

        public bool IsGoodAmount()
        {
            List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
            //ProductsCart.DataSource = orderDetails;
            //ProductsCart.DataBind();
            int sumAmount = 0;
            for (var index = 0; index < ProductsCart.Items.Count; index++)
            {
                DataListItem item = ProductsCart.Items[index];
                TextBox t = (TextBox)item.FindControl("numOfProduct");
                string numOfProduct = t.Text;
                if (numOfProduct != "")
                {
                    int amount = int.Parse(t.Text);
                    sumAmount += amount;
                    orderDetails[item.ItemIndex].amount = amount;
                }
                else
                {
                    t.Text = "1";
                }

            }

            if (sumAmount > 5)
            {
                return false;
            }
            Session["orderDetails"] = orderDetails;
            return true;
        }

        protected void EmptyCart_OnClick(object sender, ImageClickEventArgs e)
        {
            Update(null);
            EmptyCart.Visible = false;
        }

        //protected void numOfProduct_OnTextChanged(object sender, EventArgs e)
        //{
        //    if (!IsGoodAmount())
        //    {
        //        //msg!!!!!!!!!!! 
        //    }


        //}

        //protected void productAmount_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList productAmount = (DropDownList)sender;
        //    string StringIndex = productAmount.Attributes["Item.index"];
        //    int productIndex = int.Parse(StringIndex);
        //    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
        //    orderDetails[productIndex].amount = int.Parse(productAmount.Items[productAmount.SelectedIndex].ToString());
        //    Session["orderDetails"] = orderDetails;
        //}

        //protected void ProductsCart_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList productAmount = (DropDownList)sender;
        //    string StringIndex = productAmount.Attributes["Item.index"];
        //    int productIndex = int.Parse(StringIndex);
        //    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
        //    orderDetails[productIndex].amount = int.Parse(productAmount.Items[productAmount.SelectedIndex].ToString());
        //    Session["orderDetails"] = orderDetails;
        //}
    }
}