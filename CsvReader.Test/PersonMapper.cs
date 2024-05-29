using Simple.CsvReader;

namespace CsvReader.Test;

internal class PersonMapper : RowMapper<Person>
{
    public override Person? MapRow(string[] fields)
    {
        return new Person()
        {
            FirstName = GetField(fields, "FirstName"),
            MiddleName = GetField(fields, "MiddleName"),
            LastName = GetField(fields, "LastName"),
        };
    }
}