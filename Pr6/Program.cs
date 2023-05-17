using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pr6
{
    internal class Program
    {
        private const string XmlFilePath = @"C:\Users\Lenovo T470p\source\repos\Pr6\Pr6\StudentData.xml";
        private const string TxtFilePath = @"C:\Users\Lenovo T470p\source\repos\Pr6\Pr6\StudentText.txt";
        private const int BIRTH_YEAR_PLACEMENT = 5;

        static async Task Main(string[] args)
        {
            Student.OnSuccesfullParsing += Student_OnSuccesfullParsing;

            XmlSerializer serializer = new XmlSerializer(typeof(Student[]));

            Student[] students = CreateArrayOfStudents();

            Array.Sort(students);

            SerializeAll(serializer, students);
            WriteDownAll(students);
            var task = SerializeAllJsonAsync(students, "Json.json");

            PrintStudentsFromXML(serializer);
            PrintStudentsFromTxt();

            await task;
        }

        private static void Student_OnSuccesfullParsing(object? sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The object has been parsed succesfully.");
            Console.ForegroundColor = ConsoleColor.White;
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
            string birthDate = Console.ReadLine();

            Console.WriteLine("Enter the abmission date using \".\" to split the numbers");
            string abmissionDate = Console.ReadLine();

            Student student = new Student(sequenceNumber, fullName, birthDate, abmissionDate);
            return student;
        }

        static void SerializeAll(XmlSerializer serializer, Student[] students)
        {
            string filePath = XmlFilePath;

            using (FileStream data = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(data, students);
            }
        }

        async static Task<string> SerializeJsonAsync(object? obj)
        {
            return await Task.FromResult(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        async static Task SerializeAllJsonAsync(Student[] students, string filePath)
        {
            using (StreamWriter sw = new(File.Create(filePath)))
            {
                for (int i = 0; i < students.Length; i++)
                {
                    var line = await SerializeJsonAsync(students[0]);
                    await sw.WriteLineAsync(line);
                }
            }
        }

        async static Task<Student> DeserializeJsonAsync(string line)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<Student>(line));
        }

        async static IAsyncEnumerable<Student> DeserializeAllJsonAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open)))
            {
                while (sr.Peek() > -1)
                {
                    yield return await DeserializeJsonAsync(await sr.ReadLineAsync());
                }
            }
        }

        static void WriteDownAll(Student[] students)
        {
            using (StreamWriter text = new StreamWriter(TxtFilePath, false))
            {
                for (int i = 0; i < students.Length; i++)
                {
                    text.WriteLine(students[i].ToString());
                }
            }
        }

        static void PrintStudentsFromXML(XmlSerializer serializer)
        {
            int birthYear = ReadBirthYear();

            using (FileStream data = new FileStream(XmlFilePath, FileMode.Open))
            {
                Student[] students = (Student[])serializer.Deserialize(data);

                foreach (var student in students)
                {
                    if (int.Parse(student.BirthDate.Split('.')[2]) == birthYear)
                    {
                        Console.WriteLine(student.ToString());
                    }
                }
            }
        }

        static void PrintStudentsFromTxt()
        {
            int birthYear = ReadBirthYear();

            using (StreamReader ourText = new StreamReader(TxtFilePath, Encoding.UTF8))
            {
                while (ourText.Peek() != -1)
                {
                    string[] line = ourText.ReadLine().Trim().Split(' ', '.');

                    if (int.Parse(line[BIRTH_YEAR_PLACEMENT]) == birthYear)
                    {
                        foreach (var item in line)
                        {
                            Console.Write(item + " ");
                        }
                    }
                }
            }
        }

        private static int ReadBirthYear()
        {
            Console.WriteLine("Enter the year of birth");
            int birthYear = int.Parse(Console.ReadLine());
            return birthYear;
        }
    }
}