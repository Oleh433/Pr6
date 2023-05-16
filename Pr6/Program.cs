using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pr6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Student));

            Student[] students = CreateArrayOfStudents();

            Array.Sort(students);

            SerializeAll( serializer, students);
            WriteAll(students);
        }
        
        static Student[] CreateArrayOfStudents()
        {
            Console.WriteLine("Type number of students in array");
            int numberOfStudents = int.Parse(Console.ReadLine());
            Student[] students = new Student[numberOfStudents];

            for (int i = 0; i < numberOfStudents; i++)
            {
                students[i] = CreateStudent();
            }

            return students;
        }

        static Student CreateStudent()
        {
            Console.WriteLine("\nCreating student:");
            Console.WriteLine("Enter sequence number of the student");
            int sequenceNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter full name of the student (starting with surname)");
            string[] fullName = Console.ReadLine().Trim().Split();

            Console.WriteLine("Enter the birth date using \".\" to split the numbers");
            string[] birthDate = Console.ReadLine().Trim().Split(".");

            Console.WriteLine("Enter the abmission date using \".\" to split the numbers");
            string[] abmissionDate = Console.ReadLine().Trim().Split(".");

            Student student = new Student(sequenceNumber, fullName, birthDate, abmissionDate);
            return student;
        }

        static void SerializeAll(XmlSerializer serializer, Student[] students)
        {
            string filePath = @"C:\Users\Lenovo T470p\source\repos\Pr6\Pr6\StudentData";

            using (FileStream data = new FileStream(filePath, FileMode.Create))
            {
                for (int i = 0; i < students.Length; i++)
                {
                    serializer.Serialize(data, students[i]);
                }
            }
        }

        static void WriteAll(Student[] students)
        {
            string filePath = @"C:\Users\Lenovo T470p\source\repos\Pr6\Pr6\StudentText";

            using (StreamWriter text = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < students.Length; i++)
                {
                    text.WriteLine(students[i].ToString());
                }
            }
        }

        static void PrintStudents()
        {
            Console.WriteLine("Enter the year of birth");
            int birthYear = int.Parse(Console.ReadLine());
        }
    }
}