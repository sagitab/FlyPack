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
            if (!Page.IsPostBack)
            {
                //to gneratte grid view dynamically*************
                DataTable orders = BLOrderUser.GetOrders(user.Type, user.UserID, true, "");
                DataColumnCollection columns = orders.Columns;
                foreach (DataColumn column in columns)
                {
                    if (column.ColumnName != "ID")
                    {
                        //Declare the bound field and allocate memory for the bound field.
                        BoundField field = new BoundField();

                        //Initalize the DataField value.
                        field.DataField = column.ColumnName;

                        //Initialize the HeaderText field value.
                        field.HeaderText = column.ColumnName;

                        //Add the newly created bound field to the GridView.
                        OrderTable.Columns.Add(field);
                    }

                }
                int t = user.Type;
                if (t == 4)
                {
                    CommandField cf = new CommandField();
                    cf.ButtonType = ButtonType.Button;
                    cf.DeleteText = "cancel";
                    cf.ShowDeleteButton = true;

                    OrderTable.Columns.Add(cf);
                }
                else if (t == 3)
                {
                    ButtonField b = new ButtonField();
                    b.Text = "Start order";
                    b.ButtonType = ButtonType.Button;
                    b.CommandName = "updateArrivalTime";
                    OrderTable.Columns.Add(b);
                }
                else if (t == 1)
                {
                    ButtonField b = new ButtonField();
                    b.Text = "product ready to delivered";
                    b.ButtonType = ButtonType.Button;
                    b.CommandName = "updateReadyTime";
                    OrderTable.Columns.Add(b);
                }

                //BoundField b = new BoundField();
                //b.DataField=

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
                Dictionary<string, string> d = new Dictionary<string, string> { { "New orders", "N" }, { "Old orders", "O" } };
                NewOrOld.DataSource = d;
                //NewOrOld.DataTextField = "ShopName";
                //NewOrOld.DataValueField = ;
                // Bind the data to the control.
                NewOrOld.DataTextField = "Key";
                NewOrOld.DataValueField = "Value";
                NewOrOld.DataBind();

                // Set the default selected item, if desired.
                NewOrOld.SelectedIndex = 0;
            }

            int type = user.Type;
            UpOrders(user, "");

        }
        public void UpOrders(BLUser user, string condition)
        {
            int type = user.Type;
            DataTable orders = null;
            int index = NewOrOld.SelectedIndex;
            switch (NewOrOld.Items[index].Value)
            {
                case "N":
                    orders = BLOrderUser.GetOrders(type, user.UserID, true, condition);
                    break;
                default:
                    orders = BLOrderUser.GetOrders(type, user.UserID, false, condition);
                    break;
            }

            if (orders != null && orders.Rows.Count > 0)
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


            bool success = BLOrder.DeleteOrder(ID);
            if (success)
            {
                MSG.Text = "order cancel successfully";
                UpOrders(user, "");
                //Page_Load(sender, e);
            }
            else
            {
                MSG.Text = "Fail to cancel order ";
            }
        }

        protected void SearchOrderB_Click(object sender, EventArgs e)
        {
            BLUser user = (BLUser)Session["user"];
            string SearchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            string condition = "";
            string Value = serchedValue.Text;
            if (SearchBys == "ArrivalTime")
            {
                condition = $"AND (Orders.{SearchBys}=#{Value}#)";

            }
            else if (SearchBys == "OrderStatus")
            {
                condition = $"AND (Orders.{SearchBys}={Value})";

            }
            else if (SearchBys == "FirstName" && user.Type != 3)
            {
                condition = $"AND (Users_1.{SearchBys}='{Value}')";
            }
            //else if (SearchBys == "FirstName" && user.Type == 3)
            //{
            //    condition = $"AND (Users.{SearchBys}='{Value}')";
            //}
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

        protected void OrderTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable orders = (DataTable)OrderTable.DataSource;
            int orderID = int.Parse(orders.Rows[index]["ID"].ToString());
            int status = BLOrder.GetOrderStatus(orderID);
            if (e.CommandName == "updateArrivalTime"/*&& status==3*/)
            {


                DateTime ExitTime = DateTime.Now;
                DateTime AraivelTime = ExitTime.AddMinutes(20);//need to cuculate how match time
                bool succsess = BLOrder.UpdateArrivalTime(AraivelTime, orderID) /*&& BLOrder.UpdateStatus(status + 1, orderID)*/;
                if (!succsess)
                {
                    ErMSG.Text = "fail to start order";

                }
                else
                {
                    Button myButton = null;
                    GridViewRow row = OrderTable.Rows[index];
                    myButton = (Button)row.Cells[0].Controls[0];
                    myButton.Text = "Order started";
                    myButton.CommandName = "started";
                    UpOrders((BLUser)Session["user"], "");
                }
            }
            else if (e.CommandName == "updateReadyTime" /*&& status == 2*/)
            {

                DateTime ReadyTime = DateTime.Now;
               
                //get the order object 
                BLOrder order =BLOrder.GetBLOrderByID(orderID);
                //get the shop object 
                BLShop shop = BLShop.GetShopById(order.ShopID);
                //get the Id of the closest and . delivery  
                string MatchDeliveryID = BLUser.GetMatchesDeliveryID(shop.Possision);
                bool seccsess = BLOrder.UpdateReadyTime(ReadyTime, orderID)&&BLOrder.UpdateDelivery(orderID,MatchDeliveryID)/*&&BLOrder.UpdateStatus(status+1, orderID)*/;
                if (!seccsess)
                {
                    ErMSG.Text = "fail to update ready time";
                }
                else
                {
                    Button myButton = null;
                    GridViewRow row = OrderTable.Rows[index];

                    myButton = (Button)(row.Cells[4].Controls[0]);
                    myButton.Text = "Product Ready";
                    myButton.CommandName = "Ready";
                    UpOrders((BLUser)Session["user"], "");
                }
            }

        }

        protected void OrderTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow row = e.Row;
            //LinkButton myButton = row.FindControl("#ContentPlaceHolder1_OrderTable > tbody > tr:nth-child(2) > td:nth-child(5) > input[type=button]") as LinkButton;

            //if (myButton != null)
            //{
            //    myButton.Text = "Order started";
            //    myButton.CommandName = "started";
            //}

        }

        //protected void OrderTable_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    int index = e.NewEditIndex;
        //    OrderTable.EditIndex = index;
        //}

        //protected void OrderTable_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int index = e.RowIndex;
        //    DataTable orders = (DataTable)OrderTable.DataSource;
        //    int orderID = int.Parse(orders.Rows[index]["ID"].ToString());

        //    DateTime ExitTime = DateTime.Now;
        //    DateTime AraivelTime = ExitTime.AddMinutes(20);//need to cuculate how match time
        //    bool seccsess = BLOrder.UpdateArrivalTime(AraivelTime, orderID);
        //    if (!seccsess)
        //    {
        //        ErMSG.Text = "fail to start order";
        //    }
        //    else
        //    {
        //        UpOrders((BLUser)Session["user"], "");
        //    }
        //}

        //protected void OrderTable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    OrderTable.EditIndex = -1;
        //    UpOrders((BLUser)Session["user"], "");
        //}


    }
}