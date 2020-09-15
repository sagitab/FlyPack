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
    public partial class ViewOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              if (!Page.IsPostBack)
            {
                //if(type==4)
                //{
                //    //CommandField cf = new CommandField();
                //    //cf.ButtonType = ButtonType.Button;
                //    //cf.DeleteText = "c";
                //    //cf.ShowDeleteButton = true;
                //    //OrderTable.Columns.Add(cf);
                //    OrderTable.Columns[4].AccessibleHeaderText = "c";
                //}    
                //set data source
                Dictionary<string, string> d = new Dictionary<string, string> { {"New orders","N" },{ "Old orders", "O" } };
                NewOrOld.DataSource =d ;
                //NewOrOld.DataTextField = "ShopName";
                //NewOrOld.DataValueField = ;
                // Bind the data to the control.
                NewOrOld.DataTextField = "Key";
                NewOrOld.DataValueField = "Value" ;
                NewOrOld.DataBind();

                // Set the default selected item, if desired.
                NewOrOld.SelectedIndex = 0;
            }
            BLUser user = (BLUser)Session["user"];
            int type = user.Type;
            UpOrders(user,"");
          
        }
        public void UpOrders(BLUser user,string condition)
        {
            int type = user.Type;
            DataTable orders = null;
            int index = NewOrOld.SelectedIndex;
            if (NewOrOld.Items[index].Value=="N")
            {
                orders = BLOrderUser.GetOrders(type, user.UserID, true,condition);
            }
            else
            {
                orders = BLOrderUser.GetOrders(type, user.UserID, false,condition);
            }
          
            if(orders!=null&&orders.Rows.Count>0)
            {
                OrderTable.DataSource = orders;
                OrderTable.DataBind();
                ErMSG.Text = "";
                OrderTable.Visible = true;
            }
            else
            {
                OrderTable.Visible = false;
                ErMSG.Text = "there is no orders";
            }
          
        }
        protected void OrderTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;
            //GridViewRow d = OrderTable.Rows[index];
            //string t = d.Cells[0].Text;
            //int id = int.Parse(t);
            BLUser user = (BLUser)Session["user"];
            int type = user.Type;
            DataTable orders = (DataTable)OrderTable.DataSource;
            //if (NewOrOld.Items[NewOrOld.SelectedIndex].Value == "N")
            //{
            //    orders = BLOrderUser.GetOrders(type, user.UserID, true,"");
            //}
            //else
            //{
            //    orders = BLOrderUser.GetOrders(type, user.UserID, false,"");
            //}

            int ID = (int)orders.Rows[index]["ID"];
           

            bool seccces = BLOrder.DeleteOrder(ID);
            if (seccces)
            {
                MSG.Text = "order cencel seccsessfuly";
                UpOrders(user, "");
            }
            else
            {
                MSG.Text = "Fail to cencel order ";
            }
        }

        protected void SearchOrderB_Click(object sender, EventArgs e)
        {
            string SearchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            string condition = "";
            string Value = serchedValue.Text;
            if (SearchBys == "ArrivalTime")
            {
                condition = $"AND (Orders.{SearchBys}=#{Value}#)";

            }
            else if ( SearchBys == "OrderStutus")
            {
                condition = $"AND (Orders.{SearchBys}={Value})";

            }
            else if (SearchBys == "FirstName")
            {
                condition = $"AND (Users_1.{SearchBys}='{Value}')";
            }
            else
            {
                condition = $"AND (Shops.{SearchBys}='{Value}')";
            }
            UpOrders((BLUser)Session["user"], condition);

        }
        protected void NewOrOld_Click(object sender, EventArgs e)
        {
            UpOrders((BLUser)Session["user"], "");

        }
    }
}