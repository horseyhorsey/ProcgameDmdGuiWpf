using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hs.ProcgameDmdModule.Business
{
    public interface IDmdPlayer
    {
        void PlayDmd(string dmdFile, int Width, int Height, int frameTime = 1, bool loop = false, bool VgaDmd = false);
    }
}
