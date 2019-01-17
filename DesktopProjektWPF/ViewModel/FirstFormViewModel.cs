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
    class FirstFormViewModel : NotificationObject
    {
        private Student _student;

        public Student Student
        {
            get { return _student; }
            set { _student = value; OnPropertyChanged("Student"); }
        }
        private Kierunek _kierunek;

        public Kierunek Kierunek
        {
            get { return _kierunek; }
            set { _kierunek = value; OnPropertyChanged("Kierunek"); }
        }
        
        public FirstFormViewModel()
        {
            Student = new Student();
            Kierunek = new Kierunek();
            SaveCommand = new RelayCommand(pars => Save(Student, Kierunek));
            DeleteCommand = new RelayCommand(pars => Delete(Student, Kierunek));
        }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void Save(Student student, Kierunek kierunek)
        {
            if (!IsValid(student, kierunek))
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
                ).Students.Add(student);
            }
            else
            {
                kierunek.Students.Add(student);
                kierunki.Add(kierunek);
            }
            XMLActions.Save(kierunki);
            Refresh();
            MessageBox.Show("Poprawnie dodano studenta!", "Sukces!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Delete(Student student, Kierunek kierunek)
        {
            if (!IsValid(student, kierunek))
            {
                MessageBox.Show("Nie wszystkie dane zostały podane!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult rsltMessageBox = DialogBoxWarn("Projekt WPF", "Czy na pewno cchcesz usunąć studenta?");

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
                ).Students.RemoveWhere(t => t.Name.ToLower().Equals(student.Name.ToLower()) && t.Surname.ToLower().Equals(student.Surname.ToLower()));
            }
            else
            {
                MessageBox.Show("Nie znaleziono takiego studenta!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var toDelte = kierunki.Where(k => k.Students.Count() == 0 && k.Teachers.Count() == 0);
            if (toDelte.Count() > 0)
            {
                kierunki.Remove(toDelte.First());
            }
            XMLActions.Save(kierunki);
            Refresh();
            MessageBox.Show("Poprawnie usunięto studenta!", "Sukces!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private MessageBoxResult DialogBoxWarn(string sCaption, string sMessageBoxText)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            return MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        private void Refresh()
        {
            Student = new Student();
            Kierunek = new Kierunek();
        }
        private bool IsValid(Student student, Kierunek kierunek)
        {
            if (string.IsNullOrWhiteSpace(student.Name) ||
                string.IsNullOrWhiteSpace(student.Surname) ||
                string.IsNullOrWhiteSpace(kierunek.Name) ||
                string.IsNullOrWhiteSpace(kierunek.Faculty))
            {
                return false;
            }
            return true;
        }
    }
}
