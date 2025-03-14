// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

IEnumerable<int> numbers = Enumerable.Range(0, 7).ToList();

double average = Computations.CalculateAverage(numbers);
Console.WriteLine($"Average: {average}");

double median = Computations.CalculateMedian(numbers);
Console.WriteLine($"Median: {median}");

Console.ReadLine();
