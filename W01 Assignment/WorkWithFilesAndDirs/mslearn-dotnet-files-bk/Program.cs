using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Text;

// var salesFiles = FindFiles("stores");
var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

// File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), String.Empty);
File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

var salesSummary = BuildSalesSummary(salesFiles);
File.WriteAllText(Path.Combine(salesTotalDir, "salessummary.txt"), $"{salesSummary}{Environment.NewLine}");

// foreach (var file in salesFiles)
// {
//     Console.WriteLine(file);
// }

IEnumerable<string> FindFiles(string folderName) // static IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>(); // []

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // // The file name will contain the full path, so only check the end of it
        // if (file.EndsWith("sales.json"))
        // {
        //     salesFiles.Add(file);
        // }
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

string BuildSalesSummary(IEnumerable<string> salesFiles)
{
    StringBuilder salesSummary = new StringBuilder();
    StringBuilder salesDetail = new StringBuilder();
    string header = "Sales Summary";
    string divider = "----------------------------";

    salesSummary.AppendLine(header);
    salesSummary.AppendLine(divider);

    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        FileInfo info = new FileInfo(file);
        if (!info.Name.Equals("sales.json", StringComparison.OrdinalIgnoreCase))
        {
            continue;
        }
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);
        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
        Console.WriteLine(info.Directory.Name);
        var relativePath = Path.Combine(info.Directory.Name, info.Name);
        Console.WriteLine(relativePath);
        salesDetail.AppendLine($"  {relativePath}: {(data?.Total ?? 0):C}");
    }

    salesSummary.AppendLine($" Total Sales: {salesTotal:C}");
    salesSummary.AppendLine("");
    salesSummary.AppendLine($" Details:");
    salesSummary.Append(salesDetail.ToString());

    return salesSummary.ToString();
}

record SalesData(double Total);

// *****************************
// Other practice statements
// *****************************
// Console.WriteLine(Directory.GetCurrentDirectory());
// string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
// Console.WriteLine(docPath);
// Console.WriteLine($"stores{Path.DirectorySeparatorChar}201");
// Console.WriteLine(Path.Combine("stores", "201")); // outputs: stores/201
// Console.WriteLine(Path.GetExtension("sales.json")); // outputs: .json

// string fileName = $"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales{Path.DirectorySeparatorChar}sales.json";

// FileInfo info = new FileInfo(fileName);

// Console.WriteLine($"Full Name: {info.FullName}{Environment.NewLine}Directory: {info.Directory}{Environment.NewLine}Extension: {info.Extension}{Environment.NewLine}Create Date: {info.CreationTime}"); // And many more

// Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "stores", "201", "newDir"));
// string filePath = Directory.GetCurrentDirectory();
// bool doesDirectoryExist = Directory.Exists(filePath);
// File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "greeting.txt"), "Hello World!");

// File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
// string sales = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
// Console.WriteLine(sales);

// var salesJson = File.ReadAllText($"stores{Path.DirectorySeparatorChar}201{Path.DirectorySeparatorChar}sales.json");
// var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// Console.WriteLine(salesData.Total); // Console.WriteLine(salesData?.Total)


// var data = JsonConvert.DeserializeObject<SalesTotal>(salesJson);

// File.WriteAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", data.Total.ToString());

// File.AppendAllText($"salesTotalDir{Path.DirectorySeparatorChar}totals.txt", $"{data.Total}{Environment.NewLine}");

// class SalesTotal
// {
//     public double Total { get; set; }
// }
