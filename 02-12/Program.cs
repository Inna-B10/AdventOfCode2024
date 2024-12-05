/*
//* PART1
Given (online) a dataset of numbers, where each line is a report containing a list of numbers (levels) that are separated by spaces.
Determine which reports are safe. A report is considered safe if both of the following are true:
- The levels are either all increasing or all decreasing.
- Any two adjacent levels differ by at least one and at most three (1 and 3 inclusive).
How many reports are safe?
//* PART2
The same rules apply as before + situations where removing a single level from an unsafe report would make it safe, the report instead counts as safe.
How many reports are safe?
*/
namespace _02_12;

class Program
{
    static void Main()
    {
        //* ------------------------------- Preparation ------------------------------ */
        //# get data from the link and save it to new file input-day2.txt
        //# curl -H "Cookie: session=..." https://adventofcode.com/2024/day/2/input > input-day2.txt

        //# read the file input-day2.txt
        string filePath = "input-day2.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file not found!");
            return;
        }
        int safeCount = 0;

        //* ------------------------------- SOLUTION 1 ------------------------------- */
        string[]? lines = File.ReadAllLines(filePath);
        int count = lines.Length;
        // int count = 20; //for short testing

        bool isError = false;

        //# Create an array of arrays (reports)
        int[][]? reports = new int[count][];
        // Create a list of lists (for only safe reports)
        // List<List<int>> safeList = new List<List<int>>();

        for (int i = 0; i < count; i++)
        {
            //# split line by spaces and remove them
            string[] parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            //# Initialize the sub-array
            reports[i] = new int[parts.Length];

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
        //* ----------------- SOLUTION 2 (and Variant2 For Solution1) ---------------- */
        safeCount = 0;
        var rows = File.ReadLines(filePath);
        // var rows = File.ReadLines(filePath).Take(20); //# limit 20rows for testing

        //# Create list with levels from one row
        //# в течении цикла работаем только с одной строкой из файла. Создаем лист/массив из чисел(levels), делаем все проверки.
        //# обновляем safeCount при выполнении условия. При следующем цикле создается новый лист из следующей строки.
        //# таким образом, одновременно существует только один rowOfLevels(report) в виде [16, 19, 21, 24, 21]
        foreach (var row in rows)
        {
            var rowOfLevels = row.Split(' ').Select(int.Parse).ToList();
            // Console.WriteLine(string.Join(", ", rowOfLevels));
            // Console.WriteLine(rowOfLevels[0]); //# list works with index exactly as array

            if (isReportSafe(rowOfLevels) || isCanBeSafe(rowOfLevels))
            {
                safeCount++;
            }
        }
        Console.WriteLine("New count of safe reports: " + safeCount); //561

        bool isReportSafe(List<int> report)
        {
            //# if only one or none number in the row
            if (report.Count < 2)
            {
                return false;
            }
            //# determine (by comparing the first 2 numbers) whether the row "should" be in ascending or descending order
            bool isAsc = report[1] > report[0];

            for (int i = 1; i < report.Count; i++)
            {
                //# Absolute distance
                int diff = Math.Abs(report[i] - report[i - 1]);

                if (diff < 1 || diff > 3 || (report[i] > report[i - 1] != isAsc))
                {
                    return false;
                }

            }
            return true;
        }

        bool isCanBeSafe(List<int> report)
        {
            if (report.Count() < 2)
            {
                return false;
            }
            //# В каждом цикле создаем новый лист с числами, исключая число с текущим индексом
            //# и проверяем стал ли новый лист(рапорт) безопасным.
            //# если да, выходим из цикла и возвращаем true
            for (int i = 0; i < report.Count; i++)
            {
                //# create a new list of levels without level with current index
                //# and check is this new list safe or not
                var modLevelsArr = report.Where((_, index) => index != i).ToList();

                if (isReportSafe(modLevelsArr))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
