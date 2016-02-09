using System.Diagnostics;

namespace Hs.ProcgameDmdModule.Business
{
    class DmdPlayer : IDmdPlayer
    {
        public void PlayDmd(string dmdFile, int Width,int Height,int frameTime=1, bool loop=false, bool VgaDmd=false)
        {
            string loopParams = "";
            if (loop)
                loopParams = " -r";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = @"C:\Python26\Scripts";
            startInfo.FileName = @"procgame.exe";
            string argPrefix = " dmdplayer ";

            if (VgaDmd)
            {                                                
                startInfo.Arguments = argPrefix + "\"" + dmdFile + "\"" +
                        @" -s " + Width + " " + Height + @" -f " + frameTime + " " + loopParams;                
            }
            else
            {
                startInfo.Arguments = argPrefix + "\"" + dmdFile + "\"" + loopParams;
            }

            //Process.Start(startInfo).WaitForExit();
            Process.Start(startInfo);
        }
    }
}
