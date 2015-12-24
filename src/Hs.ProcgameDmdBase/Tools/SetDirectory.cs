using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hs.ProcgameDmdBase.Tools
{    
   public class SetDirectory : ISetDirectory
    {        
        public SetDirectory()
        {
            
        }

        public string SetADirectory()
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();

            if (fb.ShowDialog() == DialogResult.OK)
            {
                return fb.SelectedPath;
            }

            return "";
        }
    }
}
