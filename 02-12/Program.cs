/*
//* PART1
Given (online) a dataset of numbers, where each line is a report containing a list of numbers (levels) that are separated by spaces.
Determine which reports are safe. A report is considered safe if both of the following are true:
- The levels are either all increasing or all decreasing.
- Any two adjacent levels differ by at least one and at most three (1 and 3 inclusive).
How many reports are safe?
*/

namespace _02_12;

class Program
{
    static void Main()
    {
        //* ------------------------------- Preparation ------------------------------ */
        //# get data from the link and save it to new file input-day1.txt
        //# curl -H "Cookie: session=..." https://adventofcode.com/2024/day/2/input > input-day2.txt

        //# read the file input-day2.txt
        string filePath = "input-day2.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file not found!");
            return;
        }
        string[]? lines = File.ReadAllLines(filePath);
        int count = lines.Length;
        // int count = 20; //for short testing

        //# Create an array of arrays (reports)
        int[][]? reports = new int[count][];
        // Create a list of lists (for only safe reports)
        // List<List<int>> safeList = new List<List<int>>();
        int safeCount = 0;
        bool isError = false;

        for (int i = 0; i < count; i++)
        {
            //# split line by spaces and remove them
            string[] parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            //# Initialize the sub-array
            reports[i] = new int[parts.Length];

            //* ------------------------------- SOLUTION 1 ------------------------------- */
            bool isAsc = false;
            bool isDesc = false;
            bool isSafe = true;

            //# Convert string "parts" to an array of numbers and add to general reports array
            for (int j = 0; j < parts.Length; j++)
            {
                int.TryParse(parts[j], out int level);
                reports[i][j] = level;

                if (j > 0) //no manipulation with the first level(number) in the report
                {
                    //# Checking the difference
                    if ((Math.Abs(level - reports[i][j - 1]) < 1 || Math.Abs(level - reports[i][j - 1]) > 3))
                    {
                        isSafe = false;
                        break;
                    }
                    else
                    {
                        //# Checking for monotonicity for ASC
                        if (level > reports[i][j - 1])
                        {
                            if (isDesc) //# if monotonicity is broken
                            {
                                isSafe = false;
                                break;
                            }
                            else
                            {
                                isAsc = true;
                            }
                        }
                        //# Checking for monotonicity for DESC
                        if (level < reports[i][j - 1])
                        {
                            if (isAsc) //# if monotonicity is broken
                            {
                                isSafe = false;
                                break;
                            }
                            else
                            {
                                isDesc = true;
                            }
                        }
                    }
                }
            }
            if (isAsc && isDesc)
            {
                isError = true;
                break;
            }
            if ((isAsc || isDesc) && isSafe)
            {
                // safeList.Add(new List<int>(reports[i]));
                safeCount++;
            }
        }
        if (isError)
        {
            Console.WriteLine("Some logical error happened!");
        }
        else
        {
            // Output of safeList
            // for (int k = 0; k < safeList.Count; k++)
            // {
            //     Console.WriteLine($"safeList[{k}]: {string.Join(", ", safeList[k])}");
            // }
            Console.WriteLine("Count of safe reports: " + safeCount); //516
        }
    }
}
