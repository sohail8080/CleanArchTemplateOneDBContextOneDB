using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(CleanArchTemplate.Startup))]
namespace CleanArchTemplate
{
    // This class is called at application Start up
    // Configure the auth and auth of appl
    // Configure the Web server for the application
    // Most configuration are done in web.config
    // Identity Configuration are done here by fluecnet interface.
    // in asp.ent core all configurationare done in Start up class
    // no more web. config in asp.net ocre
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
