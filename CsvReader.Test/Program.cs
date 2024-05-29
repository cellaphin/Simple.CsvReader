
using CsvReader.Test;
using Simple.CsvReader;

string currentDirectory = Directory.GetCurrentDirectory();
string rootDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\.."));
string directoryPath = Path.Combine(rootDirectory, "TestFiles");

Console.WriteLine(directoryPath);
if (Directory.Exists(directoryPath))
{
    string[] files = Directory.GetFiles(directoryPath);

    foreach (string file in files)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine(file);
            Console.WriteLine($"Processing file: {Path.GetFileName(file)}");
            IEnumerable<Person> people = ProcessFile(file);
            VisualCheck(people);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing file: {Path.GetFileName(file)} - {ex.Message}");
            Console.WriteLine(ex);
        }
    }
}
else
{
    Console.WriteLine("TestFiles directory not found.");
}

static IEnumerable<Person> ProcessFile(string filePath)
{
    CsvReader<Person> reader = new(
        fullPath: filePath,
        columnsToRead: new string[] { "FirstName", "middleName", "LastName" },
        mapper: new PersonMapper(),
        new string[] { ",", ";" }
    );

    return reader.ReadAll().AsEnumerable();

}

static void VisualCheck(IEnumerable<Person> people)
{

    foreach (var person in people)
    {
        Console.WriteLine($"FirstName:'{person.FirstName}', MiddleName:'{person.MiddleName}', LastName:'{person.LastName}'");
    }
}