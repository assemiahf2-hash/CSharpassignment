using System;

namespace StudentResultsProcessingSystem
{
    // ---------------------------------------------------------------------------
    // Student Results Processing System
    // A beginner-to-intermediate C# console application that records details and
    // scores for a number of students, then calculates each student's total,
    // average, grade and final academic status.
    // ---------------------------------------------------------------------------
    class Program
    {
        // The five courses every student is scored on. Stored here so they are
        // easy to reuse and easy to change in one place.
        static readonly string[] Courses =
        {
            "Operating Systems",
            "Programming with Java",
            "Information Theory",
            "Computer Architecture",
            "Digital Systems"
        };

        // The minimum number of students the assignment requires.
        const int MinStudents = 3;

        static void Main(string[] args)
        {
            // Greet the user once at the start of the program.
            Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");

            // We use arrays to store every student's data so that we can show a
            // full report later. The actual number of students is decided by the
            // user (must be at least MinStudents).
            string[] names      = new string[0];
            string[] ids        = new string[0];
            string[] programmes = new string[0];
            string[] levels     = new string[0];
            double[,] scores    = new double[0, 0];
            double[] totals     = new double[0];
            double[] averages   = new double[0];

            bool hasData = false;

            // ----- Main menu loop -------------------------------------------------
            // The menu keeps showing until the user chooses to exit.
            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                // If input ends (e.g. Ctrl+Z), exit gracefully instead of looping.
                if (choice == null)
                {
                    Console.WriteLine("Thank you for using the Student Results Processing System.");
                    return;
                }

                switch (choice)
                {
                    case "1":
                        // Enter student results.
                        int count = ReadStudentCount();

                        names      = new string[count];
                        ids        = new string[count];
                        programmes = new string[count];
                        levels     = new string[count];
                        scores     = new double[count, Courses.Length];
                        totals     = new double[count];
                        averages   = new double[count];

                        for (int i = 0; i < count; i++)
                        {
                            EnterStudent(i, names, ids, programmes, levels, scores, totals, averages);
                        }

                        hasData = true;
                        Console.WriteLine();
                        Console.WriteLine("Results saved successfully for " + count + " student(s).");
                        break;

                    case "2":
                        // View the full report for all students.
                        if (hasData)
                        {
                            ViewReport(names, ids, programmes, levels, scores, totals, averages);
                        }
                        else
                        {
                            Console.WriteLine("No student data found. Please choose option 1 first.");
                        }
                        break;

                    case "3":
                        // Exit the program.
                        Console.WriteLine("Thank you for using the Student Results Processing System.");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please enter 1, 2 or 3.");
                        break;
                }
            }
        }

        // ----- Menu -------------------------------------------------------------
        // Displays the main menu exactly as specified in the assignment.
        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
            Console.WriteLine();
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");
        }

        // ----- Student count ----------------------------------------------------
        // Reads the number of students to process. Must be at least MinStudents.
        static int ReadStudentCount()
        {
            int count;
            while (true)
            {
                Console.Write("How many students do you want to process? (minimum " + MinStudents + "): ");
                string input = Console.ReadLine();

                // Treat EOF as the minimum valid count so we never hang.
                if (input == null)
                {
                    return MinStudents;
                }

                if (int.TryParse(input, out count) && count >= MinStudents)
                {
                    return count;
                }

                Console.WriteLine("Invalid entry. Please enter a whole number of " + MinStudents + " or more.");
            }
        }

        // ----- Enter one student ------------------------------------------------
        // Collects the personal details and the 5 course scores for a single
        // student, then calculates the total and average.
        static void EnterStudent(int index,
                                 string[] names, string[] ids,
                                 string[] programmes, string[] levels,
                                 double[,] scores, double[] totals, double[] averages)
        {
            Console.WriteLine();
            Console.WriteLine("Enter details for Student " + (index + 1));
            Console.WriteLine();

            Console.Write("Enter full name: ");
            names[index] = Console.ReadLine();

            Console.Write("Enter student ID: ");
            ids[index] = Console.ReadLine();

            Console.Write("Enter programme: ");
            programmes[index] = Console.ReadLine();

            Console.Write("Enter level: ");
            levels[index] = Console.ReadLine();

            Console.WriteLine();

            double total = 0;
            // Loop through every course and read a validated score.
            for (int c = 0; c < Courses.Length; c++)
            {
                scores[index, c] = ReadValidScore(Courses[c]);
                total += scores[index, c];
            }

            // Store the calculated values for this student.
            totals[index] = total;
            averages[index] = total / Courses.Length;
        }

        // ----- Score validation -------------------------------------------------
        // Keeps asking for a score until a value between 0 and 100 is entered.
        // A null (EOF, e.g. Ctrl+Z) is treated as 0 so the program never hangs.
        static double ReadValidScore(string course)
        {
            while (true)
            {
                Console.Write("Enter score for " + course + ": ");
                string input = Console.ReadLine();

                if (input == null)
                {
                    return 0;
                }

                if (double.TryParse(input, out double score) && score >= 0 && score <= 100)
                {
                    return score;
                }

                Console.WriteLine("Invalid score. Score must be between 0 and 100.");
            }
        }

        // ----- Report -----------------------------------------------------------
        // Displays a neat, complete report for every student.
        static void ViewReport(string[] names, string[] ids, string[] programmes, string[] levels,
                               double[,] scores, double[] totals, double[] averages)
        {
            Console.WriteLine();
            Console.WriteLine("===== STUDENT RESULTS REPORT =====");

            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Student Name: " + names[i]);
                Console.WriteLine("Student ID: "   + ids[i]);
                Console.WriteLine("Programme: "    + programmes[i]);
                Console.WriteLine("Level: "        + levels[i]);
                Console.WriteLine();

                for (int c = 0; c < Courses.Length; c++)
                {
                    Console.WriteLine(Courses[c] + ": " + scores[i, c]);
                }

                Console.WriteLine();
                Console.WriteLine("Total Score: "   + totals[i]);
                Console.WriteLine("Average Score: " + averages[i].ToString("0.#"));
                Console.WriteLine("Grade: "         + GetGrade(averages[i]));
                Console.WriteLine("Status: "        + GetStatus(averages[i]));
            }

            // ----- Bonus summary -------------------------------------------------
            // Not scored, but a nice addition to the report.
            ShowSummary(averages, names);
        }

        // ----- Grade ------------------------------------------------------------
        // Returns the letter grade for an average using the official table.
        static string GetGrade(double average)
        {
            if (average >= 80) return "A";
            if (average >= 70) return "B";
            if (average >= 60) return "C";
            if (average >= 50) return "D";
            return "F";
        }

        // ----- Status -----------------------------------------------------------
        // Returns Passed for 50 and above, Failed for anything below 50.
        static string GetStatus(double average)
        {
            return average >= 50 ? "Passed" : "Failed";
        }

        // ----- Bonus: best / lowest / class average ----------------------------
        // Displays the best student, the student with the lowest average and the
        // class (overall) average. These are extras and are not scored.
        static void ShowSummary(double[] averages, string[] names)
        {
            int bestIndex    = 0;
            int lowestIndex  = 0;
            double sum       = 0;

            for (int i = 0; i < averages.Length; i++)
            {
                if (averages[i] > averages[bestIndex])   bestIndex   = i;
                if (averages[i] < averages[lowestIndex]) lowestIndex = i;
                sum += averages[i];
            }

            double classAverage = sum / averages.Length;

            Console.WriteLine();
            Console.WriteLine("----- Summary -----");
            Console.WriteLine("Best student: "          + names[bestIndex]
                              + " (" + averages[bestIndex].ToString("0.#") + ")");
            Console.WriteLine("Lowest average student: " + names[lowestIndex]
                              + " (" + averages[lowestIndex].ToString("0.#") + ")");
            Console.WriteLine("Class average: "         + classAverage.ToString("0.##"));
        }
    }
}
