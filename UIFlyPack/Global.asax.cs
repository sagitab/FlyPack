using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Configuration;
using BLFlyPack;

namespace UIFlyPack
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.IO.Directory.SetCurrentDirectory(Server.MapPath("~/"));
            string connectionString = WebConfigurationManager.AppSettings["Path"];
            General.SetPath(connectionString);
            GlobalVariable.ShopManager = 1;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
          
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
             
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}