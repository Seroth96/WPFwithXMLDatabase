using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopProjektWPF.BaseClass;
using DesktopProjektWPF.Commands;
using DesktopProjektWPF.Model;
using DesktopProjektWPF.XML;

namespace DesktopProjektWPF.ViewModel
{
    class KierunkiListViewModel : NotificationObject
    {
        private ObservableCollection<Kierunek> _kierunki;

        public ObservableCollection<Kierunek> Kierunki
        {
            get { return _kierunki; }
            set { _kierunki = value; OnPropertyChanged("Kierunki"); }
        }
        
        public KierunkiListViewModel(string filename)
        {
            Kierunki = new ObservableCollection<Kierunek>();
            Kierunki = XMLActions.Read(filename);
        }
        
    }
}
