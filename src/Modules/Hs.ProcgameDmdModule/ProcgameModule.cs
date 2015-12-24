using Hs.ProcgameDmdBase;
using Hs.ProcgameDmdBase.Base;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;


namespace Hs.ProcgameDmdModule
{
    public class ProcgameModule : PrismBaseModule
    {
        public DelegateCommand<string> NavigateCommand { get; set; }

        public ProcgameModule(IUnityContainer container, IRegionManager manager) : base(container, manager)
        {
            //UnityContainer.RegisterType<ILotteryRepo, Hs.ProcgameDmdModule.Models.LotteryRepo>();
            
            RegionManager.RegisterViewWithRegion(RegionNames.PlayerRegion, typeof(PlayerView));
            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(FilesView));


            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Views.SidebarView));

        }

        public override void Initialize()
        {
            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(PlayerView));
            //UnityContainer.RegisterType<SidebarView>();
            NavigateCommand = new DelegateCommand<string>(Navigate);


        }

        public void Navigate(string uri)
        {
            RegionManager.RequestNavigate("ContentRegion", uri);
        }


    }
}
