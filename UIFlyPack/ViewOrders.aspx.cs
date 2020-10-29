using BLFlyPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                    orders = blOrderUser.GetOrders(true, "");//get orders table
                }


                if (orders != null)
                {
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
                }



                int type = user.Type;
                switch (type)//add command fields by user type
                {
                    case 4:
                        {
                            CommandField cf = new CommandField
                            {
                                ButtonType = ButtonType.Button,
                                DeleteText = "cancel",
                                ShowDeleteButton = true
                            };

                            OrderTable.Columns.Add(cf);
                            break;
                        }
                    case 3:
                        {
                            ButtonField b = new ButtonField
                            {
                                Text = "Start order",
                                ButtonType = ButtonType.Button,
                                CommandName = "updateArrivalTime"
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

            int userType = user.Type;//get user type 
            UpOrders(user, "");
            int length = OrderTable.Rows.Count;
            switch (userType)//convert button values by status and user type
            {

                case 3:
                    {
                        for (int i = 0; i < length; i++)
                        {
                            GridViewRow row = OrderTable.Rows[i];
                            TableCell cell = row.Cells[1];
                            if (cell.Text != "delivery take care your order") continue;
                            Button myButton = null;
                            myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                            myButton.Text = "Order Finished";
                            myButton.CommandName = "finish";
                        }

                        break;
                    }
                case 1:
                    {

                        for (int i = 0; i < length; i++)
                        {
                            GridViewRow row = OrderTable.Rows[i];
                            TableCell cell = row.Cells[1];
                            if (cell.Text != "shipping time selected") continue;
                            var myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                            myButton.Text = "Ready time selected";
                            myButton.CommandName = "";
                        }

                        break;
                    }
            }

        }
        public void UpOrders(BlUser user, string condition)
        {
            DataTable orders = null;
            int index = NewOrOld.SelectedIndex;
            BlOrderUser orderUser = new BlOrderUser(user.Password);
            switch (NewOrOld.Items[index].Value)//get order table by selected option
            {
                case "N":
                    OrderTable.Columns[OrderTable.Columns.Count - 1].Visible = true;
                    try
                    {
                        orders = orderUser.GetOrders(true, condition);
                    }
                    catch (Exception e)
                    {
                        ErMSG.Text = "fail" + e.Message;
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

            if (orders != null && orders.Rows.Count > 0)//if there is orders
            {
                //show table
                OrderTable.DataSource = orders;
                OrderTable.DataBind();
                ErMSG.Text = "";
                OrderTable.Visible = true;
            }
            else
            {
                //hide table
                OrderTable.Visible = false;
                ErMSG.Text = "there is no orders";
            }

        }
        protected void OrderTable_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get selected row index
            int index = e.RowIndex;
            DataTable orders = (DataTable)OrderTable.DataSource;
            int orderId = (int)orders.Rows[index]["ID"];
            BlOrder order = new BlOrder(BlOrder.GetOrderById(orderId));
            int status = order.GetOrderStatus();//get order status by id
            bool success;
            try
            {
                success = status != -1 && status < 4 && order.DeleteOrder();//delete and chwck if status is good
            }
            catch (Exception exception)
            {
                ErMSG.Text = "fail cancel order " + exception.Message;//massage error
                return;
            }

            if (!success) return;
            MSG.Text = "order cancel successfully";//success massage
            Response.Redirect("ViewOrders.aspx");
            //Page_Load(sender, e);
        }

        protected void SearchOrderB_Click(object sender, EventArgs e)
        {
            BlOrderUser user = (BlOrderUser)Session["user"];
            //get input values
            string searchBys = SearchBy.Items[SearchBy.SelectedIndex].Value;
            string condition = "";
            string value = serchedValue.Text;
            switch (searchBys)
            {
                //update condition by selected search value 
                case "ArrivalTime":
                    condition = $"AND (Orders.{searchBys}=#{value}#)";
                    break;
                case "OrderStutus" when int.TryParse(value, out var status):
                    condition = $"AND (Orders.OrderStutus={status})";
                    break;
                case "OrderStutus":
                    {
                        Dictionary<int, string> stautus = new Dictionary<int, string> { { 1, "order sent" }, { 2, "shop take care your order" }, { 3, "shipping time selected" }, { 4, "delivery take care your order" }, { 5, "order shipped" } };
                        condition = $"AND (Orders.OrderStutus={stautus.FirstOrDefault(x => x.Value == value).Key})";
                        break;
                    }
                case "FirstName" when user.Type != 3:
                    condition = $"AND (Users_1.{searchBys}='{value}')";
                    break;
                default:
                    condition = $"AND (Shops.{searchBys}='{value}')";
                    break;
            }
            UpOrders((BlUser)Session["user"], condition);//update table

        }
        protected void NewOrOld_Click(object sender, EventArgs e)
        {
            UpOrders((BlUser)Session["user"], "");

        }

        protected void OrderTable_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);//get row index
            DataTable orders = (DataTable)OrderTable.DataSource;//get data
            int orderId = int.Parse(orders.Rows[index]["ID"].ToString());
            BlOrder order = new BlOrder(BlOrder.GetOrderById(orderId));
            int status = order.GetOrderStatus();
            if (e.CommandName == "updateArrivalTime"/*&& status==3*/)
            {
                if (!(Session["user"] is Deliver))
                {
                    Response.Redirect("HomePage.aspx");
                    return;
                }
                Response.Redirect("DeliveryMap.aspx?text='order started seccesfuly'");
                DateTime exitTime = DateTime.Now;
                double distanceToCustomerHome = GlobalVariable.Distance;
                double minutes = distanceToCustomerHome / GlobalVariable.Speed;
                DateTime arrivalTime = exitTime.AddMinutes(5 + minutes);//add 5 minute of insure 
                bool success = order.UpdateArrivalTime(arrivalTime) /*&& BLOrder.UpdateStatus(status + 1, orderID)*/;//update arrive time and order status 
                if (!success)
                {
                    Response.Redirect("ViewOrders.aspx");
                    ErMSG.Text = "fail to start order ";//massage error
                }
                else
                {
                    //Button myButton = null;
                    //GridViewRow row = OrderTable.Rows[index];
                    //myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                    //myButton.Text = "Order started";
                    //myButton.CommandName = "started";
                    ErMSG.Text = "fail to start order ";//massage error
                    UpOrders((BlUser)Session["user"], "");//update table
                }
            }
            else if (e.CommandName == "updateReadyTime" /*&& status == 2*/)
            {

                DateTime readyTime = DateTime.Now;

                //get the order object 
                //get the shop object 
                BlShop shop = BlShop.GetShopById(order.ShopId);
                //get the Id of the closest and . delivery  
                Deliver deliver = (Deliver)Session["user"];
                string matchDeliveryId = deliver.GetMatchesDeliveryId(shop.Location);
                bool success = order.UpdateReadyTime(readyTime) && order.UpdateDelivery(matchDeliveryId)/*&&BLOrder.UpdateStatus(status+1, orderID)*/;//update deliver and ready time in DB
                if (!success)
                {
                    ErMSG.Text = "fail to update ready time";//massage error
                }
                else
                {
                    //update button
                    Button myButton = null;
                    GridViewRow row = OrderTable.Rows[index];
                    myButton = (Button)(row.Cells[4].Controls[0]);
                    myButton.Text = "Product Ready";
                    myButton.CommandName = "Ready";
                    UpOrders((BlUser)Session["user"], "");//update table
                }
            }
            else if (e.CommandName == "finish")
            {
                if (status == 4/*&&BlOrder.UpdateStatus(status + 1, orderId)*/)//update order status
                {
                    NewOrOld.SelectedIndex = 1;//to see that the orders turn to old one
                    UpOrders((BlUser)Session["user"], "");//update table
                }
                else
                {
                    ErMSG.Text = "fail to update status";//massage error
                }



            }
        }

        //public  void GetBestWayLists(Deliver deliver)
        //{
        //    List<BlOrder> orders = deliver.GetOrdersListByTime();
        //    //set the road lists
        //    List<BlShop> shops = new List<BlShop>();
        //    List<BlCustomersAddress> customersAddresses = new List<BlCustomersAddress>();
        //    //full road lists
        //    foreach (BlOrder order in orders)
        //    {
        //        shops.Add(BlShop.GetShopById(order.ShopId));
        //        Point customerAddress = order.Location != null ? new Point(order.Location) : new Point(BlUser.UserById(order.CustomerId).Location);
        //        customersAddresses.Add(new BlCustomersAddress(customerAddress, order.NumOfFloor, deliver.GetName()));
        //    }

        //    if (orders.Count == 0) { ErMSG.Text = "there is no orders"; return; }
        //    else
        //    {
        //        ErMSG.Text = "";
        //    }
        //    if (orders.Count > 1)//to change to orders.Count>1!!!!!!!!!!!!!####$$$$$$$$########@@@@@@@*********#########&&&&&&&&&&$$$$$$$$$$^^^^^^^^^^%%%%%%%%%**********
        //    {
        //        //List<BlShop> sameShops = BlShop.isHaveSameShop(shops);
        //        //bool isHaveSameShop = sameShops.Count > 0;
        //        //if (isHaveSameShop)
        //        //{

        //        //}
        //        bool isAllSameShop = BlShop.isAllSameShop(shops);
        //        //calculate shorter way to deliver 
        //       DeliveryMap.GetBestWayLists(isAllSameShop, shops, customersAddresses, orders, deliver.Location);
        //    }
        //    else
        //    {
        //        //update the global vars
        //        Shops = Json.Encode(shops);
        //        Customers = Json.Encode(customersAddresses);
        //    }


        //}
        //protected void OrderTable_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //GridViewRow row = e.Row;
        //    //LinkButton myButton = row.FindControl("#ContentPlaceHolder1_OrderTable > tbody > tr:nth-child(2) > td:nth-child(5) > input[type=button]") as LinkButton;

        //    //if (myButton != null)
        //    //{
        //    //    myButton.Text = "Order started";
        //    //    myButton.CommandName = "started";
        //    //}

        //}
    }
}