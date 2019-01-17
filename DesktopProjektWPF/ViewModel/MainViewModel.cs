using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DesktopProjektWPF.BaseClass;
using DesktopProjektWPF.Commands;
using DesktopProjektWPF.Model;
using DesktopProjektWPF.XML;

namespace DesktopProjektWPF.ViewModel
{
    public class MainViewModel : NotificationObject
    {
        private object _myContent;
        public object MyContent {
            get { return _myContent; }
            private set {
                _myContent = value;
                OnPropertyChanged("MyContent");
            } }
        public MainViewModel()
        {
            FileStream fs = new FileStream("test.xml", FileMode.OpenOrCreate);
            fs.Close();
            MyContent = new FirstFormViewModel();
            SaveCommand = new RelayCommand(pars => SaveCommandExecute());
            OpenCommand = new RelayCommand(pars => OpenCommandExecute());
            ImportCommand = new RelayCommand(pars => ImportCommandExecute());
            ShowDataCommand = new RelayCommand(pars => ShowDataCommandExecute());
            ExitCommand = new RelayCommand(pars => ExitCommandExecute());
            ShowFirstFormCommand = new RelayCommand(pars => ShowFirstFormCommandExecute());
            ShowSecondFormCommand = new RelayCommand(pars => ShowSecondFormCommandExecute());
        }
        private MessageBoxResult DialogBoxWarn(string sCaption, string sMessageBoxText)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);            
        }
        private void ExitCommandExecute()
        {
            MessageBoxResult rsltMessageBox = DialogBoxWarn("Projekt WPF", "Czy na pewno chcesz zamknąć aplikację?");

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    Application.Current.Shutdown();
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
        }

        private void ShowDataCommandExecute()
        {
            MyContent = new KierunkiListViewModel(XMLActions.Filename);
        }

        private void ImportCommandExecute()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                XMLActions.Filename = filename;
            }
        }

        private void OpenCommandExecute()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                MyContent = new KierunkiListViewModel(filename);
            }
        }

        private void ShowFirstFormCommandExecute()
        {
            MyContent = new FirstFormViewModel();
        }

        private void ShowSecondFormCommandExecute()
        {
            MyContent = new SecondFormViewModel();            
        }

        private void SaveCommandExecute()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                var kierunki = XMLActions.Read();
                XMLActions.Save(kierunki, filename);
            }
        }

        public ICommand SaveCommand { get; set; }
            public ICommand OpenCommand { get; set; }
            public ICommand ImportCommand { get; set; }
            public ICommand ShowDataCommand { get; set; }
            public ICommand ExitCommand { get; set; }
            public ICommand ShowFirstFormCommand { get; set; }
            public ICommand ShowSecondFormCommand { get; set; }

        private string _FirstName = null;
            public string FirstName
            {
                get
                {
                    return _FirstName;
                }
                set
                {
                    _FirstName = null;
                    OnPropertyChanged("FirstName");
                }
            }
            private string _LastName = null;
            public string LastName
            {
                get
                {
                    return _LastName;
                }
                set
                {
                    _LastName = null;
                    OnPropertyChanged("LastName");
                }
            }
            
        

    }
}
