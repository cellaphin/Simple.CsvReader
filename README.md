# Simple.CsvReader

Simple.CsvReader is a lightweight and easy-to-use C# library for reading and parsing CSV (Comma-Separated Values) files. This library is designed to provide a straightforward way to handle CSV data in your C# projects, offering flexibility and simplicity.

## Features

- **Easy Integration:** Because the intended use is for 1-off assignments, all you really need to do is specify the fields to retrieve from a file and the types, create a CsvReader object, and read data.

- **Configurable:** Customize the CSV parsing behavior with various options, Almost exclusively specifying delimiters.

- **Error Handling:** None included!

## Getting Started

### Installation

You can install Simple.CsvReader via NuGet Package Manager Console:

```bash
Install-Package Simple.CsvReader
```
Or use the .NET CLI:
```bash
dotnet add package Simple.CsvReader
```
### Usage
To use the Simple.CsvReader, we'll start with the assumption that your have a csv file where the first line contains column names, and the proceeding lines are rows with values. As an example:
```csv
FirstName, MiddleName, LastName
"John","Michael","Smith"
"Emma","Grace","Johnson"
"Alexander","James","Davis"
```
To use Simple.CsvReader, you'll first need to implement the RowMapper class and override MapRow. Be sure to use the "GetField" method, and pass in the value of the column heading that you want to parse:
```cs
public record Person
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

public class PersonMapper : RowMapper<Person>
{
    public override Person? MapRow(string[] fields)
    {
        return new Person()
        {
            FirstName = GetField(fields, "FirstName"),
            LastName = GetField(fields, "LastName"),
        };
    }
}
```

Create your CsvReader, passing in the full path of the file you want to read, the column header values, and the mapper you want to use, then call ReadAll to get your data!
```cs
CsvReader<Person> reader = new (
    fullPath: "A:\\Example\\filename.csv", 
    columnsToRead: new string[] { "FirstName", "LastName" }, 
    mapper: new PersonMapper()
);

IEnumerable<Person> fileData = reader.ReadAll();

foreach (var person in fileData) {
    Console.WriteLine($"{person.FirstName} {person.LastName}");
}

// Outputs:
// "John Smith"
// "Emma Johnson"
// "Alexander Davis"
``` 
For more detailed usage and configuration options, check out the source code. There's not much :)

### Contributions
Contributions are welcome! If you find any issues or have suggestions for improvements, feel free to create an issue or submit a pull request.

### License
This project is licensed under the GNU General Public License - see the (LICENSE)[LICENSE] file for details.
