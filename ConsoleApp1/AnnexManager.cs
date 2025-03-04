using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AnnexManager
    {
        public Annex Head;

        public void AddAnnex(string name, string location, string ownerName, double distance, double charges, int students, string contactNumber)
        {
            Annex newAnnex = new Annex
            {
                Name = name,
                Location = location,
                OwnerName = ownerName,
                Distance = distance,
                MonthlyCharges = charges,
                AvailableStudents = students,
                ContactNumber = contactNumber, // Assigning contact number
                Next = Head
            };
            Head = newAnnex;
        }



        // Sorting methods (unchanged)
        //bubble sort
        public void BubbleSortByDistance(List<Annex> annexes)
        {
            int n = annexes.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (annexes[j].Distance > annexes[j + 1].Distance)
                    {
                        Annex temp = annexes[j];
                        annexes[j] = annexes[j + 1];
                        annexes[j + 1] = temp;
                    }
                }
            }
        }

        public void BubbleSortByCharges(List<Annex> annexes)
        {
            int n = annexes.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (annexes[j].MonthlyCharges > annexes[j + 1].MonthlyCharges)
                    {
                        Annex temp = annexes[j];
                        annexes[j] = annexes[j + 1];
                        annexes[j + 1] = temp;
                    }
                }
            }
        }

        public void BubbleSortByStudents(List<Annex> annexes)
        {
            int n = annexes.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (annexes[j].AvailableStudents > annexes[j + 1].AvailableStudents)
                    {
                        Annex temp = annexes[j];
                        annexes[j] = annexes[j + 1];
                        annexes[j + 1] = temp;
                    }
                }
            }
        }
        //Mege Sort
        public void MergeSortByDistance(List<Annex> annexes)
        {
            if (annexes.Count <= 1) return;

            int mid = annexes.Count / 2;
            List<Annex> left = annexes.GetRange(0, mid);
            List<Annex> right = annexes.GetRange(mid, annexes.Count - mid);

            MergeSortByDistance(left);
            MergeSortByDistance(right);

            annexes.Clear();
            annexes.AddRange(MergeD(left, right));
        }

        private List<Annex> MergeD(List<Annex> left, List<Annex> right)
        {
            List<Annex> result = new List<Annex>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i].Distance <= right[j].Distance)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));
            return result;
        }

        public void MergeSortByCharges(List<Annex> annexes)
        {
            if (annexes.Count <= 1) return;

            int mid = annexes.Count / 2;
            List<Annex> left = annexes.GetRange(0, mid);
            List<Annex> right = annexes.GetRange(mid, annexes.Count - mid);

            MergeSortByCharges(left);
            MergeSortByCharges(right);

            annexes.Clear();
            annexes.AddRange(Merge(left, right));
        }

        private List<Annex> Merge(List<Annex> left, List<Annex> right)
        {
            List<Annex> result = new List<Annex>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i].MonthlyCharges <= right[j].MonthlyCharges)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));
            return result;
        }

        public void MergeSortByStudents(List<Annex> annexes)
        {
            if (annexes.Count <= 1) return;

            int mid = annexes.Count / 2;
            List<Annex> left = annexes.GetRange(0, mid);
            List<Annex> right = annexes.GetRange(mid, annexes.Count - mid);

            MergeSortByStudents(left);
            MergeSortByStudents(right);

            annexes.Clear();
            annexes.AddRange(MergeS(left, right));
        }

        private List<Annex> MergeS(List<Annex> left, List<Annex> right)
        {
            List<Annex> result = new List<Annex>();
            int i = 0, j = 0;
            while (i < left.Count && j < right.Count)
            {
                if (left[i].AvailableStudents <= right[j].AvailableStudents)
                    result.Add(left[i++]);
                else
                    result.Add(right[j++]);
            }
            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));
            return result;
        }

        //Quick Sort
        public void QuickSortByDistance(List<Annex> annexes, int low, int high)
        {
            if (low < high)
            {
                int pi = PartitionD(annexes, low, high);
                QuickSortByDistance(annexes, low, pi - 1);
                QuickSortByDistance(annexes, pi + 1, high);
            }
        }

        private int PartitionD(List<Annex> annexes, int low, int high)
        {
            double pivot = annexes[high].Distance;
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (annexes[j].Distance >= pivot)
                {
                    i++;
                    (annexes[i], annexes[j]) = (annexes[j], annexes[i]);
                }
            }
            (annexes[i + 1], annexes[high]) = (annexes[high], annexes[i + 1]);
            return i + 1;
        }

        public void QuickSortByCharges(List<Annex> annexes, int low, int high)
        {
            if (low < high)
            {
                int pi = PartitionC(annexes, low, high);
                QuickSortByCharges(annexes, low, pi - 1);
                QuickSortByCharges(annexes, pi + 1, high);
            }
        }

        private int PartitionC(List<Annex> annexes, int low, int high)
        {
            double pivot = annexes[high].MonthlyCharges;
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (annexes[j].MonthlyCharges >= pivot)
                {
                    i++;
                    (annexes[i], annexes[j]) = (annexes[j], annexes[i]);
                }
            }
            (annexes[i + 1], annexes[high]) = (annexes[high], annexes[i + 1]);
            return i + 1;
        }

        public void QuickSortByStudents(List<Annex> annexes, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(annexes, low, high);
                QuickSortByStudents(annexes, low, pi - 1);
                QuickSortByStudents(annexes, pi + 1, high);
            }
        }

        private int Partition(List<Annex> annexes, int low, int high)
        {
            int pivot = annexes[high].AvailableStudents;
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (annexes[j].AvailableStudents >= pivot)
                {
                    i++;
                    (annexes[i], annexes[j]) = (annexes[j], annexes[i]);
                }
            }
            (annexes[i + 1], annexes[high]) = (annexes[high], annexes[i + 1]);
            return i + 1;
        }

        public void DisplayAnnexes()
        {
            if (Head == null)
            {
                Console.WriteLine("No annexes available.");
                return;
            }

            // Define the header and separator with fixed widths for each column.
            string header = string.Format(
                "| {0,-20} | {1,-22} | {2,-14} | {3,-20} | {4,-18} | {5,-20} |",
                "Name", "Location", "Distance (km)", "Monthly Charges(Rs.)", "Available Students", "Contact Number");
            string separator = new string('-', header.Length);

            Console.WriteLine(separator);
            Console.WriteLine(header);
            Console.WriteLine(separator);

            // Traverse the linked list and print each annex in the table.
            Annex current = Head;
            while (current != null)
            {
                Console.WriteLine(string.Format(
                    "| {0,-20} | {1,-22} | {2,-14} | {3,-20} | {4,-18} | {5,-20} |",
                    current.Name,
                    current.Location,
                    current.Distance,
                    current.MonthlyCharges,
                    current.AvailableStudents,
                    current.ContactNumber));
                current = current.Next;
            }

            Console.WriteLine(separator);
        
        }

        public List<Annex> GetAnnexesList()
        {
            List<Annex> annexes = new List<Annex>();
            Annex current = Head;
            while (current != null)
            {
                annexes.Add(current);
                current = current.Next;
            }
            return annexes;
        }

        public void EditAnnex(string name, string ownerName)
        {
            Annex current = Head;

            while (current != null && (current.Name != name || current.OwnerName != ownerName))
            {
                current = current.Next;
            }

            if (current == null)
            {
                Console.WriteLine("Annex not found or you don't have permission to edit this annex.");
                return;
            }

            Console.WriteLine("Editing annex: " + current.Name);
            Console.Write("Enter new Location: ");
            current.Location = Console.ReadLine();
            Console.Write("Enter new Distance from Campus (km): ");
            current.Distance = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter new Monthly Charges (Rs.): ");
            current.MonthlyCharges = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter new Available Student Capacity: ");
            current.AvailableStudents = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter new Contact Number: ");
            current.ContactNumber = Console.ReadLine();

            Console.WriteLine("Annex updated successfully.");
        }

        public void RemoveAnnex(string name, string ownerName)
        {
            Annex current = Head;
            Annex previous = null;

            while (current != null && (current.Name != name || current.OwnerName != ownerName))
            {
                previous = current;
                current = current.Next;
            }

            if (current == null)
            {
                Console.WriteLine("Annex not found or you don't have permission to remove this annex.");
                return;
            }

            if (previous == null)
            {
                Head = current.Next;
            }
            else
            {
                previous.Next = current.Next;
            }
            Console.WriteLine("Annex removed successfully.");
        }
    }
}
