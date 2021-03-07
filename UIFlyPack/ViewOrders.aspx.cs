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
            /*  Session["user"] = (BlUser)new BlShopManager("12345678");*///del
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
                            ButtonField b = new ButtonField
                            {
                                Text = "Show order details",
                                ButtonType = ButtonType.Button,
                                CommandName = "ShowOrderDetails"
                            };
                            OrderTable.Columns.Add(b);
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
        }

        public void UpdateButtonsText(int userType)
        {
            int length = OrderTable.Rows.Count;
            switch (userType)//convert button values by status and user type
            {

                case 3:
                    {
                        for (int i = 0; i < length; i++)
                        {
                            GridViewRow row = OrderTable.Rows[i];
                            TableCell cell = row.Cells[1];
                            string status = cell.Text;
                            if (status== "order sent")
                            {
                                Button button = null;
                                button = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                                button.CommandName = "not ready to start";
                            }
                            if (status != "delivery take care your order") continue;
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
                    int numOfColums = OrderTable.Columns.Count;
                    if (numOfColums > 0)
                    {
                        OrderTable.Columns[numOfColums - 1].Visible = true;
                    }
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
            //int userType = user.Type;//get user type 
            //int length = OrderTable.Rows.Count;
            //switch (userType)//convert button values by status and user type
            //{

            //    case 3:
            //    {
            //        for (int i = 0; i < length; i++)
            //        {
            //            GridViewRow row = OrderTable.Rows[i];
            //            TableCell cell = row.Cells[1];
            //            if (cell.Text != "delivery take care your order") continue;
            //            Button myButton = null;
            //            myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
            //            myButton.Text = "Order Finished";
            //            myButton.CommandName = "finish";
            //        }

            //        break;
            //    }
            //    case 1:
            //    {

            //        for (int i = 0; i < length; i++)
            //        {
            //            GridViewRow row = OrderTable.Rows[i];
            //            TableCell cell = row.Cells[1];
            //            if (cell.Text != "shipping time selected") continue;
            //            var myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
            //            myButton.Text = "Ready time selected";
            //            myButton.CommandName = "";
            //        }

            //        break;
            //    }
            //}
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
            UpdateButtonsText(user.Type);
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
                success = status != -1 && status < 4 && BLOrderDetailsDB.DeleteOrderDetails(orderId) && order.DeleteOrder();//delete and chwck if status is good
            }
            catch (Exception exception)
            {
                ErMSG.Text = "fail cancel order " + exception.Message;//massage error
                return;
            }

            if (!success)
            {
                MSG.Text = "to late to cancel this order please try another order";
                return;
            }
            MSG.Text = "order cancel successfully";//success massage
            UpOrders((BlUser)Session["user"], "");

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
                        Dictionary<int, string> status = new Dictionary<int, string> { { 1, "order sent" }, { 2, "shop take care your order" }, { 3, "shipping time selected" }, { 4, "delivery take care your order" }, { 5, "order shipped" } };
                        condition = $"AND (Orders.OrderStutus={status.FirstOrDefault(x => x.Value == value).Key})";
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
                // calculate the best way time
                Deliver deliver = (Deliver)Session["user"];
                //get the new orders of the deliver
                List<BlOrder> Orders = deliver.GetOrdersListByTime();
                //set the road lists
                List<BlShop> shops = new List<BlShop>();
                List<BlCustomersAddress> customersAddresses = new List<BlCustomersAddress>();
                //full road lists
                foreach (BlOrder Order in Orders)
                {
                    shops.Add(BlShop.GetShopById(Order.ShopId));
                    Point customerAddress = Order.Location != null ? new Point(Order.Location) : new Point(BlUser.UserById(Order.CustomerId).Location);
                    customersAddresses.Add(new BlCustomersAddress(customerAddress, Order.NumOfFloor, ""));
                }
                //calculate minutes
                double minutes = deliver.GetDistanceToCustomerHome(shops, customersAddresses);
                DateTime exitTime = DateTime.Now;
                DateTime arrivalTime = exitTime.AddMinutes(5 + minutes);//add 5 minute of insure 
                bool success = order.UpdateArrivalTime(arrivalTime) /*&& BLOrder.UpdateStatus(status + 1, orderID)*/;//update arrive time and order status 
                if (!success)
                {
                    Response.Redirect("ViewOrders.aspx");//go back to  page when there is a error
                    MSG.Visible = true;
                    MSG.Text = "fail to start order ";//massage error
                }
                else
                {
                    //Button myButton = null;
                    //GridViewRow row = OrderTable.Rows[index];
                    //myButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
                    //myButton.Text = "Order started";
                    //myButton.CommandName = "started";
                    MSG.Visible = true;
                    MSG.Text = "success to start order!!!";//massage error
                    UpOrders((BlUser)Session["user"], "");//update table
                    UpdateButtonsText(3);//3=delivery
                }
            }
            else if (e.CommandName == "updateReadyTime" /*&& status == 2*/)
            {

                DateTime readyTime = DateTime.Now;
                //get the order object 
                //get the shop object 
                BlShop shop = BlShop.GetShopById(order.ShopId);
                //get the Id of the closest  delivery  
                string matchDeliveryId = Deliver.GetMatchesDeliveryId(shop.Location);
                bool success = order.UpdateReadyTime(readyTime) && order.UpdateDelivery(matchDeliveryId) && BlOrder.UpdateStatus(status + 1,orderId);//update deliver and ready time in DB
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
                    BlUser userToSendMailTo = BlUser.UserById(order.DeliveryId);
                    List<BLOrderDetailsDB> Details = BLOrderDetailsDB.DetailsListOfOrder(orderId);
                    string productsString = BLOrderDetailsDB.GetProductString(Details);
                    bool isEmailSent = Register.sendEmail(userToSendMailTo.Email, " Fly pack you have a new order to deliver",
                        $"Hi,{userToSendMailTo} please accept your order as soon as you can.Here a summery of the order's products <br/> {productsString} <br/> Have a nice day,The Fly Pack Team");
                    if (!isEmailSent)
                    {
                        //take care if email dont send
                    }

                    MSG.Text = " update ready time!";
                }
            }
            else if (e.CommandName == "finish")
            {
                if (status == 4/*&&BlOrder.UpdateStatus(status + 1, orderId)*/)//update order status
                {
                    BlUser customer = BlUser.UserById(order.CustomerId);
                    NewOrOld.SelectedIndex = 1;//to see that the orders turn to old one
                    BlUser CurrentUser = (BlUser)Session["user"];
                    NewOrOld.SelectedIndex = 0;
                    UpOrders(CurrentUser, "");//update table
                    UpdateButtonsText(CurrentUser.Type);
                    //send email to customer
                    bool isEmailSent = Register.sendEmail(customer.Email, " Fly pack your order arrived!!!",
                        $"Hi,{customer} the drone arrive to your home please take your order.Have a nice day,The Fly Pack Team");
                    if (!isEmailSent)
                    {
                        //take care if email don't send
                    }
                    MSG.Text = "success to update status";
                }
                else
                {
                    ErMSG.Text = "fail to update status";//massage error
                }



            }
            else if (e.CommandName == "ShowOrderDetails")
            {
                List<BLOrderDetail> orderDetails = BLOrderDetail.GetOrderDetailsByOrderId(orderId);
                Update(orderDetails);
                OrderDetailsPanel.Visible = true;
            }

            if (e.CommandName== "not ready to start")
            {
                MSG.Text = "wait to shop manager to confirm order ";//massage 
            }
        }
        public void Update(List<BLOrderDetail> products)
        {
            ProductsCart.DataSource = products;
            ProductsCart.DataBind();
            UpdateSumCart(products);
            if (products == null || products.Count == 0)
            {
                ProductError.Text = "there is no products"; //error msg
                NumOfProducts.Text = "";
                TotalPrice.Text = "";
                return;
            }
            ProductError.Text = "";
        }
        /// <summary>
        /// update text of numOfProducts and totalPrice labels by productsCart list
        /// </summary>
        /// <param name="productsCart"></param>
        public void UpdateSumCart(List<BLOrderDetail> productsCart)
        {
            int numOfProducts = BLOrderDetail.TotalAmount(productsCart);
            NumOfProducts.Text = "Num Of Products-" + numOfProducts;
            double totalPrice = BLOrderDetail.TotalPrice(productsCart);
            TotalPrice.Text = "Total Price-" + totalPrice;
        }
        protected void XButton_OnClick(object sender, ImageClickEventArgs e)
        {
            OrderDetailsPanel.Visible = false;//to 'close' the window of the order details panel
        }
    }
}