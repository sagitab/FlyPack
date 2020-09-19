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
                if (!BindTable(Deliveries, DeliveriesTable))
                {
                    ErMSG.Text = "fail show deliveries table";
                }
                if (!BindTable(Customes, CustomersTable))
                {
                    ErMSG.Text = "fail show custimers table";
                }
                NumOfOrders.Text ="Number of orders- "+ user.GetNumOfOrders()+"Number of customers that order- "+user.GetNumOfActiveCustomers();
            }
        }

        protected void SearchCustomerB_Click(object sender, EventArgs e)
        {
            BLUser user = (BLUser)Session["user"];
            string SearchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            
            string Value = serchedValue.Text;
            DataTable Customes = user.CustomersSerch($"(Users.{SearchBys}='{Value}')");
            bool isExsist= BindTable(Customes, CustomersTable);
            if(isExsist)
            {
                ErMSG.Text = "Not valied search value";
            }
        }
        public bool BindTable(DataTable table,GridView gridView)
        {
            bool seccsess = table != null&&table.Rows.Count>0;
            if(seccsess)
            {
                gridView.DataSource = table;
                gridView.DataBind();
                ErMSG.Text = "";
                return true;
            }
            return false;
        }
    }
}