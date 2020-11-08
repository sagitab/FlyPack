using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLFlyPack;
namespace UIFlyPack
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {

                //Session["user"]=new BLUser("12345678");
                BlUser user = (BlUser)Session["user"];
                if (user != null)
                {
                    int type = user.Type;
                    if (type == 1)//massage to shop manager
                    {
                        instractor.InnerHtml = "Type shop address or click on the map to add address";
                    }

                    int RegType = 0;
                    try
                    {
                        RegType = int.Parse(Request.QueryString.Get("Type"));
                    }
                    catch
                    {
                        Response.Redirect("HomePage.aspx");
                    }

                    switch (RegType)//change header text
                    {
                        case 3:
                            PageHeader.InnerHtml = "Add delivery";
                            break;
                        case 1:
                            PageHeader.InnerHtml = "Add shop manager";
                            break;
                        case 4:
                            PageHeader.InnerHtml = "Register";
                            break;
                        default:
                            Response.Redirect("HomePage.aspx");
                            break;
                    }

                }
                //else
                //{
                //    //security 
                //}

            }
            //pass.Text = "00000000";
            Validate();

        }

        public static string GenerateRandomCode()
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = chars.Length;
            string code = "";
            const int codeLength = 6;
            for (int i = 0; i < codeLength; i++)
            {
                int a = random.Next(length - 1);
                code = code + chars.ElementAt(a).ToString();
            }
            return code;
        }
        public static bool sendEmail(string sentToEmail, string subject, string info)
        {
            const string sentFromEmail = "ramoncomp2021@gmail.com";
            const string smtpEmailAddress = "smtp.gmail.com";
            const string EmailPassword = "RamonComp2021";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(smtpEmailAddress);
                string sendTo = sentToEmail;
                mail.From = new MailAddress(sentFromEmail);
                mail.To.Add(sendTo);
                mail.IsBodyHtml = true;
                mail.Subject = subject;//mail subject 
                mail.Body = info;//mail info 
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(sentFromEmail, EmailPassword);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }

        }

        protected void regB_Click(object sender, EventArgs e)
        {
            bool validators = Page.IsValid;// to check if email is valid !!!!!!!!!!!
            if (validators && Name.Text != "" && LName.Text != "" && Email.Text != "" && pass.Text != "" && Phone.Text != "" && ID.Text != "" && pass.Text != "00000000")
            {
                //get input values
                string id = ID.Text;
                string name = Name.Text;
                string lname = LName.Text;
                string password = pass.Text;
                string phoneNum = Phone.Text;
                string email = Email.Text;
                bool passCheck = false;
                try
                {
                    passCheck = BlUser.PasswordCheck(password);//check if there is a user with the same password
                }
                catch (Exception exception)
                {
                    MSG.Text = "fail register " + exception.Message;//error massage
                }
                string latLng = this.LatLng.Value.ToString();
                double lat = OrderNow.GetLat(latLng);
                double lng = OrderNow.GetLng(latLng);
                if (passCheck)
                {
                    int type = 0;
                    try
                    {
                        type = int.Parse(Request.QueryString.Get("Type"));
                    }
                    catch
                    {
                        Response.Redirect("HomePage.aspx");
                        return;
                    }

                    //if (type!=1&& type != 3&&type != 4)
                    //{
                    //    Response.Redirect("HomePage.aspx");
                    //    return;
                    //}
                    BlUser user = null;
                    try
                    {
                        user = new BlUser(id, type, email, phoneNum, name, lname, password, lat, lng);//add new user to DB
                    }
                    catch (Exception exception)
                    {
                        MSG.Text = "fail register " + exception.Message;//error massage
                    }

                    //log in when finish to register
                    //if (user!=null)
                    //{
                    //    string pass = user.Password;
                    //    user = user.Type == 1 ? (BlUser)new BlShopManager(pass) : new BlOrderUser(pass);
                    //    Session["user"] = user;
                    //}
                    if (user == null || user.UserId == "-1") return;
                    //create a verify code
                    string verifyCode = GenerateRandomCode();
                    //send verify code to email to verify the email
                    bool isEmailSent = sendEmail(email, " Fly pack please verify your email address.",
                        $"Almost done,{name}! To complete your  sign up, we just need to verify your email address: {email}.<br/> Verification code: {verifyCode}<br/> Once verified, you can start order products.<br/>You’re receiving this email because you recently created a new Fly Pack account or added a new email address.<br/> If this wasn’t you, please ignore this email.<br/>Thanks,<br/>The Fly Pack Team");
                    if (!isEmailSent)
                    {
                        MSG.Text = "unvalidated email address";//error massage
                    }
                    else
                    {
                        BlOrderUser currentUser = (BlOrderUser) Session["user"];
                        bool isCustomer = (currentUser != null && currentUser.Type == 4);
                        if (isCustomer)
                        {
                            Session["user"] = new BlOrderUser(user.Password);
                        }
                    
                        try
                        {
                            GlobalVariable.UnVerifyEmail.Add(user.UserId, verifyCode);
                        }
                        catch 
                        {
                            GlobalVariable.UnVerifyEmail = new Dictionary<string, string> {{user.UserId, verifyCode}};
                        }

                        if (isCustomer)
                        {
                            Response.Redirect("VerifyEmail.aspx");
                        }
                        
                    }
                }
                else
                {
                    MSG.Text = "Password caught please try again";//error massage
                }

            }
            else
            {
                //convert the the text box style by the validator
                Phone.CssClass = PhoneValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                pass.CssClass = passValidator.IsValid && PassNotEmpty.IsValid ? "TextBox" : "TextBoxUnValidValue";
                Email.CssClass = EmailValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                Name.CssClass = NameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                LName.CssClass = LNameValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";
                ID.CssClass = IDValidator.IsValid ? "TextBox" : "TextBoxUnValidValue";

                MSG.Text = "Please type all the details";//error massage
            }
        }


    }
}