

public static class Computations
{
    public static double CalculateAverage<T>(IEnumerable<T> numbers)
    {
        IEnumerable<T> enumerable = numbers.ToList();
        if (numbers == null || !enumerable.Any())
        {
            throw new ArgumentException("The collection cannot be null or empty.");
        }

        double sum = 0;
        int count = 0;

        foreach (T number in enumerable)
        {
            if (number is IConvertible convertible)
            {
                sum += convertible.ToDouble(null);
                count++;
            }
            else
                throw new ArgumentException("All elements must be convertible to double.");
        }

        return sum / count;
    }
    
    public static double CalculateMedian<T>(IEnumerable<T> numbers)
    {
        IEnumerable<T> enumerable = numbers.ToList();
        if (numbers == null || !enumerable.Any())
        {
            throw new ArgumentException("The collection cannot be null or empty.");
        }

        List<double> sortedNumbers = enumerable
            .Select(n => Convert.ToDouble(n))
            .OrderBy(n => n)
            .ToList();

        int count = sortedNumbers.Count;
        if (count % 2 == 0)
        {
            return (sortedNumbers[count / 2 - 1] + sortedNumbers[count / 2]) / 2.0;
        }
        else
        {
            return sortedNumbers[count / 2];
        }
    }
    
    // a function that returns the mode of a list of numbers    
    public static double CalculateMode<T>(IEnumerable<T> numbers)
    {
        IEnumerable<T> enumerable = numbers.ToList();
        if (numbers == null || !enumerable.Any())
        {
            throw new ArgumentException("The collection cannot be null or empty.");
        }

        Dictionary<double, int> frequency = new();

        foreach (T number in enumerable)
        {
            if (number is IConvertible convertible)
            {
                double value = convertible.ToDouble(null);
                if (frequency.ContainsKey(value))
                {
                    frequency[value]++;
                }
                else
                {
                    frequency[value] = 1;
                }
            }
            else
                throw new ArgumentException("All elements must be convertible to double.");
        }

        return frequency.OrderByDescending(x => x.Value).First().Key;
    }
    
    // a function that returns the standard deviation of a list of numbers
    public static void BubbleSort<T>(IEnumerable<T> numbers)
    {
        IEnumerable<T> enumerable = numbers.ToList();
        if (numbers == null || !enumerable.Any())
        {
            throw new ArgumentException("The collection cannot be null or empty.");
        }

        List<T> list = enumerable.ToList();
        int n = list.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (Comparer<T>.Default.Compare(list[j], list[j + 1]) > 0)
                {
                    T temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }

        foreach (T number in list)
        {
            Console.WriteLine(number);
        }
    }
    
    
    
}