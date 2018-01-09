using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Backend.DataObjects;
using Backend.Models;
using Owin;

namespace Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();
                
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var mobileConfig = new MobileAppConfiguration();

            mobileConfig
                .AddTablesWithEntityFramework()
                .ApplyTo(config);

            app.UseWebApi(config);
        }
    }
}