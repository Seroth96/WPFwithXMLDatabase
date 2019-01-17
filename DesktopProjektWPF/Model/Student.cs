using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DesktopProjektWPF.BaseClass;

namespace DesktopProjektWPF.Model
{
    public class Student : NotificationObject
    {
        string name;

        [XmlElement(ElementName = "Name")]
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value.Trim();
                    OnPropertyChanged("Name");
                }
            }
        }

        string surname;

        [XmlElement(ElementName = "Surname")]
        public string Surname
        {
            get { return this.surname; }
            set
            {
                if (this.surname != value)
                {
                    this.surname = value.Trim();
                    OnPropertyChanged("Surname");
                }
            }
        }
        
        public Student()
        {
        }
    }
}
