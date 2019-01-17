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
    public class Kierunek : NotificationObject
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
                    this.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        string faculty;

        [XmlElement(ElementName = "Faculty")]
        public string Faculty
        {
            get { return this.faculty; }
            set
            {
                if (this.faculty != value)
                {
                    this.faculty = value;
                    OnPropertyChanged("Faculty");
                }
            }
        }

        [XmlElement(ElementName = "Teachers")]
        ObservableCollection<Teacher> teachers;
        public ObservableCollection<Teacher> Teachers
        {
            get
            { return this.teachers; }
            set
            {
                if (this.teachers != value)
                {
                    this.teachers = value;
                    OnPropertyChanged("Teachers");
                }
            }
        }

        [XmlElement(ElementName = "Students")]
        ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get
            { return this.students; }
            set
            {
                if (this.students != value)
                {
                    this.students = value;
                    OnPropertyChanged("Students");
                }
            }
        }

        public Kierunek()
        {
            this.Teachers = new ObservableCollection<Teacher>();
            this.Students = new ObservableCollection<Student>();
        }
    }
}
