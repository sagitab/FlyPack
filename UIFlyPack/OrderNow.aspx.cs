using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
namespace UIFlyPack
{
    public partial class OrderNow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //DateTime time = new DateTime(2020, 9, 9, 1, 30, 0);
                //times.Items[0].Value = time.ToString() ;
                //set data source
                ShopDropDownList.DataSource = BLFlyPack.BlShop.GetShops();
                ShopDropDownList.DataTextField = "ShopName";
                ShopDropDownList.DataValueField = "ID";
                // Bind the data to the control.
                ShopDropDownList.DataBind();

                // Set the default selected item, if desired.
                ShopDropDownList.SelectedIndex = 0;
            }
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;


        }
        public double GetLat(string latlng)
        {
            string lat = "";
            for (int i = 0; i < latlng.Length && latlng[i] != ','; i++)
            {
                lat += latlng[i];
            }

            return double.Parse(lat);
        }
        public double GetLng(string latlng)
        {
            string lng = "";
            int start = latlng.IndexOf(',');
            for (int i = start + 1; i < latlng.Length; i++)
            {
                lng += latlng[i];
            }

            return double.Parse(lng);
        }
        protected void OrderB_Click(object sender, EventArgs e)
        {
            int shopOrderId = int.Parse(ShopOrderID.Text);//ShopDropDownList
            string address = Adress.Text;
            //string arriveTime = times.Items[times.SelectedIndex].Value.ToString();
            int shopId = int.Parse(ShopDropDownList.SelectedValue);
            //DateTime ariveDateTime = DateTime.Parse(arriveTime);
            int numOfFloor = int.Parse(NumOfFloor.Text);

            string latLng = this.LatLng.Value.ToString();
            double lat = GetLat(latLng);
            double lng = GetLng(latLng);
            BlUser user = (BlUser)Session["user"];
            try
            {
                BlOrder order = new BlOrder(user.UserId, "111111111", shopId, new DateTime(2000, 1, 1, 1, 1, 1), new DateTime(2000, 1, 1, 1, 1, 1), 1, lat, lng, numOfFloor);
                if (order!=null&&order.OrderId!=-1)
                {
                    MSG.Text = "order successes!!";
                }
                else
                {
                    MSG.Text = "order failed :(";
                }
             
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
    }
}