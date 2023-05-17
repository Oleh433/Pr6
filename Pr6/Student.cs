using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr6
{
    [Serializable]
    public struct Student : IComparable<Student>
    {
        private static List<int> _sequenceNumbers = new List<int>();

        public static event EventHandler? OnSuccesfullParsing;

        private int sequenceNumber;
        public int SequenceNumber
        {
            get { return sequenceNumber;  }
            set {sequenceNumber = Math.Abs(value); } 
        }

        public string Surname;
        public string Name;

        private DateOnly _birthDate;
        public string BirthDate
        {
            get => $"{_birthDate.Day}.{_birthDate.Month}.{_birthDate.Year}";
            set
            {
                string[] date = value.Split('.');
                _birthDate = new DateOnly(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            }
        }

        private DateOnly _abmissionDate;
        public string AbmissionDate
        {
            get => $"{_abmissionDate.Day}.{_abmissionDate.Month}.{_abmissionDate.Year}";
            set
            {
                string[] date = value.Split('.');
                _abmissionDate = new DateOnly(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            }
        }

        public Student() { }
        public Student(int sequenceNumber, string[] fullName, string birthDate, string abmissionDate)
        {
            if (_sequenceNumbers.Contains(sequenceNumber))
            {
                throw new ArgumentException("There is already a student with the same sequence number.");
            }

            SequenceNumber = sequenceNumber;
            Surname = fullName[0];
            Name = fullName[1];

            BirthDate = birthDate;
            AbmissionDate = abmissionDate;

            _sequenceNumbers.Add(sequenceNumber);

            OnSuccesfullParsing?.Invoke(this, new EventArgs());
        }

        public int CompareTo(Student other)
        {
            return SequenceNumber.CompareTo(other.SequenceNumber);
        }

        public override string ToString()
        {
            return $"{sequenceNumber} {Surname} {Name} {BirthDate} {AbmissionDate}";
        }
    }
}
