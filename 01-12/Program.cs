/* 
//*PART1
A text file containing a few thousand rows of two integers, separated by a spaces and/or \t.
Import the file as an array of text, each row is an element with two pairs of numbers.
Pair up the smallest number in the left list with the smallest number in the right list, 
then the second-smallest left number with the second-smallest right number, and so on.

Within each pair, figure out how far (Absolute) apart the two numbers are.
To find the total distance between the left list and the right list, add up(Sum) the distances between all of the pairs.


//*PART2
Figure out exactly how often each number from the left list appears in the right list. 
Calculate a total similarity score by adding up each number in the left list after multiplying it by the number of times that number appears in the right list.
*/
namespace _01_12;

internal class Program
{
    public static void Main()
    {
        //* ------------------------------- Preparation ------------------------------ */
        //# get data from the link and save it to new file input-day1.txt
        //# curl -H "Cookie: session=..." https://adventofcode.com/2024/day/1/input > input-day1.txt

        //# read the file input-day1.txt
        string[]? lines = File.ReadAllLines("input-day1.txt");
        int count = lines.Length;
        // int count = 10; //for short testing

        //to check if we get some data from the file
        //Console.WriteLine(lines[0]);

        int[]? column1 = new int[count];
        int[]? column2 = new int[count];

        for (int i = 0; i < count; i++)
        {
            //# split line by spaces and/or tabs and remove them: Split(' ') / Split(new[] { ' ', '\t' }) if several separators
            //# var parts = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string[]? parts = lines[i].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);

            //# out int num1: Declares a new variable num1 directly inside the method call.
            //# After TryParse, the variable will either contain the converted number or returns false.
            if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
            {
                column1[i] = num1;
                column2[i] = num2;
            }
        }
        //* -------------------------------- SOLUTION 1 ------------------------------- */
        //# 1. Sorting array - Array.Sort(array); / Array.Reverse(array); // original array mutated !!!
        //# 2. Sorting list -  list.Sort(); /  list.Sort((x, y) => y.CompareTo(x)); // original list mutated !!!
        //# 3. Universal LINQ method for sorting:   // create new array/list with sorted result, original array/list is UNmutated!
        //#     using System.Linq;
        //#
        //#     int[] array = { 5, 2, 8, 3, 1 };
        //#     List<int> list = new List<int> { 5, 2, 8, 3, 1 };
        //# 
        //#     Ascending sort
        //#     var sortedArray = array.OrderBy(x => x).ToArray();
        //#     var sortedList = list.OrderBy(x => x).ToList();
        //# 
        //#     Descending sort
        //#     var descArray = array.OrderByDescending(x => x).ToArray();
        //#     var descList = list.OrderByDescending(x => x).ToList();

        Array.Sort(column1);
        Array.Sort(column2);

        //# Zip method processes two arrays/lists in parallel at once.
        //# If the arrays/lists are of different lengths, Zip takes elements only up to the end of the shortest array/list.
        //# In this case, it takes the elements from column1 and column2 IN PAIRS!

        var differencesArr = column1.Zip(column2, (a, b) => Math.Abs(a - b)).ToArray();

        int result = differencesArr.Sum();
        Console.WriteLine("Total distance: " + result); //2285373




        //* -------------------------------- SOLUTION 2 ------------------------------- */

        //# Dictionary to count the number of each number in the right list
        var rightCount = column2.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        // foreach (var item in rightCount)
        // {
        //     Console.WriteLine(item); 
        //     [33335, 1] num as key, count as value
        // }

        // Console.WriteLine(string.Join(", ", rightCount)); //short version of code above


        //# Calculating the total similarity sum
        int totalSimilarity = 0;
        foreach (var num in column1)
        {
            if (rightCount.ContainsKey(num))
            {
                totalSimilarity += num * rightCount[num];
            }
        }
        Console.WriteLine("Total similarity: " + totalSimilarity); //21142653
    }
}
