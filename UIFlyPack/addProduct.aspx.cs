using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;

namespace UIFlyPack
{
    public partial class addProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }


        protected void AddProduct_OnClick(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload.PostedFile;
            //Extract Image File Name.
            string name = Path.GetFileName(postedFile.FileName);
            string data = Path.GetExtension(postedFile.FileName);
            string type = data.ToLower();
            bool fileIsPic = (type == ".png" || type == ".gif" || type == ".bmp" || type == ".jpg");
            if (Page.IsValid&& fileIsPic)
            {
                BlShopManager user = (BlShopManager)Session["user"];
                string productName = ProductName.Text;
                double productPrice = double.Parse(ProductPrice.Text);
                int shopId = user.ShopId;


                try
                {
                    string path = name;
                    //Save the Image File in Folder.
                    postedFile.SaveAs(Server.MapPath(path));
                    BLFlyPack.BLProduct product = new BLProduct(productPrice, productName, shopId, -1, path);
                    MSG.Text = product.Id != -1 ? "product added !!" : "product added failed";
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
             
             
            }
            else
            {
                MSG.Text = "Please check if file is picture or all other field are valid";
            }
        }
    }
}