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
                if (!Page.IsPostBack)
                {
                    if (!UpdateData(BLProduct.GetAllProducts("")))//Update Data to all products
                    {
                        MSG.Text = "there is no products"; //error msg
                    }
                }
                else
                {
                    UpdateData((List<BLProduct>)Session["products"]);//Update Data to the product in session
                }
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
                const int maxOfProductPerOrder = 6;//num of max products to deliver
                BLProduct product = ((List<BLProduct>)Session["products"])[e.Item.ItemIndex];
                try
                {
                    //get data from session
                    List <BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                    int[] productAmounts = (int[])Session["productAmount"];
                    //check if session nul and create new object
                    if (productsCart==null)
                    {
                        productsCart=new List<BLProduct>();
                    }
                    if (productAmounts == null)
                    {
                        productAmounts = new int[6];
                    }
                    //check if order is full
                    if (productsCart.Count == maxOfProductPerOrder || BLProduct.SumArr(productAmounts) == maxOfProductPerOrder)
                    {
                        addToCartMsg.Text = "You can order up to 6 product";
                    }
                    else
                    {
                        //get product index
                        int indexOfProduct = BLProduct.IndexOfProduct(productsCart, product);
                        if (indexOfProduct != -1)
                        {//if product already in the productsCart update amount
                            productAmounts[indexOfProduct]++;
                            Session["productsCart"] = productsCart;
                            Session["productAmount"] = productAmounts;
                            Response.Redirect("Store.aspx?errorMSG=false");
                        }
                        else
                        {
                            //if product not in the productsCart added him to there
                            productsCart.Add(product);
                            if (Session["numOfProducts"] == null)
                            {
                                productAmounts[0] = 1;
                                Session["numOfProducts"] = 1;
                            }
                            else
                            {
                                int numOfProducts = (int)Session["numOfProducts"];
                                productAmounts[numOfProducts] = 1;
                                Session["numOfProducts"] = numOfProducts + 1;
                            }
                            //update sessions
                            Session["productAmount"] = productAmounts;
                            Session["productsCart"] = productsCart;
                            //Redirect to the same page to activate master page page loud to update the data in the data list
                            Response.Redirect($"Store.aspx?errorMSG=true");
                        }
                    }

                  

                }
                catch
                {
                    //Session["productsCart"] = new List<BLProduct>() { product };
                    //int[] amountArr = new int[maxOfProductPerOrder - 1];
                    //amountArr[0] = 1;
                    //Session["productAmount"] = amountArr;
                    MSG.Text = "error!!! :("; //error msg
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