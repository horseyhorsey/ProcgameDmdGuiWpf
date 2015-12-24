using Microsoft.Practices.Unity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Globalization;

namespace Hs.ProcgameDmdGui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IUnityContainer _container = new UnityContainer();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Bootstrapper bootStrap = new Bootstrapper();
            bootStrap.Run();

            //_container.RegisterType<IMyObject, MyObject>();

            //ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            //{
            //    var viewName = viewType.FullName;
            //    var viewAssembleyName = viewType.GetTypeInfo().Assembly.FullName;
            //    var viewModelName = String.Format(CultureInfo.InvariantCulture,
            //        "{0}ViewModel, {1}", viewName, viewAssembleyName);

            //    return Type.GetType(viewModelName);
            //});

            //ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
            //{
            //    return _container.Resolve(type);
            //});


        }
    }
}
