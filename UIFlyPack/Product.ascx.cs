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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UpdateData(BLProduct.GetAllProducts("")))
                {
                    MSG.Text = "there is no products"; //error msg
                }
            }
            else
            {
                UpdateData((List<BLProduct>)Session["products"]);
            }
        }

        public bool UpdateData(List<BLProduct> products)
        {
            if (products == null || products.Count == 0)
            {
                return false;
            }
            ProductsList.DataSource = products;
            Session["products"] = products;
            ProductsList.DataBind();
            return true;
        }



        public static string setOrderBy(int orderBy)
        {
            string[] OrderByArr = { "Price DESC", "Price ACS", "Description DESC", "Description ACS", "ID" };
            return " ORDER BY " + OrderByArr[orderBy - 1];
        }
        public static List<BLProduct> Search(string SearchVal, int shopId, bool isSearchName, int orderBy)
        {


            string condition = "";
            List<BLProduct> products = null;
            if (shopId == -1)
            {
                if (isSearchName)
                {
                    condition = $"WHERE Description='{SearchVal}'";
                }
                else
                {
                    condition = "WHERE Price=" + SearchVal;
                }
                condition += setOrderBy(orderBy);
                products = BLProduct.GetAllProducts(condition);
            }
            else
            {
                if (isSearchName)
                {
                    condition = $"AND Description='{SearchVal}'";
                }
                else
                {
                    condition = "AND Price=" + SearchVal;
                }
                condition += setOrderBy(orderBy);
                products = BLProduct.GetAllProductsByShopId(shopId, condition);
            }

            if (products == null || products.Count == 0)
            {
                return null;
            }

            return products;
        }
        public static int SumArr(int[] arr)
        {
            int sum = 0;
            foreach (var t in arr)
            {
                sum += t;
            }
            return sum;
        }
        protected void ProductsList_OnItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "AddToCart")
            {
                const int maxOfProductPerOrder = 6;
                BLProduct product = ((List<BLProduct>)Session["products"])[e.Item.ItemIndex];
                try
                {
                    List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                    int[] productAmounts = (int[])Session["productAmount"];
                    if (productsCart.Count == maxOfProductPerOrder || SumArr(productAmounts) == maxOfProductPerOrder)
                    {
                        MSG.Text = "You can order up to 6 product";
                    }
                    if (productsCart.Contains(product))
                    {
                        productAmounts[productsCart.IndexOf(product)]++;
                    }
                    else
                    {
                        productsCart.Add(product);
                    }

                }
                catch
                {
                    Session["productsCart"] = new List<BLProduct>() { product };
                    int[] amountArr = new int[maxOfProductPerOrder - 1];
                    amountArr[0] = 1;
                    Session["productAmount"] = amountArr;
                }
            }
        }
    }
}