using System.Collections;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Student
{
    public class Group : IEnumerable<Student>
    {
        private List<Student> students;
        private string groupName;
        private string specialization;
        private int courseNumber;

        private static string[] fakeSpecializations =
        {
            "Computer Science", "Software Engineering", "Information Technology",
            "Data Science", "Cybersecurity", "Artificial Intelligence"
        };
        private static string[] fakeSurnames = 
        { 
            "Ivanenko", "Petrenko", "Kovalenko", 
            "Bondarenko", "Tkachenko", "Moroz", 
            "Kovalchuk", "Kravchenko", "Oliynyk", "Shevchenko"
        };
        private static string[] fakeNames = 
        { 
            "Ivan", "Oleksandr", "Andriy", 
            "Mykhailo", "Serhiy", "Volodymyr", 
            "Oleh", "Yaroslav", "Viktor", "Dmytro" 
        };
        private static string[] fakePatronymics = 
        {
            "Ivanovych", "Petrovych", "Mykhailovych", 
            "Oleksiyovych", "Serhiyovych", "Andriyovych", 
            "Viktorovych", "Olehovych", "Yaroslavovych", "Volodymyrovych" 
        };
        private static string[] fakeStreets = 
        { 
            "Khreshchatyk St", "Volodymyrska St",
            "Saksahanskoho St", "Velyka Vasylkivska St", "Yaroslaviv Val St"
        };
        private static Dictionary<string, string> fakeCitiesAndStates = 
        new Dictionary<string, string>()
        {
            { "Kyiv", "Kyivska" },
            { "Kharkiv", "Kharkivska" },
            { "Odesa", "Odeska" },
            { "Dnipro", "Dnipropetrovska" },
            { "Lviv", "Lvivska" }
        };
        private static string[] fakePostalCodes = 
        { 
            "01001", "02002", 
            "03003", "04004", "05005"
        };

        public Group()
        {
            Random rnd = new Random();

            groupName = rnd.Next(1, 4) + " year course";
            specialization = fakeSpecializations[rnd.Next(fakeSpecializations.Length)];
            courseNumber = rnd.Next(100, 300);

            students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                string fakeSurname = fakeSurnames[rnd.Next(fakeSurnames.Length)];
                string fakeName = fakeNames[rnd.Next(fakeNames.Length)];
                string fakePatronymic = fakePatronymics[rnd.Next(fakePatronymics.Length)];

                DateTime fakeBirthDate = new DateTime(
                    rnd.Next(1990, 2004),
                    rnd.Next(1, 12),
                    rnd.Next(1, 28)
                );

                string fakeStreet = fakeStreets[rnd.Next(fakeStreets.Length)];

                int randomIndex = rnd.Next(fakeCitiesAndStates.Count);
                KeyValuePair<string, string> fakeCityAndState = fakeCitiesAndStates.ElementAt(randomIndex);

                string fakeCity = fakeCityAndState.Key;
                string fakeState = fakeCityAndState.Value;

                string fakePostalCode = fakePostalCodes[rnd.Next(fakePostalCodes.Length)];

                Address fakeAddress = new Address(fakeStreet, fakeCity, fakeState, fakePostalCode);

                string fakePhoneNumber = "+" + rnd.Next(100, 999) + rnd.Next(100, 999) + rnd.Next(1000, 9999);

                int[] fakeHomeworkGrades = new int[] { rnd.Next(6, 12), rnd.Next(6, 12), rnd.Next(6, 12) };
                int[] fakeFinalWorkGrades = new int[] { rnd.Next(6, 12), rnd.Next(6, 12), rnd.Next(6, 12), rnd.Next(6, 12) };
                int[] fakeExamGrades = new int[] { rnd.Next(6, 12), rnd.Next(6, 12) };

                students.Add(new Student(
                    fakeSurname,
                    fakeName,
                    fakePatronymic,
                    fakeBirthDate,
                    fakeAddress,
                    fakePhoneNumber,
                    fakeHomeworkGrades,
                    fakeFinalWorkGrades,
                    fakeExamGrades
                ));
            }
        }

        public Group(List<Student> students)
        {
            Random rnd = new Random();

            groupName = rnd.Next(1, 4) + " year course";
            specialization = fakeSpecializations[rnd.Next(fakeSpecializations.Length)];
            courseNumber = rnd.Next(100, 300);

            this.students = students;
        }

        public Group(Group group)
        {
            students = new List<Student>(group.Students);
            groupName = group.GroupName;
            specialization = group.Specialization;
            courseNumber = group.CourseNumber;
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return students.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)students).GetEnumerator();
        }

        public void Sort(IComparer<Student> sortType)
        {
            students.Sort(sortType);
        }

        public Student this[uint index]
        {
            get
            {
                if (index >= students.Count)
                    throw new GroupIndexOutOfRangeException("Student index is out of range!");
                return students[(int)index];
            }
            set
            {
                if (index >= students.Count)
                    throw new GroupIndexOutOfRangeException("Student index is out of range!");
                students[(int)index] = value;
            }
        }

        public List<Student> Students
        {
            get { return students; }
            set
            {
                if (value == null || value.Count == 0)
                    throw new InvalidGroupStudentsException("Invalid student list. The list cannot be null or empty.");
                
                var distinctStudents = value.Distinct();
                if (distinctStudents.Count() != value.Count)
                    throw new GroupDuplicateStudentsException("Duplicate students cannot be in the one list.");
                students = value;
            } 
        }

        public string GroupName
        {
            get { return groupName; }
            set
            {
                if (!Regex.IsMatch(value, @"[1-4] year course"))
                    throw new InvalidGroupNameException("Group name should match this pattern: '[1-4] year course'.");
                groupName = value;
            }
        }

        public string Specialization 
        { 
            get { return specialization; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidGroupSpecializationException("Group specialization cannot be null or whitespace.");
                if (!value.All(el => char.IsLetter(el) || char.IsWhiteSpace(el)))
                    throw new InvalidGroupSpecializationException("Group specialization cannot contain not letter characters.");
                specialization = value;
            }
        }

        public int CourseNumber 
        { 
            get { return courseNumber; }
            set
            {
                if (!(value > 0 && value < 1000))
                    throw new InvalidGroupCourseNumberException("Group specialization number cannot be negative or bigger that 999.");
                courseNumber = value;
            }
        }

        public void Print()
        {
            foreach (var student in students)
                Console.WriteLine(student + "\n");
        }

        public void ShowAllStudents()
        {
            Console.WriteLine($"Group: {groupName}\nSpecialization: {specialization}\n" +
                $"Course number: {courseNumber}\nStudents list:\n");

            foreach (var student in students.OrderBy(s => s.Surname))
                Console.WriteLine(student + "\n");
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void InsertStudent(int index, Student student)
        {
            students.Insert(index, student);
        }

        public void ReplaceStudent(Student oldStudent, Student newStudent)
        {
            int index = students.IndexOf(oldStudent);
            if (index == -1)
                throw new InvalidGroupReplaceStudentException("Student was not found in the group.");
            students[index] = newStudent;
        }

        public void EditGroup(string groupName, string specialization, int courseNumber)
        {
            this.groupName = groupName;
            this.specialization = specialization;
            this.courseNumber = courseNumber;
        }

        public void TransferStudent(Student student, Group newGroup)
        {
            if (!students.Remove(student))
                throw new InvalidGroupTransferStudentException("The student is not in the current group.");
            newGroup.AddStudent(student);
        }

        public void ShowAllFailingStudents()
        {
            students.ForEach(student =>
            {
                if (student.IsFailing())
                {
                    student.Print();
                    Console.WriteLine();
                }
            });
        }

        public void ShowLowestPerformingStudent()
        {
            Student lowestPerformingStudent =
                students.OrderBy(student => student.GetAverageGrade()).First();
            lowestPerformingStudent.Print();
        }

        public void ExpelAllFailingStudents()
        {
            students.RemoveAll(student => student.IsFailing());
        }

        public void ExpelLowestPerformingStudent()
        {
            Student lowestPerformingStudent = 
                students.OrderBy(student => student.GetAverageGrade()).First();
            students.Remove(lowestPerformingStudent);
        }
    }
}
