using Hs.ProcgameDmdBase;
using Hs.ProcgameDmdBase.Base;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Hs.ProcgameDmdConvert
{
    public class ProcgameConvertModule : PrismBaseModule
    {
        public ProcgameConvertModule(IUnityContainer container, IRegionManager manager) : base(container, manager)
        {
            //RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentA));
            //RegionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(TestView));


            RegionManager.RegisterViewWithRegion(RegionNames.ConvertRegion, typeof(ConvertView));
        }

        public override void Initialize()
        {
            base.Initialize();
            //UnityContainer.RegisterType<ToolBarA>();
            //UnityContainer.RegisterType<IContentAView, ContentA>();
            //UnityContainer.RegisterType<IContentAViewViewModel, ContentAViewViewModel>();

            //RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ToolBarA));

            ////View Injection with more complicated items
            ////var vm = _container.Resolve<IContentAViewViewModel>();
            ////vm.Message = "First View yeah";
            ////_regionManager.Regions[RegionNames.ContentRegion].Add(vm.View);

            //// View injection best way??
            //var vm = UnityContainer.Resolve<IContentAViewViewModel>();           
            //vm.Message = "First View";

            //IRegion region = RegionManager.Regions[RegionNames.ContentRegion];

            //region.Add(vm.View);


            //var vm2 = UnityContainer.Resolve<IContentAViewViewModel>();
            //vm2.Message = "second View";

            //region.Deactivate(vm.View);
            //region.Add(vm2.View);

        }
    }
}
