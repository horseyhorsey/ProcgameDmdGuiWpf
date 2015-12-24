using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;

namespace Hs.ProcgameDmdGui
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            // Register ModuleA
            moduleCatalog.AddModule(typeof(Hs.ProcgameDmdModule.ProcgameModule));
            moduleCatalog.AddModule(typeof(Hs.ProcgameDmdConvert.ProcgameConvertModule));
            //moduleCatalog.AddModule(typeof(SidebarModule));
            //moduleCatalog.AddModule(typeof(LotteryResultsModule));
            //moduleCatalog.AddModule(typeof(HeaderModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //Container.RegisterType<LotteryDraws.IMyObject, LotteryDraws.MyObject>();
            // Old implementation
            //Container.RegisterType(typeof(object), typeof(Hs.ProcgameDmdModule.PlayerView),"PlayerView");

            //Container.RegisterTypeForNavigation<ProcgameDmdModule.PlayerView>("PlayerView");
            //Container.RegisterTypeForNavigation<ProcgameDmdModule.SidebarView>("SidebarView");
        }

        

    }
}
