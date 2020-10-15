using BLFlyPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mime;

namespace UIFlyPack
{
    public partial class ViewOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["user"] = (BlUser)new BlShopManager("12345678");//del
            BlUser user = (BlUser)Session["user"];
            if (!Page.IsPostBack)
            {
                //BlOrderUser orderUser=new BlOrderUser(user.Password);
                // generate grid view dynamically
                DataTable orders = null;
                if (user is BlOrderUser blOrderUser)
                {
                    orders = blOrderUser.GetOrders(true, "");
                }
                DataColumnCollection columns = orders.Columns;
                foreach (DataColumn column in columns)
                {
                    if (column.ColumnName != "ID")
                    {
                        //Declare the bound field and allocate memory for the bound field.
                        BoundField field = new BoundField();

                        //Initialize the DataField value.
                        field.DataField = column.ColumnName;

                        //Initialize the HeaderText field value.
                        field.HeaderText = column.ColumnName;

                        //Add the newly created bound field to the GridView.
                        OrderTable.Columns.Add(field);
                    }

                }
                int type = user.Type;
                switch (type)
                {
                    case 4:
                    {
                        CommandField cf = new CommandField
                        {
                            ButtonType = ButtonType.Button, DeleteText = "cancel", ShowDeleteButton = true
                        };

                        OrderTable.Columns.Add(cf);
                        break;
                    }
                    case 3:
                    {
                        ButtonField b = new ButtonField
                        {
                            Text = "Start order", ButtonType = ButtonType.Button, CommandName = "updateArrivalTime"
                        };
                        OrderTable.Columns.Add(b);
                        break;
                    }
                    case 1:
                    {
                        ButtonField b = new ButtonField
                        {
                            Text = "product ready to delivered",
                            ButtonType = ButtonType.Button,
                            CommandName = "updateReadyTime"
                        };
                        OrderTable.Columns.Add(b);
                        break;
                    }
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

            int userType = user.Type;
            UpOrders(user, "");
            int length = OrderTable.Rows.Count;
            switch (userType)
            {

                case 3:
                {
                    for (int i = 0; i < length; i++)
                    {
                        GridViewRow row = OrderTable.Rows[i];
                        TableCell cell = row.Cells[1];
                        if (cell.Text == "delivery take care your order")
                        {
                            Button myButton = null;
                            myButton = (Button)row.Cells[row.Cells.Count-1].Controls[0];
                            myButton.Text = "Order Finished";
                            myButton.CommandName = "finish";
                        }
                    }

                    break;
                }
                case 1:
                {
                    
                    for (int i = 0; i < length; i++)
                    {
                        GridViewRow row = OrderTable.Rows[i];
                        TableCell cell = row.Cells[1];
                        if (cell.Text == "shipping time selected")
                        {
                            Button myButton = null;
                            myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                            myButton.Text = "Ready time selected";
                            myButton.CommandName = "";
                        }
                    }

                    break;
                }
            }

        }
        public void UpOrders(BlUser user, string condition)
        {
            int type = user.Type;
            DataTable orders = null;
            int index = NewOrOld.SelectedIndex;
            BlOrderUser orderUser = new BlOrderUser(user.Password);
            switch (NewOrOld.Items[index].Value)
            {
                case "N":
                    OrderTable.Columns[OrderTable.Columns.Count - 1].Visible = true;
                    try
                    {
                        orders = orderUser.GetOrders(true, condition);
                    }
                    catch (Exception e)
                    {
                        ErMSG.Text = "fail"+e.Message;
                    }
                   
                    break;
                default:
                    OrderTable.Columns[OrderTable.Columns.Count - 1].Visible = false;
                    try
                    {
                        orders = orderUser.GetOrders(false, condition);
                    }
                    catch (Exception e)
                    {
                        ErMSG.Text = "fail " + e.Message;
                    }
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
            BlUser user = (BlUser)Session["user"];
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

            int orderId = (int)orders.Rows[index]["ID"];
            int status = BlOrder.GetOrderStatus(orderId);
            bool success = false;
            try
            {
                success = BlOrder.DeleteOrder(orderId) && status < 4;
            }
            catch (Exception exception)
            {
                ErMSG.Text = "fail cancel order " + exception.Message;
            }
            if (success)
            {
                MSG.Text = "order cancel successfully";
               Response.Redirect("ViewOrders.aspx");
                //Page_Load(sender, e);
            }
            else
            {
                MSG.Text = "Fail to cancel order ";
            }
        }

        protected void SearchOrderB_Click(object sender, EventArgs e)
        {
            BlUser user = (BlUser)Session["user"];
            //get input values
            string searchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            string condition = "";
            string value = serchedValue.Text;
           
            if (searchBys == "ArrivalTime")
            {
                condition = $"AND (Orders.{searchBys}=#{value}#)";

            }
            else if (searchBys == "OrderStutus")
            {
                if (int.TryParse(value,out var status))
                {
                    condition = $"AND (Orders.OrderStutus={status})";
                }
                else
                {
                    Dictionary<int, string> stautus = new Dictionary<int, string> { { 1, "order sent" }, { 2, "shop take care your order" }, { 3, "shipping time selected" }, { 4, "delivery take care your order" }, { 5, "order shipped" } };
                    condition = $"AND (Orders.OrderStutus={stautus.FirstOrDefault(x => x.Value == value).Key})";
                }
            }
            else if (searchBys == "FirstName" && user.Type != 3)
            {
                condition = $"AND (Users_1.{searchBys}='{value}')";
            }
            //else if (SearchBys == "FirstName" && user.Type == 3)
            //{
            //    condition = $"AND (Users.{SearchBys}='{Value}')";
            //}
            else
            {
                condition = $"AND (Shops.{searchBys}='{value}')";
            }
            UpOrders((BlUser)Session["user"], condition);

        }
        protected void NewOrOld_Click(object sender, EventArgs e)
        {
            UpOrders((BlUser)Session["user"], "");

        }

        protected void OrderTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable orders = (DataTable)OrderTable.DataSource;
            int orderId = int.Parse(orders.Rows[index]["ID"].ToString());
            int status = BlOrder.GetOrderStatus(orderId);
            if (e.CommandName == "updateArrivalTime"/*&& status==3*/)
            {


                DateTime exitTime = DateTime.Now;
                DateTime araivelTime = exitTime.AddMinutes(20);//need to calculate how match time with GetDistanceToCustomerHome()
                bool success = BlOrder.UpdateArrivalTime(araivelTime, orderId) /*&& BLOrder.UpdateStatus(status + 1, orderID)*/;
                if (!success)
                {
                    ErMSG.Text = "fail to start order ";

                }
                else
                {
                    //Button myButton = null;
                    //GridViewRow row = OrderTable.Rows[index];
                    //myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                    //myButton.Text = "Order started";
                    //myButton.CommandName = "started";
                    UpOrders((BlUser)Session["user"], "");
                }
            }
            else if (e.CommandName == "updateReadyTime" /*&& status == 2*/)
            {

                DateTime readyTime = DateTime.Now;
               
                //get the order object 
                BlOrder order =BlOrder.GetBlOrderById(orderId);
                //get the shop object 
                BlShop shop = BlShop.GetShopById(order.ShopId);
                //get the Id of the closest and . delivery  
                string matchDeliveryId = BlUser.GetMatchesDeliveryId(shop.Location);
                bool success = BlOrder.UpdateReadyTime(readyTime, orderId)&&BlOrder.UpdateDelivery(orderId,matchDeliveryId)/*&&BLOrder.UpdateStatus(status+1, orderID)*/;
                if (!success)
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
                    UpOrders((BlUser)Session["user"], "");
                }
            }
            else if (e.CommandName == "finish")
            {
                if (/*BlOrder.UpdateStatus(status + 1, orderId)&&*/status==4)
                {
                    NewOrOld.SelectedIndex = 1;
                    UpOrders((BlUser)Session["user"], "");
                }
                else
                {
                    ErMSG.Text = "fail to update status";
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