namespace _01_12;

internal class Program
{
    public static void Main()
    {
        string[]? lines = File.ReadAllLines("input-day1.txt");
        int count = lines.Length;
        // int count = 10;

        //to check if we get some data from the file
        // Console.WriteLine(lines[0]);

        int[]? column1 = new int[count];
        int[]? column2 = new int[count];

        for (int i = 0; i < count; i++)
        {
            //split by spaces and/or tabs and remove them
            var parts = lines[i].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && int.TryParse(parts[0], out int num1) && int.TryParse(parts[1], out int num2))
            {
                column1[i] = num1;
                column2[i] = num2;
            }
        }
        Array.Sort(column1);
        Array.Sort(column2);

        // foreach (var item in column1)
        // {
        //     Console.WriteLine(item);
        // }

        var differencesArr = column1.Zip(column2, (a, b) => Math.Abs(a - b)).ToArray();

        int result = differencesArr.Sum();
        Console.Write(result); //2285373
    }
}
