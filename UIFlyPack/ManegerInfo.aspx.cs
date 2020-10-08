using BLFlyPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace UIFlyPack
{
    public partial class ManagerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            if(!Page.IsPostBack)
            {
                DataTable customes = user.CustomersTable();
                DataTable deliveries = user.DeliveriesTable();
                ErDelivery.Text = !BindTable(deliveries, DeliveriesTable) ? "fail show deliveries table" : "";
                ErCustomer.Text = !BindTable(customes, CustomersTable) ? "fail show customers table" : "";
                NumOfOrders.Text ="Number of orders- "+ user.GetNumOfOrders()+"Number of customers that order- "+user.GetNumOfActiveCustomers();
            }
        }

        protected void SearchCustomerB_Click(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            string searchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            
            string value = serchedValue.Text;
            DataTable customers = user.CustomersSearch($"(Users.{searchBys}='{value}')");
            bool isExsist= BindTable(customers, CustomersTable);
            if(!isExsist)
            {
                ErCustomer.Text = "Not valid search value";
            }
            else
            {
                ErCustomer.Text = "";
            }
        }
        public bool BindTable(DataTable table,GridView gridView)
        {
            bool success = table != null&&table.Rows.Count>0;
            if (!success) return false;
            gridView.DataSource = table;
            gridView.DataBind();
            return true;
        }
    }
}