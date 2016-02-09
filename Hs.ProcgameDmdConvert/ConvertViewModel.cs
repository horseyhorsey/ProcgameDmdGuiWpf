using System;
using Hs.ProcgameDmdBase.Base;
using Prism.Commands;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Prism.Interactivity.InteractionRequest;
using System.Drawing;

namespace Hs.ProcgameDmdConvert
{
    public class ConvertViewModel : ViewModelBase
    {
        public ProcgameDmdConvert.Tools.ISetDirectory setDirectoryInterface = new ProcgameDmdConvert.Tools.SetDirectory();

        public InteractionRequest<INotification> NotificationRequest { get; set; }

        public ProcgameDmdModule.Business.IDmdPlayer playDmd = new ProcgameDmdModule.Business.DmdPlayer();

        #region Constructors
        public ConvertViewModel()
        {
            PickImageCommand = new DelegateCommand(PickImage);
            DestinationPicker = new DelegateCommand(PickDestination);
            ConvertDmdCommand = new DelegateCommand(ConvertImagesToDmd, ConvertCanExecute)
                .ObservesProperty(() => ImageSourcePath)
                .ObservesProperty(() => ImageDestinationPath);
            NotificationRequest = new InteractionRequest<INotification>();
            Notification = new DelegateCommand(RaiseNotification);
        }

        private bool ConvertCanExecute()
        {
            return File.Exists(ImageSourcePath) && Directory.Exists(ImageDestinationPath);
        }
        #endregion

        #region Properties
        private int _imageCount;
        public int ImageCount
        {
            get { return _imageCount; }
            set { SetProperty(ref _imageCount, value); }
        }

        private Size _imageSize;
        public Size ImageSize
        {
            get { return _imageSize; }
            set { _imageSize = value; }
        }

        private string _imageSourcePath;
        public string ImageSourcePath
        {
            get { return _imageSourcePath; }
            set { SetProperty(ref _imageSourcePath, value); }
        }

        private string _ImageDestinationPath;
        public string ImageDestinationPath
        {
            get { return _ImageDestinationPath; }
            set
            { SetProperty(ref _ImageDestinationPath, value); }
        }

        private string _dmdFileName;
        public string DmdFileName
        {
            get { return _dmdFileName; }
            set { SetProperty(ref _dmdFileName, value); }
        }

        private int _imageDigits = 3;
        public int ImageDigits
        {
            get { return _imageDigits; }
            set { SetProperty(ref _imageDigits, value); }
        }

        private string _procgameExePath;
        public string ProcgameExePath
        {
            get { return _procgameExePath; }
            set { SetProperty(ref _procgameExePath, value); }
        }

        private bool _isSavingToSource;
        public bool IsSavingToSource
        {
            get { return _isSavingToSource; }
            set {
                SetProperty(ref _isSavingToSource , value);
                if (_isSavingToSource)
                {                    
                    ImageDestinationPath = Path.GetDirectoryName(ImageSourcePath);
                }
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private bool _PlayAfterConversion;
        public bool PlayAfterConversion
        {
            get { return _PlayAfterConversion; }
            set { SetProperty(ref _PlayAfterConversion, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand PickImageCommand { get; set; }
        public DelegateCommand DestinationPicker { get; set; }
        public DelegateCommand ConvertDmdCommand { get; set; }
        public DelegateCommand Notification { get; set; }
        #endregion

        #region Methods
        private void RaiseNotification()
        {
            NotificationRequest.Raise(new Notification { Title = "Notification", Content = "Notice Message" }, r => Status = "Notified");
        }

        private void PickDestination()
        {
            ImageDestinationPath = setDirectoryInterface.SetADirectory();
            IsSavingToSource = false;
        }

        private void ConvertImagesToDmd()
        {
            string source = ImageSourcePath;
            string file = Path.GetFileName(source);
            //try
            //{
            string destination;            
            string path = Path.GetDirectoryName(source);            
            string ext = Path.GetExtension(source);
            string replace = @"%0" + ImageDigits + "d" + ext;
            string filename = DmdFileName;
            string fullFilename = DmdFileName + @".dmd";
            string procgameExe = ProcgameExePath + "\\";

            file = Regex.Replace(file, @"(/*[0-9].*)", replace);
            source = Path.Combine(path, file);

            if (IsSavingToSource)
            {
                destination = path;
            }
            else
            {
                destination = ImageDestinationPath;
            }

            ConvertDMD(destination, source, fullFilename);

        }

        public void ConvertDMD(string dest, string source, string filename)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = @"C:\Python26\Scripts";
            startInfo.FileName = @"procgame.exe";
            startInfo.Arguments = " dmdconvert " + "\"" + source + "\"" + " " + "\"" + dest + "\\" + filename + "\"";
            startInfo.UseShellExecute = false;
            Process.Start(startInfo).WaitForExit();
                        
            //DMDPlayer(dest + "\\" + filename);

             if (PlayAfterConversion)
              {
                playDmd.PlayDmd(dest + "\\" + filename,128,32);
             }
        }

        private void PickImage()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Images All Files (*.*)|*.*";
            fd.FilterIndex = 1;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                ImageSourcePath = fd.FileName.ToString();
                string text = Path.GetFileNameWithoutExtension(fd.ToString());
                ImageSize = GetImageSize(ImageSourcePath);
                //Remove digits for new dmd filename
                text = Regex.Replace(text, @"(/*[0-9].*)", "");
                DmdFileName = text;

                var imageExtension = Path.GetExtension(ImageSourcePath);
                CountImagesToBeConverted(imageExtension);
            }

        }

        private Size GetImageSize(string inputImage)
        {
            var imageSize = new Size();
            // Load the Image
            using (Image image = Image.FromFile(inputImage))
            {
                imageSize = new Size(image.Width, image.Height);
            }

            return imageSize;
        }

        private void CountImagesToBeConverted(string extension)
        {
            ImageCount = 0;
            foreach (var imageFile in Directory.GetFiles(Directory.GetParent(ImageSourcePath).ToString(), DmdFileName + "*" + extension))
            {
                ImageCount++;                
            }            
        }
        #endregion
    }
}
