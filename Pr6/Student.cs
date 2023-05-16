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
        private int sequenceNumber;
        public int SequenceNumber
        {
            get { return sequenceNumber;  }
            set {sequenceNumber = Math.Abs(value); } 
        }

        public string Surname;
        public string Name;
        public DateOnly BirthDate;
        public DateOnly AbmissionDate;

        public Student() { }
        public Student(int sequenceNumber, string[] fullName, string[] birthDate, string[] abmissionDate)
        {
            SequenceNumber = sequenceNumber;
            Surname = fullName[0];
            Name = fullName[1];

            BirthDate = new DateOnly(int.Parse(birthDate[2]), int.Parse(birthDate[1]), int.Parse(birthDate[0]));
            
            AbmissionDate = new DateOnly(int.Parse(abmissionDate[2]), int.Parse(abmissionDate[1]), int.Parse(abmissionDate[0])); ;
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
