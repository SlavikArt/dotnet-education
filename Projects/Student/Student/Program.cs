namespace Student
{
    class Program
    {
        static void Main(string[] args)
        {
            Student st = new Student(
                "Kovalenko", "Vasiliy", "Alexandrovich",
                new DateTime(2002, 7, 15),
                new Address("Shevchenko's street", 
                    "Kiyv", "Kiyvska oblast", "01001"),
                "+380979747385", 
                new int[] { 10, 12, 9, 10, 11, 10 }, 
                new int[] { 12, 11, 10, 11 },
                new int[] { 10, 10, 10, 11 }
            );

            try
            {
                st.HomeAddress.PostalCode = "160";
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e + "\n");
            }

            Console.WriteLine("Created student:" +
                "\n----------------\n");
            st.Print();
            Console.WriteLine();

            Group grp = new Group();
            Console.WriteLine("All the students:" +
                "\n-----------------\n");
            grp.ShowAllStudents();

            grp.AddStudent(st);

            Console.WriteLine("All failing students:" +
                "\n---------------------\n");

            grp.ShowAllFailingStudents();

            Console.WriteLine("The lowest performing student:" +
                "\n-----------------------\n");

            grp.ShowLowestPerformingStudent();

            Console.WriteLine("\nExpel the lowest performing student:" +
                "\n------------------------------------\n");
            grp.ExpelLowestPerformingStudent();

            grp.ShowAllStudents();

            try
            {
                grp.GroupName = "test name";
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "\n");
            }

            try
            {
                grp.CourseNumber = 1003;
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "\n");
            }

            try
            {
                grp.Students.First().Name = "Alex123";
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "\n");
            }

            Student stud1 = grp.Students[new Random().Next(0, grp.Students.Count)];
            stud1.Print();
            Console.WriteLine();
            Student stud2 = grp.Students[new Random().Next(0, grp.Students.Count)];
            stud2.Print();
            Console.WriteLine();

            if (stud1)
                Console.WriteLine("stud1 is True");
            else
                Console.WriteLine("stud1 is False");
            if (stud2)
                Console.WriteLine("stud2 is True");
            else
                Console.WriteLine("stud2 is False");

            Console.WriteLine($"stud1 > stud2 is {stud1 > stud2} ({stud1.GetAverageGrade():F2} > {stud2.GetAverageGrade():F2})");
            Console.WriteLine($"stud1 < stud2 is {stud1 < stud2} ({stud1.GetAverageGrade():F2} < {stud2.GetAverageGrade():F2})");

            Console.WriteLine($"stud1 >= stud2 is {stud1 >= stud2} ({stud1.GetAverageGrade():F2} >= {stud2.GetAverageGrade():F2})");
            Console.WriteLine($"stud1 <= stud2 is {stud1 <= stud2} ({stud1.GetAverageGrade():F2} <= {stud2.GetAverageGrade():F2})");

            Console.WriteLine($"stud1 == stud2 is {stud1 == stud2} ({stud1.GetAverageGrade():F2} == {stud2.GetAverageGrade():F2})");
            Console.WriteLine($"stud1 != stud2 is {stud1 != stud2} ({stud1.GetAverageGrade():F2} != {stud2.GetAverageGrade():F2})");

            Console.WriteLine("\n" + grp[3]);

            Console.WriteLine(
                "\n-------------------\n" +
                "Sorted by Homework:" +
                "\n-------------------\n");
            Student.SortByHomework sortByHW = new Student.SortByHomework();
            grp.Sort(sortByHW);

            grp.Print();

            Console.WriteLine(
                "---------------------\n" +
                "Sorted by Final Work:" +
                "\n---------------------\n");
            Student.SortByFinalWork sortByFW = new Student.SortByFinalWork();
            grp.Sort(sortByFW);

            grp.Print();

            Console.WriteLine("---------\n");

            foreach (Student student in grp)
                Console.WriteLine(student.Name);
        }
    }
}
