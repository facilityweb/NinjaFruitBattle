using Microsoft.Owin;
using NinjaFruitBattle.WebApi.App_Start;
using Owin;
[assembly: OwinStartup(typeof(Startup))]
namespace NinjaFruitBattle.WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}