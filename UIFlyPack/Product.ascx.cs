using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class Product : System.Web.UI.UserControl
    {
        private List<BLProduct> _productsList;
        public List<BLProduct> ProductsCollections
        {
            get => _productsList;
            set
            {
                _productsList = value;
                UpdateData(value);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateData((List<BLProduct>)Session["products"]);//Update Data to the product in session
            }
            catch (Exception exception)
            {
                MSG.Text = exception.Message;//error msg
            }

        }
        /// <summary>
        /// update the ProductsList data
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public bool UpdateData(List<BLProduct> products)
        {
            if (products == null || products.Count == 0)
            {
                ProductsList.DataSource = products;
                Session["products"] = products;
                ProductsList.DataBind();
                MSG.Text = "there is no products"; //error msg
                return false;
            }
            ProductsList.DataSource = products;
            Session["products"] = products;
            ProductsList.DataBind();
            MSG.Text = "";
            return true;
        }
        protected void ProductsList_OnItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                int maxOfProductPerOrder = GlobalVariable.MaxOrderForDeliver;//num of max products to deliver
                BLProduct product = ((List<BLProduct>)Session["products"])[e.Item.ItemIndex];
                try
                {
                    //get data from session
                    List<BLOrderDetailsDB> orderDetails = (List<BLOrderDetailsDB>)Session["orderDetails"];
                    if (orderDetails == null)
                    {
                        orderDetails = new List<BLOrderDetailsDB>();
                    }
                    //check if order is full
                    if (orderDetails.Count == maxOfProductPerOrder || BLOrderDetailsDB.IsFull(orderDetails))
                    {
                        addToCartMsg.Text = "You can order up to 6 product";
                    }
                    else
                    {
                        //get product index
                        int indexOfProduct = BLOrderDetailsDB.IndexOfProduct(orderDetails, product.Id);
                        if (indexOfProduct != -1)
                        {
                            //if product already in the productsCart update amount
                            orderDetails[indexOfProduct].amount++;
                            //update session
                            Session["orderDetails"] = orderDetails;
                            addToCartMsg.Text = "you have " + orderDetails[indexOfProduct].amount + " " + product.Description + " in your cart";
                        }
                        else
                        {
                            //if product not in the productsCart added him to there
                            orderDetails.Add(new BLOrderDetailsDB(product.Id, 1, product.Price));
                            //update sessions
                            Session["orderDetails"] = orderDetails;
                            //Redirect to the same page to activate master page page loud to update the data in the data list
                            addToCartMsg.Text = product.Description + " added to your cart";
                            //Response.Redirect($"Store.aspx?productName=" + );
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    MSG.Text = "fail to add product to your cart :(" + exception.Message; //error msg
                }
            }
        }

        //protected void OnClick(object sender, EventArgs e)
        //{
        //    BLProduct product = ((List<BLProduct>)Session["products"])[e.];
        //}

        //protected void ProductsList_OnItemDataBound(object sender, DataListItemEventArgs e)
        //{
        //    //BLProduct product = ((List<BLProduct>)Session["products"])[e.Item.ItemIndex];
        //    //if (e.GetType() is  Button)
        //    //{
        //    //    e.
        //    //}
        //}
    }
}