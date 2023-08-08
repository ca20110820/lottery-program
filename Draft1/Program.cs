/* ===================================================================================================== */
/* Subroutines */

void ArrayPrinter(int[] inpArr)
{
    for (int i = 0; i < inpArr.Length; i++)
    {
        Console.Write(inpArr[i] + " ");
    }
}

int? BinarySearch(int Elem, int[] inpArr)
{
    /* Return the Index of the Element (in the sorted array) if exist, otherwise return null */

    int[] array = inpArr.OrderBy(x => x).ToArray(); // Sort without mutating the Original Array

    // Apply Binary Search Algorithm
    int l = 0, r = inpArr.Length - 1; // Initial Left and Right Indexes
    while (l <= r)
    {
        int mid = l + (r - l) / 2;
        if (array[mid] == Elem)
        {
            return mid;
        }

        // If Middle Element of current (l, r) index is less than Elem
        if (array[mid] < Elem)
        {
            // Update the Left Index
            l = mid + 1;
        }
        // If Middle Element of current (l, r) index is geater than or equal to Elem
        else
        {
            // Update the Right Index
            r = mid - 1;
        }
    }

    return null;
}

int? LinearSearch(int Elem, int[] inpArr)
{
    /* Return the Index of the Element if exists, otherwise return null */
    for (int i = 0; i < inpArr.Length; i++)
    {
        if (inpArr[i] == Elem)
        {
            return i;
        }
    }

    return null;
}

int[] RandomArrayGenerator(int lowestValue, int highestValue, int Size)
{
    int[] outArr = new int[Size];

    int i = 0;
    while (true)
    {
        Random rnd = new Random(); // Warning: Random Class in C# us Right-Exclusive!!!
        int randNumber = rnd.Next(lowestValue, highestValue + 1);

        // Check if the number does not exist in outArr
        // int? matchIndex = BinarySearch(randNumber, outArr);
        int? matchIndex = LinearSearch(randNumber, outArr);

        if (matchIndex == null)
        {
            outArr[i] = randNumber;
            i++; // Counter Update

            // Exit Loop when Generated *Size* Random Numbers
            if (i == Size)
            {
                break;
            }
        }
        // We aleady included the random number in the array, skip iteration
        else
        {
            continue;
        }
    }

    return outArr;
}

/* ===================================================================================================== */

/* Program Parameters */
int arrSize = 10; // Size of Array as a Parameter. No need to prompt.
int minVal = 1, maxVal = 99; // Min and Max Values for the Lottery Numbers

int[] userArr = new int[arrSize]; // User's Lottery Numbers

/* Prompt the User for their Lottery Numbers */
int promptCounter = 0;
Console.WriteLine($"Please enter your {arrSize} lottery numbers: ");
while (true)
{
    Console.Write($"Enter Number {promptCounter + 1}: ");

    int tempInput;
    bool resInput = int.TryParse( Console.ReadLine(), out tempInput);

    // Check and see if user inserted valid values.
    if (!resInput)
    {
        Console.WriteLine("Cannot Parse the given input into integer, please try again ...");
        continue;
    }
    if (tempInput < minVal || tempInput > maxVal)
    {
        Console.WriteLine($"Invalid Entry! The Number must be in between {minVal} and {maxVal}. Please try again ...");
        continue;
    }
    if (BinarySearch(tempInput, userArr) != null)
    {
        Console.WriteLine($"You already included {tempInput}. Please try a different number ...");
        continue;
    }

    userArr[promptCounter] = tempInput;

    promptCounter++;

    // Loop Exit Criterion
    if (promptCounter >= arrSize) { break; }
}

/* ===================================================================================================== */
void Main()
{
    int[] userNumbers = userArr;

    int[] randNumbers = RandomArrayGenerator(minVal, maxVal, arrSize); // Generate Random Lottery

    Console.Write("Lottery Numbers: ");
    ArrayPrinter(randNumbers);
    Console.Write("\n");

    Console.Write("Your Numbers: ");
    ArrayPrinter(userArr);
    Console.Write("\n");

    // Create List to Keep Track How many correct numbers the user guessed
    List<int> matchList = new List<int>();

    foreach(int elem in randNumbers)
    {
        int? matchIndex = BinarySearch(elem, userNumbers);
        if (matchIndex != null)
        {
            matchList.Add(elem);
        }
    }

    if (matchList.Count == 0)
    {
        Console.WriteLine("Sorry, you did not guessed anything correct. Better luck next time.");
    }
    else
    {
        Console.WriteLine($"You managed to guess {matchList.Count} number(s)! They are: ");
        foreach(int elem in matchList)
        {
            Console.WriteLine(elem);
        }
    }
}

Main();