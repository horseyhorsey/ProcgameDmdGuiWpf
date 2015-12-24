using Hs.ProcgameDmdBase.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace Hs.ProcgameDmdModule
{
    public class PlayerViewModel : ViewModelBase
    {        
        public string ProcgameExePath { get; set; }

        public Tools.ISetDirectory setDirectoryInterface = new Tools.SetDirectory();

        public Business.IDmdPlayer playDmd = new Business.DmdPlayer();

        #region Constructors
        public PlayerViewModel()
        {
            ProcgameExePath = @"C:\P-ROC";

            DirectoryToSearch = @"C:\P-ROC\Shared\DMD";
            //ScanForDmdFiles();

            ScanCommand = new DelegateCommand(ScanForDmdFiles, ScanCanExecute).ObservesProperty(() => DirectoryToSearch);
            SetDirectoryPath = new DelegateCommand(setDire);
            PlayDmdCommand = new DelegateCommand<object>(DMDPlayer);

        }
        #endregion

        #region Properties
        private int _dmdWidth = 128;
        public int DmdWidth
        {
            get { return _dmdWidth; }
            set { SetProperty(ref _dmdWidth, value); }
        }

        private int _dmdHeight = 32;
        public int DmdHeight
        {
            get { return _dmdHeight; }
            set { SetProperty(ref _dmdHeight, value); }
        }

        private int _dmdFrameTime = 1;
        public int DmdFrameTime
        {
            get { return _dmdFrameTime; }
            set { SetProperty(ref _dmdFrameTime , value); }
        }

        private bool _loopWhenPlayed;
        public bool LoopWhenPlayed
        {
            get { return _loopWhenPlayed; }
            set { SetProperty(ref _loopWhenPlayed , value); }
        }
        private bool _isVgaDmd;
        public bool IsVgaDmd
        {
            get { return _isVgaDmd; }
            set {SetProperty(ref _isVgaDmd , value); }
        }

        private string directoryToSearch;
        public string DirectoryToSearch
        {
            get { return directoryToSearch; }
            set { SetProperty(ref directoryToSearch, value); }
        }

        public bool ScanSubDirectories { get; set; }

        private ObservableCollection<Models.Dmd> dmdFilesList;
        public ObservableCollection<Models.Dmd> DmdFilesList
        {
            get { return dmdFilesList; }
            set { SetProperty(ref dmdFilesList, value); }
        }
        #endregion
              
        #region Commands
        public DelegateCommand ScanCommand { get; set; }
        public DelegateCommand SetDirectoryPath { get; set; }
        public DelegateCommand<object> PlayDmdCommand { get; }

        #endregion
       
        #region Methods
        private void setDire()
        {
            DirectoryToSearch = setDirectoryInterface.SetADirectory();
        }

        private bool ScanCanExecute()
        {
            return DirectoryToSearch.Length > 4;
        }

        public void DMDPlayer(object o)
        {
            if (o != null)
            {
                var dmd = o as Models.Dmd;
                playDmd.PlayDmd(dmd.DmdFile, DmdWidth, DmdHeight, DmdFrameTime, LoopWhenPlayed, IsVgaDmd);
            }
            //Console.WriteLine(args[0]); 

        }

        public void ScanForDmdFiles()
        {
            string[] filePaths;


            try
            {
                if (ScanSubDirectories)
                {
                    filePaths = Directory.GetFiles(DirectoryToSearch, "*.dmd", SearchOption.AllDirectories);
                }
                else
                {
                    filePaths = Directory.GetFiles(DirectoryToSearch, "*.dmd");
                }


                DmdFilesList = new ObservableCollection<Models.Dmd>();
                //listView1.Items.Clear();
                foreach (var dmd in filePaths)
                {
                    if (ScanSubDirectories)
                    {
                        DmdFilesList.Add(new Models.Dmd { DmdFile = dmd });
                    }
                    else
                    {
                        //var dmdNew = Path.GetFileName(dmd);
                        DmdFilesList.Add(new Models.Dmd { DmdFile = dmd });
                    }
                }
            }
            catch (Exception)
            {


            }

        }
        #endregion

    }
}
