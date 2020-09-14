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

            BLUser user = (BLUser)Session["user"];
            int type = user.Type;
            UpOrders(user);
            if (!Page.IsPostBack)
            {
                if(type==4)
                {
                    //CommandField cf = new CommandField();
                    //cf.ButtonType = ButtonType.Button;
                    //cf.DeleteText = "c";
                    //cf.ShowDeleteButton = true;
                    //OrderTable.Columns.Add(cf);
                    OrderTable.Columns[4].AccessibleHeaderText = "c";
                }
            }
        }
        public void UpOrders(BLUser user)
        {
            int type = user.Type;
            DataTable orders= BLOrderUser.GetOrders(type, user.UserID);
            if(orders!=null&&orders.Rows.Count>0)
            {
                OrderTable.DataSource = orders;
                OrderTable.DataBind();
            }
            else
            {
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
            DataTable table = BLOrderUser.GetOrders(type, user.UserID);
           
            int ID = (int)table.Rows[index]["ID"];
           

            bool seccces = BLOrder.DeleteOrder(ID);
            if (seccces)
            {


                UpOrders(user);
                ErMSG.Text = "order cencel seccsessfuly";

            }
            else
            {
                ErMSG.Text = "eror ";
            }
        }
    }
}