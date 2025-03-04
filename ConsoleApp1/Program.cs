using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using ConsoleApp1;



AnnexManager annexList = new AnnexManager();
UserManager userManager = new UserManager();

// Path to the JSON file
string filePath = "annex.json"; // Ensure the file is in the correct directory or provide the full path

// Read and deserialize the JSON file
List<Annex> annexesFromFile = ReadAnnexesFromJson(filePath);
foreach (var annex in annexesFromFile)
{
    annexList.AddAnnex(
        annex.Name,
        annex.Location,
        annex.OwnerName,
        annex.Distance,
        annex.MonthlyCharges,
        annex.AvailableStudents,
        annex.ContactNumber
    );
}

static List<Annex> ReadAnnexesFromJson(string filePath)
{
    try
    {
        // Read the JSON file
        string json = File.ReadAllText(filePath);

        // Deserialize the JSON into a list of Annex objects
        List<Annex> annexes = JsonConvert.DeserializeObject<List<Annex>>(json);
        return annexes;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading JSON file: {ex.Message}");
        return new List<Annex>();
    }
}


    Stopwatch stopwatch = new Stopwatch();



while (true)
{
    Console.Clear();

    Console.WriteLine("===================================");
    Console.WriteLine("  WELCOME TO ACCOMMODATION FINDER");
    Console.WriteLine("===================================\n");


    Console.Write("Are you a finder or owner? ");
    Console.Write("\n1. Owner\n2. Finder\n3. Exit\nEnter your choice : ");
    string rolenum = Console.ReadLine().ToLower();
    string role;
    role = rolenum;
    if (rolenum == "1") { role = "owner"; }
    if (rolenum == "2") {role = "finder"; }
    
    while (role != "owner" && role != "finder" && role != "3")
        {
        Console.WriteLine("Invalid input. Press any key to return Menu...");
        Console.ReadKey();
        Console.Clear();

        Console.WriteLine("===================================");
        Console.WriteLine("  WELCOME TO ACCOMMODATION FINDER");
        Console.WriteLine("===================================\n");
        Console.WriteLine("Are you a finder or owner? ");
        Console.Write("\n1. Owner\n2. Finder\n3. Exit\nEnter your choice : ");
        role = Console.ReadLine().ToLower();
        if (rolenum == "1") { role = "owner"; }
        if (rolenum == "2") { role = "finder"; }
    }
    if (role == "3") { return; }


    Console.Write("Do you have an account? (Y/N) ");
    string hasAccount = Console.ReadLine().ToLower();

    User currentUser = null;

    if (hasAccount == "Y"|| hasAccount == "y")
    {
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        currentUser = userManager.Login(email, password, role);
        if (currentUser == null)
        {
            Console.WriteLine("Invalid login credentials or role mismatch. Press any key to retry...");
            Console.ReadKey();
            continue;
        }
    }
    else if (hasAccount == "N"|| hasAccount == "n") 
    {
        Console.Write("Enter New Username: ");
        string username = Console.ReadLine();
        Console.Write("Enter New Email: ");
        string email = Console.ReadLine();
        Console.Write("Enter New Password: ");
        string password = Console.ReadLine();
        currentUser = userManager.Signup(username, email, password, role);
    }
    else
    {
        Console.WriteLine("Invalid Input. Press any key to retry...");
        Console.ReadKey();
        continue;
    }
    

    if (currentUser.Role == "owner")
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===================================");
            Console.WriteLine("           OWNER MENU              ");
            Console.WriteLine("===================================\n");

            Console.WriteLine("1. Add Annex\n2. View Annexes\n3. Edit Annex\n4. Remove Annex\n5. Logout");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Write("Enter Annex Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Location: ");
                string location = Console.ReadLine();
                string ownerName = currentUser.Username;
                Console.Write("Enter Distance from Campus (km): ");
                double distance = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Monthly Charges (Rs.): ");
                double charges = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter Available Student Capacity: ");
                int students = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Contact Number: ");
                string contactNumber = Console.ReadLine();
                annexList.AddAnnex(name, location, ownerName, distance, charges, students, contactNumber);
            }
            else if (choice == "2")
            {
                
                annexList.DisplayAnnexes();
                Console.WriteLine("Please any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "3")
            {
                Console.Write("Enter the name of the annex to edit: ");
                string name = Console.ReadLine();
                annexList.EditAnnex(name, currentUser.Username);
                Console.WriteLine("Please any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "4")
            {
                Console.Write("Enter the name of the annex to remove: ");
                string name = Console.ReadLine();
                annexList.RemoveAnnex(name, currentUser.Username);
                Console.WriteLine("Please any key to continue...");
                Console.ReadKey();
            }
            else if (choice == "5")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to return Owner Menu...");
                Console.ReadKey();
            }
        }
    }
    else if (currentUser.Role == "finder")
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("===================================");
            Console.WriteLine("           FINDER MENU             ");
            Console.WriteLine("===================================\n");

            Console.WriteLine("1. View Annexes\n2. Search Annexes\n3. Logout");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                annexList.DisplayAnnexes();
                Console.ReadKey();
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nSearch by: 1. Distance\n\t   2. Monthly Charges\n\t   3. Available Students");
                Console.Write("Enter your choice: ");
                string searchChoice1 = Console.ReadLine();
                string header = string.Format(
                    "| {0,-20} | {1,-22} | {2,-14} | {3,-20} | {4,-18} | {5,-20} |",
                    "Name", "Location", "Distance (km)", "Monthly Charges(Rs.)", "Available Students", "Contact Number");
                string separator = new string('-', header.Length);
                if (searchChoice1 == "1")
                {

                    List<Annex> annexes = annexList.GetAnnexesList();
                    Console.Write("Enter max distance: ");
                    double maxDistance = Convert.ToDouble(Console.ReadLine());
                    // Start the stopwatch
                    stopwatch.Start();
                    annexList.QuickSortByDistance(annexes, 0, annexes.Count - 1);
                    // Stop the stopwatch
                    stopwatch.Stop();

                    Console.WriteLine(separator);
                    Console.WriteLine(header);
                    Console.WriteLine(separator);
                    annexes.Where(a => a.Distance <= maxDistance).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                    Console.WriteLine(separator);
                    // Print the elapsed time
                    Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
                    List<Annex> annexesDi1 = new List<Annex>();
                    foreach (var annex in annexes)
                    {
                        if (annex.Distance <= maxDistance)
                        {
                            annexesDi1.Add(annex);
                        }
                    }

                    Console.WriteLine("\nSearch by: 1. Monthly Charges\n\t   2. Available Students\n\t   3. Back");
                    Console.Write("Enter your choice: ");
                    string searchChoice2 = Console.ReadLine();
                    if (searchChoice2 == "1")
                    {
                        Console.Write("Enter max monthly charge(Rs.): ");
                        double maxCharge = Convert.ToDouble(Console.ReadLine());
                        // Start the stopwatch
                        stopwatch.Start();
                        annexList.MergeSortByCharges(annexesDi1);
                        // Stop the stopwatch
                        stopwatch.Stop();

                        Console.WriteLine(separator);
                        Console.WriteLine(header);
                        Console.WriteLine(separator);
                        annexesDi1.Where(a => a.MonthlyCharges <= maxCharge).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                        Console.WriteLine(separator);
                        // Print the elapsed time
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        Console.WriteLine("\nSearch by: 1. Available Students\n\t   2. Back");
                        Console.Write("Enter your choice: ");
                        string searchChoice3 = Console.ReadLine();
                        List<Annex> annexesCh2 = new List<Annex>();
                        foreach (var annexz in annexesDi1)
                        {
                            if (annexz.MonthlyCharges <= maxCharge)
                            {
                                annexesCh2.Add(annexz);
                            }
                        }
                        if (searchChoice3 == "1")
                        {
                            Console.Write("Enter maximum student capacity: ");
                            int maxStudents = Convert.ToInt32(Console.ReadLine());
                            // Start the stopwatch
                            stopwatch.Start();
                            annexList.BubbleSortByStudents(annexesCh2);
                            // Stop the stopwatch
                            stopwatch.Stop();

                            Console.WriteLine(separator);
                            Console.WriteLine(header);
                            Console.WriteLine(separator);
                            annexesCh2.Where(a => a.AvailableStudents >= maxStudents).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                            Console.WriteLine(separator);
                            // Print the elapsed time
                            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
                        }
                        else if (searchChoice3 == "2")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                            Console.ReadKey();
                        }
                    }
                    else if (searchChoice2 == "2")
                    {
                        Console.Write("Enter maximum student capacity: ");
                        int maxStudents = Convert.ToInt32(Console.ReadLine());
                        // Start the stopwatch
                        stopwatch.Start();
                        annexList.MergeSortByStudents(annexesDi1);
                        // Stop the stopwatch
                        stopwatch.Stop();

                        Console.WriteLine(separator);
                        Console.WriteLine(header);
                        Console.WriteLine(separator);
                        annexesDi1.Where(a => a.AvailableStudents >= maxStudents).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                        Console.WriteLine(separator);
                        // Print the elapsed time
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        Console.WriteLine("\nSearch by: 1. Monthly Charges\n\t   2. Back");
                        Console.Write("Enter your choice: ");
                        string searchChoice3 = Console.ReadLine();
                        List<Annex> annexesSt2 = new List<Annex>();
                        foreach (var annexx in annexesDi1)
                        {
                            if (annexx.AvailableStudents >= maxStudents)
                            {
                                annexesSt2.Add(annexx);
                            }
                        }
                        if (searchChoice3 == "1")
                        {
                            Console.Write("Enter max monthly charge(Rs.): ");
                            double maxCharge = Convert.ToDouble(Console.ReadLine());
                            // Start the stopwatch
                            stopwatch.Start();
                            annexList.BubbleSortByCharges(annexesSt2);
                            // Stop the stopwatch
                            stopwatch.Stop();

                            Console.WriteLine(separator);
                            Console.WriteLine(header);
                            Console.WriteLine(separator);
                            annexesSt2.Where(a => a.MonthlyCharges <= maxCharge).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                            Console.WriteLine(separator);
                            // Print the elapsed time
                            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
                        }
                        else if (searchChoice3 == "2")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                            Console.ReadKey();
                        }
                    }
                    else if (searchChoice2 == "3")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                        Console.ReadKey();
                    }
                    Console.WriteLine("Please any key to continue...");
                    Console.ReadKey();
                }
                else if (searchChoice1 == "2")
                {

                    List<Annex> annexes2 = annexList.GetAnnexesList();
                    Console.Write("Enter max monthly charge(Rs.): ");
                    double maxCharge = Convert.ToDouble(Console.ReadLine());
                    // Start the stopwatch
                    stopwatch.Start();
                    annexList.QuickSortByCharges(annexes2, 0, annexes2.Count - 1);
                    // Stop the stopwatch
                    stopwatch.Stop();

                    Console.WriteLine(separator);
                    Console.WriteLine(header);
                    Console.WriteLine(separator);
                    annexes2.Where(a => a.MonthlyCharges <= maxCharge).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                    Console.WriteLine(separator);
                    // Print the elapsed time
                    Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                    List<Annex> annexesCh1 = new List<Annex>();
                    foreach (var annexy in annexes2)
                    {
                        if (annexy.MonthlyCharges <= maxCharge)
                        {
                            annexesCh1.Add(annexy);
                        }
                    }

                    Console.WriteLine("\nSearch by: 1. Distance (km)\n\t   2. Available Students\n\t   3. Back");
                    Console.Write("Enter your choice: ");
                    
                    string searchChoice2 = Console.ReadLine();
                    if (searchChoice2 == "1")
                    {
                        Console.Write("Enter max distance: ");
                        double maxDistance = Convert.ToDouble(Console.ReadLine());
                        // Start the stopwatch
                        stopwatch.Start();
                        annexList.MergeSortByDistance(annexesCh1);
                        // Stop the stopwatch
                        stopwatch.Stop();

                        Console.WriteLine(separator);
                        Console.WriteLine(header);
                        Console.WriteLine(separator);
                        annexesCh1.Where(a => a.Distance <= maxDistance).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                        Console.WriteLine(separator);
                        // Print the elapsed time
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        List<Annex> annexesDi2 = new List<Annex>();
                        foreach (var annext in annexesCh1)
                        {
                            if (annext.Distance <= maxDistance)
                            {
                                annexesDi2.Add(annext);
                            }
                        }

                        Console.WriteLine("\nSearch by: 1. Available Students\n\t   2. Back");
                        Console.Write("Enter your choice: ");
                        string searchChoice3 = Console.ReadLine();
                        if (searchChoice3 == "1")
                        {
                            Console.Write("Enter maximum student capacity: ");
                            int maxStudents = Convert.ToInt32(Console.ReadLine());
                            // Start the stopwatch
                            stopwatch.Start();
                            annexList.BubbleSortByStudents(annexesDi2);
                            // Stop the stopwatch
                            stopwatch.Stop();

                            Console.WriteLine(separator);
                            Console.WriteLine(header);
                            Console.WriteLine(separator);
                            annexesDi2.Where(a => a.AvailableStudents >= maxStudents).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                            Console.WriteLine(separator);
                            // Print the elapsed time
                            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        }
                        else if (searchChoice3 == "2")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                            Console.ReadKey();
                        }

                    }
                    else if (searchChoice2 == "2")
                    {
                        Console.Write("Enter maximum student capacity: ");
                        int maxStudents = Convert.ToInt32(Console.ReadLine());
                        // Start the stopwatch
                        stopwatch.Start();
                        annexList.MergeSortByStudents(annexesCh1);
                        // Stop the stopwatch
                        stopwatch.Stop();

                        Console.WriteLine(separator);
                        Console.WriteLine(header);
                        Console.WriteLine(separator);
                        annexesCh1.Where(a => a.AvailableStudents >= maxStudents).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                        Console.WriteLine(separator);
                        // Print the elapsed time
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        List<Annex> annexesSt22 = new List<Annex>();
                        foreach (var annex in annexesCh1)
                        {
                            if (annex.AvailableStudents >= maxStudents)
                            {
                                annexesSt22.Add(annex);
                            }
                        }

                        Console.WriteLine("\nSearch by: 1. Distance\n\t   2. Back");
                        Console.Write("Enter your choice: ");
                        string searchChoice3 = Console.ReadLine();
                        if (searchChoice3 == "1")
                        {
                            Console.Write("Enter max distance: ");
                            double maxDistance = Convert.ToDouble(Console.ReadLine());
                            // Start the stopwatch
                            stopwatch.Start();
                            annexList.BubbleSortByDistance(annexesSt22);
                            // Stop the stopwatch
                            stopwatch.Stop();

                            Console.WriteLine(separator);
                            Console.WriteLine(header);
                            Console.WriteLine(separator);
                            annexesSt22.Where(a => a.Distance <= maxDistance).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                            Console.WriteLine(separator);
                            // Print the elapsed time
                            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        }
                        else if (searchChoice3 == "2")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                            Console.ReadKey();
                        }
                    }
                    else if (searchChoice2 == "3")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                        Console.ReadKey();
                    }

                    Console.WriteLine("Please any key to continue...");
                    Console.ReadKey();
                }
                else if (searchChoice1 == "3")
                {
                    List<Annex> annexes3 = annexList.GetAnnexesList();
                    Console.Write("Enter maximum student capacity: ");
                    int maxStudents = Convert.ToInt32(Console.ReadLine());
                    // Start the stopwatch
                    stopwatch.Start();
                    annexList.QuickSortByStudents(annexes3, 0, annexes3.Count - 1);
                    // Stop the stopwatch
                    stopwatch.Stop();

                    Console.WriteLine(separator);
                    Console.WriteLine(header);
                    Console.WriteLine(separator);
                    annexes3.Where(a => a.AvailableStudents >= maxStudents).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                    Console.WriteLine(separator);
                    // Print the elapsed time
                    Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                    List<Annex> annexesSt1 = new List<Annex>();
                    foreach (var annex in annexes3)
                    {
                        if (annex.AvailableStudents >= maxStudents)
                        {
                            annexesSt1.Add(annex);
                        }
                    }

                    Console.WriteLine("\nSearch by: 1. Distance (km)\n\t   2. Monthly Charges\n\t   3. Back");
                    Console.Write("Enter your choice: ");
                    string searchChoice2 = Console.ReadLine();
                    if (searchChoice2 == "1")
                    {
                        Console.Write("Enter max distance: ");
                        double maxDistance = Convert.ToDouble(Console.ReadLine());
                        // Start the stopwatch
                        stopwatch.Start();
                        annexList.MergeSortByDistance(annexesSt1);
                        // Stop the stopwatch
                        stopwatch.Stop();

                        Console.WriteLine(separator);
                        Console.WriteLine(header);
                        Console.WriteLine(separator);
                        annexesSt1.Where(a => a.Distance <= maxDistance).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                        Console.WriteLine(separator);
                        // Print the elapsed time
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        List<Annex> annexesDi22 = new List<Annex>();
                        foreach (var annex in annexesSt1)
                        {
                            if (annex.Distance <= maxDistance)
                            {
                                annexesDi22.Add(annex);
                            }
                        }

                        Console.WriteLine("\nSearch by: 1. Monthly Charges\n\t   2. Back");
                        Console.Write("Enter your choice: ");
                        string searchChoice3 = Console.ReadLine();
                        if (searchChoice3 == "1")
                        {
                            Console.Write("Enter max monthly charge(Rs.): ");
                            double maxCharge = Convert.ToDouble(Console.ReadLine());
                            // Start the stopwatch
                            stopwatch.Start();
                            annexList.BubbleSortByCharges(annexesDi22);
                            // Stop the stopwatch
                            stopwatch.Stop();

                            Console.WriteLine(separator);
                            Console.WriteLine(header);
                            Console.WriteLine(separator);
                            annexesDi22.Where(a => a.MonthlyCharges <= maxCharge).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                            Console.WriteLine(separator);
                            // Print the elapsed time
                            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        }
                        else if (searchChoice3 == "2")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                            Console.ReadKey();
                        }
                    }

                    else if (searchChoice2 == "2")
                    {
                        Console.Write("Enter max monthly charge(Rs.): ");
                        double maxCharge = Convert.ToDouble(Console.ReadLine());
                        // Start the stopwatch
                        stopwatch.Start();
                        annexList.MergeSortByCharges(annexesSt1);
                        // Stop the stopwatch
                        stopwatch.Stop();

                        Console.WriteLine(separator);
                        Console.WriteLine(header);
                        Console.WriteLine(separator);
                        annexesSt1.Where(a => a.MonthlyCharges <= maxCharge).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                        Console.WriteLine(separator);
                        // Print the elapsed time
                        Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

                        List<Annex> annexesCh22 = new List<Annex>();
                        foreach (var annex in annexesSt1)
                        {
                            if (annex.MonthlyCharges <= maxCharge)
                            {
                                annexesCh22.Add(annex);
                            }
                        }

                        Console.WriteLine("\nSearch by: 1. Distance\n\t   2. Back");
                        Console.Write("Enter your choice: ");
                        string searchChoice3 = Console.ReadLine();
                        if (searchChoice3 == "1")
                        {
                            Console.Write("Enter max distance: ");
                            double maxDistance = Convert.ToDouble(Console.ReadLine());
                            // Start the stopwatch
                            stopwatch.Start();
                            annexList.BubbleSortByDistance(annexesCh22);
                            // Stop the stopwatch
                            stopwatch.Stop();

                            Console.WriteLine(separator);
                            Console.WriteLine(header);
                            Console.WriteLine(separator);
                            annexesCh22.Where(a => a.Distance <= maxDistance).ToList().ForEach(a => Console.WriteLine($"| {a.Name,-20} | {a.Location,-22} | {a.Distance,-14} | {a.MonthlyCharges,-20} | {a.AvailableStudents,-18} | {a.ContactNumber,-20} |"));
                            Console.WriteLine(separator);
                            // Print the elapsed time
                            Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
                        }
                        else if (searchChoice3 == "2")
                        {
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                            Console.ReadKey();
                        }
                    }
                    else if (searchChoice1 == "3")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
                        Console.ReadKey();
                    }
                    Console.WriteLine("Please any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input. Press any key to go back...");
                    Console.ReadKey();
                }
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
            Console.WriteLine("Invalid input. Press any key to return Finder Menu...");
            Console.ReadKey();
            }
            
        }
    }
}
