﻿using System;
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

            Validate();

        }
        public double GetLat(string LatLng)
        {
            string lat = "";
            for (int i = 0; i < LatLng.Length && LatLng[i] != ','; i++)//get the number from the start  until the ','
            {
                lat += LatLng[i];
            }

            return double.Parse(lat);
        }
        public double GetLng(string LatLng)
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

            /* int shopOrderId = int.Parse(ShopOrderID.Text);*/ //ShopDropDownList
            string address = Adress.Text;
            //string arriveTime = times.Items[times.SelectedIndex].Value.ToString();
            int shopId = int.Parse(ShopDropDownList.SelectedValue);
            //DateTime arriveDateTime = DateTime.Parse(arriveTime);
            int numOfFloor = 0;
            try
            {
                numOfFloor = int.Parse(NumOfFloor.Text);//try to get num of floor
            }
            catch
            {
                MSG.Text = "Please enter num Of Floor";//massage
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
                    MSG.Text = order.OrderId != -1 ? "order successes!!" : "order failed :(";//fail/success massage
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