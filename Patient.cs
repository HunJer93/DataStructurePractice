using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntonProgramTwo
{
    public class Patient : IComparable<Patient>
    {
        static public int Comparisons { get; set; }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public double ProcedureCost { get; }

        public Patient(string first, string last, double cost, int id)
        {
            FirstName = first;
            LastName = last;
            ProcedureCost = cost;
            Id = id;
        }

        public Patient(string first, string last, double cost) : this(first, last, cost, -1)
        {
            Id = genPatientId(first, last);
        }

        public static int genPatientId(string first, string last)
        {
            int id = Math.Abs((last.ToUpper() + first.ToUpper()).GetHashCode()) % 799993;
            if (id < 100000)
                id += 99997;

            return id;
        }

        public int CompareTo(Patient other)
        {
            Comparisons++;
            return Id - other.Id;
        }
    }
}
