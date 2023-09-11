namespace Student
{
    public class Student
    {    
        private string surname;
        private string name;
        private string patronymic;

        private DateTime birthDate;
        private Address homeAddress;
        private string phoneNumber;

        private int[] homeworkGrades;
        private int[] finalWorkGrades;
        private int[] examGrades;

        public Student(string surname, string name, string patronymic, 
            DateTime birthDate, Address homeAddress, string phoneNumber)
        {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;

            this.birthDate = birthDate;
            this.homeAddress = homeAddress;
            this.phoneNumber = phoneNumber;
        }

        public Student(string surname, string name, string patronymic, 
            DateTime birthDate, Address homeAddress, string phoneNumber, 
            int[] homeworkGrades, int[] finalWorkGrades, int[] examGrades)
            : this(surname, name, patronymic, birthDate, homeAddress, phoneNumber)
        {
            this.homeworkGrades = homeworkGrades;
            this.finalWorkGrades = finalWorkGrades;
            this.examGrades = examGrades;
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidSurnameException("Surname cannot be null or whitespace.");
                if (!value.All(el => char.IsLetter(el)))
                    throw new InvalidSurnameException("Surname cannot contain not letter characters or whitespaces.");
                surname = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidNameException("Name cannot be null or whitespace.");
                if (!value.All(el => char.IsLetter(el)))
                    throw new InvalidNameException("Name cannot contain not letter characters or whitespaces.");
                name = value;
            }
        }

        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidPatronymicException("Patronymic cannot be null or whitespace.");
                if (!value.All(el => char.IsLetter(el)))
                    throw new InvalidPatronymicException("Patronymic cannot contain not letter characters or whitespaces.");
                patronymic = value;
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value >= DateTime.Now)
                        throw new InvalidBirthDateException("Birth date cannot be in the future.");
                if (value <= DateTime.Now.AddYears(-120)) // Assuming no student is older than 120 years (who knows)
                    throw new InvalidBirthDateException("Birth date is too far in the past.");
                birthDate = value;
            }
        }

        public Address HomeAddress
        {
            get { return homeAddress; }
            set
            {
                if (value == null)
                    throw new InvalidAddressException("Home address cannot be null.");
                homeAddress = value;
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value.Length != 10)
                    throw new InvalidPhoneNumberException("Phone number must be 10 digits long.");
                if (!value.All(char.IsDigit))
                    throw new InvalidPhoneNumberException("Phone number must contain only digits.");
                phoneNumber = value;
            }
        }

        public int[] HomeworkGrades
        {
            get { return homeworkGrades; }
            set
            {
                if (value == null)
                    throw new InvalidGradeException("Homework grades cannot be null.");
                if (!value.All(el => el >= 1 && el <= 12))
                    throw new InvalidGradeException("All homework grades must be between 1 and 12.");
                homeworkGrades = value;
            }
        }

        public int[] FinalWorkGrades
        {
            get { return finalWorkGrades; }
            set
            {
                if (value == null)
                    throw new InvalidGradeException("Final work grades cannot be null.");
                if (!value.All(el => el >= 1 && el <= 12))
                    throw new InvalidGradeException("All final work grades must be between 1 and 12.");
                finalWorkGrades = value;
            }
        }

        public int[] ExamGrades
        {
            get { return examGrades; }
            set
            {
                if (value == null)
                    throw new InvalidGradeException("Exam grades cannot be null.");
                if (!value.All(el => el >= 1 && el <= 12))
                    throw new InvalidGradeException("All exam grades must be between 1 and 12.");
                examGrades = value;
            }
        }

        public static bool operator ==(Student student1, Student student2)
        {
            if (ReferenceEquals(student1, student2))
                return true;

            if (ReferenceEquals(student1, null) || ReferenceEquals(student2, null))
                return false;

            return student1.GetAverageGrade() == student2.GetAverageGrade();
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return !(student1 == student2);
        }

        public static bool operator >(Student student1, Student student2)
        {
            return student1.GetAverageGrade() > student2.GetAverageGrade();
        }

        public static bool operator <(Student student1, Student student2)
        {
            return student1.GetAverageGrade() < student2.GetAverageGrade();
        }

        public static bool operator >=(Student student1, Student student2)
        {
            return student1.GetAverageGrade() >= student2.GetAverageGrade();
        }

        public static bool operator <=(Student student1, Student student2)
        {
            return student1.GetAverageGrade() <= student2.GetAverageGrade();
        }

        public static bool operator true(Student student)
        {
            return !student.IsFailing();
        }

        public static bool operator false(Student student)
        {
            return student.IsFailing();
        }

        public bool IsFailing()
        {
            return GetAverageGrade() < 7;
        }

        public double GetAverageGrade()
        {
            double totalGrades = homeworkGrades.Sum() + finalWorkGrades.Sum() + examGrades.Sum();
            double totalGradeCount = homeworkGrades.Length + finalWorkGrades.Length + examGrades.Length;
            return totalGrades / totalGradeCount;
        }

        public void Print()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"Surname: {surname}\nName: {name}\nPatronymic: {patronymic}"
                + $"\nBirthDate: {birthDate}\nHomeAddress: {homeAddress}\nPhoneNumber: {phoneNumber}"
                + "\nHomework Grades: " + string.Join(", ", homeworkGrades)
                + "\nFinal Work Grades: " + string.Join(", ", finalWorkGrades)
                + "\nExam Grades: " + string.Join(", ", examGrades);
        }
    }
}
