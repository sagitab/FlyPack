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
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
                Session["user"] = new BlUser("hoohoo12");
            }
            Validate();
        }
        public static double GetLat(string LatLng)
        {
            string lat = "";
            for (int i = 0; i < LatLng.Length && LatLng[i] != ','; i++)//get the number from the start  until the ','
            {
                lat += LatLng[i];
            }

            return double.Parse(lat);
        }
        public static double GetLng(string LatLng)
        {
            string lng = "";
            int start = LatLng.IndexOf(',');
            for (int i = start + 1; i < LatLng.Length; i++)//get the number from ',' to the and of the string
            {
                lng += LatLng[i];
            }

            return double.Parse(lng);
        }
        protected void OrderB_Click(object sender, EventArgs e)
        {
            //get input values

            string address = Adress.Text;
            int shopId = int.Parse(Request.QueryString.Get("shopId"));
            int numOfFloor = 0;
            try
            {
                numOfFloor = int.Parse(NumOfFloor.Text);//try to get num of floor
            }
            catch
            {
                MSG.Text = "Please enter num Of Floor";//massage
                return;
            }
            string latLng = this.LatLng.Value.ToString();
            double lat = GetLat(latLng);
            double lng = GetLng(latLng);
            BlUser user = (BlUser)Session["user"];
            bool IsNotNewAddress = lat == 1.0 && lng == 1.0 && address == "";
            if (IsNotNewAddress)//if IsNotNewAddress the address is the user address from DB
            {
                Point UserLocation = user.Location;
                lat = UserLocation.Lat;
                lng = UserLocation.Lng;
            }

            if (numOfFloor != 0 && Page.IsValid)//if the validator is valid and numOfFloor is valid add order
            {
                try
                {
                    BlOrder order = new BlOrder(user.UserId, "111111111", shopId, new DateTime(2000, 1, 1, 1, 1, 1), new DateTime(2000, 1, 1, 1, 1, 1), 1, lat, lng, numOfFloor);//add order
                    //update order details
                    bool success = order.OrderId != -1;
                    List<BLProduct> productsCart = (List<BLProduct>)Session["productsCart"];
                    int[] productAmounts = (int[])Session["productAmount"];
                    if (success)
                    {
                        success= BLOrderDetailsDB.UpdateOrderDetails(productsCart, order.OrderId, productAmounts);
                    }
                    MSG.Text = success ? "order successes!!" : "order failed :(";//fail/success massage
                    BlOrderUser customer=new BlOrderUser(order.CustomerId);
                    //get  description of the product list to send in email
                    string prodactsString = BLProduct.GetProductString(productsCart, productAmounts);
                    
                    bool isEmailSent = Register.sendEmail(customer.Email, " Fly pack order summery ",
                        $"Hi,{customer} your order received and we take care your order.here a summery of your  order- <br/>{prodactsString}<br/>Have a nice day,The Fly Pack Team");
                    if (!isEmailSent)
                    {
                        //take care if email dont send
                    }
                    BlOrderUser shopManager = new BlOrderUser(order.CustomerId);
                    bool isEmailSentToM = Register.sendEmail(shopManager.Email, " Fly pack a customer order from your shop!!",
                        $"Hi,{shopManager} please update when your products will be ready to be delivered.here a summery of your  order- <br/>{prodactsString}<br/ Have a nice day,The Fly Pack Team");
                    if (!isEmailSentToM)
                    {
                        //take care if email dont send
                    }
                }
                catch (Exception exception)
                {
                    MSG.Text = "fail to add order because-" + exception.Message;//fail massage
                }
            }
            else
            {
                NumOfFloor.CssClass = NumOfFloorValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";//convert the the bob style by the validator
            }
        }
    }
}