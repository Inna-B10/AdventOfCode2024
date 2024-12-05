/*
//* PART1
In the text needs to find ALL occurrences of mul(X,Y), where X and Y are each 1-3 digit numbers.
Multiply X by Y.
Sum up results of all multiplications.

*/

using System.Text.RegularExpressions;

namespace _03_12;

class Program
{
    static void Main()
    {
        //* ------------------------------- Preparation ------------------------------ */
        //# get data from the link and save it to new file input-day3.txt
        //NB curl -H "Cookie: session=..." https://adventofcode.com/2024/day/3/input > input-day3.txt

        //# read the entire input-day3.txt in one line
        string filePath = "input-day3.txt";
        if (!File.Exists(filePath))
        {
            Console.WriteLine("The file not found!");
            return;
        }
        string input = File.ReadAllText(filePath);

        //* ------------------------------- SOLUTION 1 ------------------------------- */
        //# declare a new list for results of multiplications
        List<int> multiArr = [];

        //# create regular expression
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        //NB Regex is a class that provides methods for working with regular expressions:
        //NB IsMatch(), Match() - the first, Matches() - all, Replace(), Split()
        //NB Matches(): Returns a collection of all matches(MatchCollection), where each match is an object of type Match.
        //NB variable.Groups: Access groups in a regular expression(defined using parentheses ())
        //NB variable.Groups[0].Value и variable.Value = соответствует полному регулярному выражению
        //NB variable.Groups[1].Value, variable.Groups[2].Value = каждая квадратная скобка соответствующая группа() в регулярном выражении
        //NB в этом примере: item.Value = item.Groups[0].Value = mul(843,597) -- mul\((\d{1,3}),(\d{1,3})\)
        //NB                 item.Groups[1].Value = 843 -- (\d{1,3}) первое число X
        //NB                 item.Groups[].Value = 597 -- (\d{1,3}) второе число Y

        //# create a regular expression object with a specific pattern
        Regex regex = new(pattern);

        //# Get all matches
        MatchCollection matches = regex.Matches(input);

        //# Process each match
        foreach (Match item in matches)
        {
            //# convert to numbers, multiply and add to multiArr
            multiArr.Add(int.Parse(item.Groups[1].Value) * int.Parse(item.Groups[2].Value));
        }
        Console.WriteLine(multiArr.Sum());
    }
}
