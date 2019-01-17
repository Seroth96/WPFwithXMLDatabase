using System;
using System.Collections.Generic;
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
    class SecondFormViewModel : NotificationObject
    {
        private Teacher _teacher;

        public Teacher Teacher
        {
            get { return _teacher; }
            set { _teacher = value; OnPropertyChanged("Teacher"); }
        }
        private Kierunek _kierunek;

        public Kierunek Kierunek
        {
            get { return _kierunek; }
            set { _kierunek = value; OnPropertyChanged("Kierunek"); }
        }
        public SecondFormViewModel()
        {
            Teacher = new Teacher();
            Kierunek = new Kierunek();
            SaveCommand = new RelayCommand(pars => Save(Teacher, Kierunek));
            DeleteCommand = new RelayCommand(pars => Delete(Teacher, Kierunek));
        }


        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        private void Save(Teacher teacher, Kierunek kierunek)
        {
            if (!IsValid(teacher, kierunek))
            {
                MessageBox.Show("Nie wszystkie dane zostały podane!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var kierunki = XMLActions.Read();
            var matches = kierunki.Where(k => k.Name.ToLower().Equals(Kierunek.Name.ToLower()) && k.Faculty.ToLower().Equals(Kierunek.Faculty.ToLower()));
            if (matches.Count() > 0)
            {
                kierunki.First(k =>
                    k.Name.ToLower().Equals(Kierunek.Name.ToLower()) &&
                    k.Faculty.ToLower().Equals(Kierunek.Faculty.ToLower())
                ).Teachers.Add(teacher);
            }
            else
            {
                kierunek.Teachers.Add(teacher);
                kierunki.Add(kierunek);
            }
            XMLActions.Save(kierunki);
            Refresh();
            MessageBox.Show("Poprawnie dodane nauczyciela!", "Sukces!", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void Delete(Teacher teacher, Kierunek kierunek)
        {

            if (!IsValid(teacher, kierunek))
            {
                MessageBox.Show("Nie wszystkie dane zostały podane!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult rsltMessageBox = DialogBoxWarn("Projekt WPF", "Czy na pewno cchcesz usunąć nauczyciela?");

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    break;
                case MessageBoxResult.No:
                    return;
            }

            var kierunki = XMLActions.Read();
            var matches = kierunki.Where(k => k.Name.ToLower().Equals(Kierunek.Name.ToLower()) && k.Faculty.ToLower().Equals(Kierunek.Faculty.ToLower()));
            if (matches.Count() > 0)
            {
                kierunki.First(k =>
                    k.Name.ToLower().Equals(Kierunek.Name.ToLower()) &&
                    k.Faculty.ToLower().Equals(Kierunek.Faculty.ToLower())
                ).Teachers.RemoveWhere(t => t.Name.ToLower().Equals(teacher.Name.ToLower()) && t.Surname.ToLower().Equals(teacher.Surname.ToLower()));
            }
            else
            {
                MessageBox.Show("Nie znaleziono takiego nauczyciela!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var toDelte = kierunki.Where(k => k.Students.Count() == 0 && k.Teachers.Count() == 0);
            if (toDelte.Count() > 0)
            {
                kierunki.Remove(toDelte.First());
            }
            XMLActions.Save(kierunki);
            Refresh();
            MessageBox.Show("Poprawnie usunięto nauczyciela!", "Sukces!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Refresh()
        {
            Teacher = new Teacher();
            Kierunek = new Kierunek();
        }
        private MessageBoxResult DialogBoxWarn(string sCaption, string sMessageBoxText)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        private bool IsValid(Teacher teacher, Kierunek kierunek)
        {
            if (string.IsNullOrWhiteSpace(teacher.Name) ||
                string.IsNullOrWhiteSpace(teacher.Surname) ||
                string.IsNullOrWhiteSpace(kierunek.Name) ||
                string.IsNullOrWhiteSpace(kierunek.Faculty))
            {
                return false;
            }
            return true;
        }
    }
}
