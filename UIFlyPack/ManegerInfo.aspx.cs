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
    public partial class ManegerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLUser user = (BLUser)Session["user"];
            if(!Page.IsPostBack)
            {
                DataTable Customes = user.CustomersTable();
                DataTable Deliveries = user.DeliveriesTable();
                ErDelivery.Text = !BindTable(Deliveries, DeliveriesTable) ? "fail show deliveries table" : "";
                ErCustomer.Text = !BindTable(Customes, CustomersTable) ? "fail show customers table" : "";
                NumOfOrders.Text ="Number of orders- "+ user.GetNumOfOrders()+"Number of customers that order- "+user.GetNumOfActiveCustomers();
            }
        }

        protected void SearchCustomerB_Click(object sender, EventArgs e)
        {
            BLUser user = (BLUser)Session["user"];
            string SearchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            
            string Value = serchedValue.Text;
            DataTable Customes = user.CustomersSearch($"(Users.{SearchBys}='{Value}')");
            bool isExsist= BindTable(Customes, CustomersTable);
            if(isExsist)
            {
                ErCustomer.Text = "Not valid search value";
            }
        }
        public bool BindTable(DataTable table,GridView gridView)
        {
            bool succsess = table != null&&table.Rows.Count>0;
            if(succsess)
            {
                gridView.DataSource = table;
                gridView.DataBind();
                return true;
            }
            return false;
        }
    }
}