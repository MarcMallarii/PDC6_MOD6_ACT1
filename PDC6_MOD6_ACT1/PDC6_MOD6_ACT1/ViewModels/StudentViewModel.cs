using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using PDC6_MOD6_ACT1.Models;
using PDC6_MOD6_ACT1.Services;
using MvvmHelpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace PDC6_MOD6_ACT1.ViewModels
{
    class StudentViewModel : BaseViewModel
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public string course { get; set; }
        public int yearLevel { get; set; }
        public string section { get; set; }

        private DBFirebase services;

        public Command AddStudentCommand { get; set; }

        public ObservableCollection<Student> _student = new ObservableCollection<Student>();

        public ObservableCollection<Student> Students
        {
            get
            {
                return _student;
            }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }

        public StudentViewModel()
        {
            services = new DBFirebase();
            Students = services.getStudent();
            AddStudentCommand = new Command(async () => await addStudentAsync(studentId, studentName, course, yearLevel, section));
        }

        public async Task addStudentAsync(int studentId, string studentName, string course, int yearLevel, string section)
        {
            await services.AddStudent(studentId, studentName, course, yearLevel, section);
        }
    }
}
