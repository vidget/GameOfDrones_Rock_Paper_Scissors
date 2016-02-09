using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameOfDrones_Rock_Paper_Scissors.Startup))]
namespace GameOfDrones_Rock_Paper_Scissors
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
