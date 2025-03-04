using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Annex
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string OwnerName { get; set; }
        public double Distance { get; set; }
        public double MonthlyCharges { get; set; }
        public int AvailableStudents { get; set; }
        public string ContactNumber { get; set; } // New field for contact number
        public Annex Next { get; set; }

    }
}
