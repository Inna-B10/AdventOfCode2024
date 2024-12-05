/*
//* PART1
In the text needs to find ALL occurrences of mul(X,Y), where X and Y are each 1-3 digit numbers.
Multiply X by Y.
Sum results of all multiplying.

*/

using System.Text.RegularExpressions;

namespace _03_12;

class Program
{
    static void Main()
    {
        //* ------------------------------- Preparation ------------------------------ */
        //# get data from the link and save it to new file input-day3.txt
        //# curl -H "Cookie: session=..." https://adventofcode.com/2024/day/3/input > input-day3.txt

        //# read the file input-day3.txt
        string filePath = "input-day3.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file not found!");
            return;
        }

        string input = "select()} <*mul(843,597)!~mul(717,524)&?}'mul(928,721)>mul(194,52)'why()]-*select()what(898,458):#*mul(31,582)mul(209,470)'-mul(834,167)>}";

        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        Regex regex = new(pattern);
        MatchCollection matches = regex.Matches(input);

        foreach (Match item in matches)
        {
            string fullItem = item.Value;
            string full = item.Groups[0].Value;
            var x = int.Parse(item.Groups[1].Value);
            var y = int.Parse(item.Groups[2].Value);
            Console.WriteLine($"fullItem: {fullItem}, full: {full}, X: {item.Groups[1].Value}, Y: {item.Groups[2].Value}");
        }

    }
}
