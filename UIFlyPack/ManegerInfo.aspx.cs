using BLFlyPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Helpers;
using  System.Web.Helpers.AntiXsrf;
namespace UIFlyPack
{
    public partial class ManagerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            BlOrderUser orderUser = null;
            try
            {
                orderUser = (BlOrderUser)Session["user"];
            }
            catch 
            {
                Response.Redirect("HomePage.aspx");
                return;
            }

            //take care of all the options of user BlOrderUser/BlShopManager/null
            if (user is BlOrderUser User)
            {
                orderUser  = User;

            }
            if (user is BlShopManager shopManager)
            {
                orderUser = shopManager;

            }

            if (orderUser==null)
            {
                Response.Redirect("HomePage.aspx");
                return;
            }
            if (Page.IsPostBack) return;
            //get data 
            DataTable customers = orderUser.CustomersTable();
            DataTable deliveries = orderUser.DeliveriesTable();
            ErDelivery.Text = !BindTable(deliveries, DeliveriesTable) ? "fail show deliveries table" : "";//error massage
            ErCustomer.Text = !BindTable(customers, CustomersTable) ? "fail show customers table" : "";//error massage
            //set the labels information
            NumOfOrders.Text ="Number of orders- "+ orderUser.GetNumOfOrders();
            NumOfCustomers.Text="Number of customers- " + customers.Rows.Count; 
        }

        protected void SearchCustomerB_Click(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            //get input values
            string searchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            string value = serchedValue.Text;
            //get data
            DataTable customers = user.CustomersSearch($"(Users.{searchBys}='{value}')");
            bool isExist= BindTable(customers, CustomersTable);
            ErCustomer.Text = !isExist ? "Not valid search value" : "";//error massage
        }
        public bool BindTable(DataTable table,GridView gridView)// check data table and up-loud the data if return true 
        {
            bool success = table != null&&table.Rows.Count>0;
            if (!success) return false;
            gridView.DataSource = table;
            gridView.DataBind();
            return true;
        }

        protected void deliverSearchB_OnClick(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            //get input values
            string searchBys = DeliverSearchBy.Items[DeliverSearchBy.SelectedIndex].Value;
            string value = DeliverSearchVal.Text;
            //get data
            DataTable customers = user.DeliversSearch($"(Users.{searchBys}='{value}')");
            bool isExist = BindTable(customers, DeliveriesTable);
            DeliverMSG.Text = !isExist ? "Not valid search value" : "";//error massage
        }
    }
}