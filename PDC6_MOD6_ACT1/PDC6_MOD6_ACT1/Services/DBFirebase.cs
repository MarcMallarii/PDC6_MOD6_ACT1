using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using PDC6_MOD6_ACT1.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PDC6_MOD6_ACT1.Services
{
    public class DBFirebase
    {
        FirebaseClient client;

        public DBFirebase()
        {
            client = new FirebaseClient("https://pdcmod6act1-default-rtdb.firebaseio.com/");
        }

        public ObservableCollection<Student> getStudent()
        {
            var studentData = client
                .Child("Student")
                .AsObservable<Student>()
                .AsObservableCollection();

            return studentData;
        }

        public async Task AddStudent(int StudentId, string StudentName, string Course, int YearLevel, string Section)
        {
            Student em = new Student()
            {
                studentId = StudentId,
                studentName = StudentName,
                course = Course,
                yearLevel = YearLevel,
                section = Section
            };

            await client
                .Child("Student")
                .PostAsync(em);
        }

        public async Task DeleteStudent(int StudentId, string StudentName, string Course, int YearLevel, string Section)
        {
            var toDeleteStudent = (await client
                .Child("Student")
                .OnceAsync<Student>()).FirstOrDefault
                (a => a.Object.studentId == StudentId || a.Object.studentName == StudentName || a.Object.course == Course || a.Object.yearLevel == YearLevel || a.Object.section == Section);

            await client
                .Child("Student")
                .Child(toDeleteStudent.Key)
                .DeleteAsync();
        }

        public async Task UpdateStudent(int StudentId, string StudentName, string Course, int YearLevel, string Section)
        {
            var toUpdateStudent = (await client
                .Child("Student")
                .OnceAsync<Student>()).FirstOrDefault();

            Student s = new Student()
            {
                studentId = StudentId,
                studentName = StudentName,
                course = Course,
                yearLevel = YearLevel,
                section = Section
            };

            await client
                .Child("Student")
                .Child(toUpdateStudent.Key)
                .PutAsync(s);
        }
    }
}
