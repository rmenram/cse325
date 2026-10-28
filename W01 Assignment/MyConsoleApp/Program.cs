Console.WriteLine("Hello, World!");
// Console.WriteLine("The current time is " + DateTime.Now);
// String interpolation:
Console.WriteLine($"The current time is {DateTime.Now}");
// How many days until Christmas?
DateTime christmas = new(DateTime.Today.Year, 12, 25);
Console.WriteLine($"The are {(christmas - DateTime.Now).Days} days until the next Christmas");
