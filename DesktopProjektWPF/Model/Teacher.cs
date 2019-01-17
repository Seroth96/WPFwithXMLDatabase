using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DesktopProjektWPF.BaseClass;

namespace DesktopProjektWPF.Model
{
    public class Teacher : NotificationObject
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

        string speciality;

        [XmlElement(ElementName = "Speciality")]
        public string Speciality
        {
            get { return this.speciality; }
            set
            {
                if (this.speciality != value)
                {
                    this.speciality = value.Trim();
                    OnPropertyChanged("Speciality");
                }
            }
        }
    }
}
