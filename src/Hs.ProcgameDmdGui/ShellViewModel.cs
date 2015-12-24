using Hs.ProcgameDmdBase.Base;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hs.ProcgameDmdGui
{
    public class ShellViewModel : ViewModelBase
    {
        Hs.ProcgameDmdBase.Tools.ISetDirectory SetDir = new Hs.ProcgameDmdBase.Tools.SetDirectory();
        
        public DelegateCommand dg { get; set; }

        public ShellViewModel()
        {
            dg = new DelegateCommand(SetDirectory);
        }

        private void SetDirectory()
        {
            ProcgameExe = SetDir.SetADirectory();
        }

        private string _procgameExe = @"C:\Python26\Scripts";
        public string ProcgameExe
        {
            get { return _procgameExe; }
            set { SetProperty(ref _procgameExe , value);
                               }
        }
        
    }
}
