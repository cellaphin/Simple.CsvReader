using Simple.CsvReader.Interfaces;

namespace Simple.CsvReader;

public abstract class RowMapper<TOutput> : IMapper<TOutput>
{
    protected Dictionary<string, int> HeaderIndexes;
    public TOutput? Map(string[] fields, string[] allColumnNames, string[] usedColumnNames)
    {
        if (fields == null || fields.Length == 0)
            throw new ArgumentNullException(nameof(fields));

        if (HeaderIndexes == null)
            CreateHeaderIndexMapping(allColumnNames, usedColumnNames);

        return MapRow(fields);
    }
    public abstract TOutput? MapRow(string[] fields);
    protected void CreateHeaderIndexMapping(string[] allColumnNames, string[] usedColumnNames)
    {
        HeaderIndexes = new Dictionary<string, int>();
        foreach (var name in usedColumnNames)
        {
            AddIndex(name, allColumnNames, HeaderIndexes);
        }
    }
    protected string GetField(string[] fields, string columnName) => fields[HeaderIndexes[columnName]].Trim();
    private void AddIndex(string header, string[] headers, Dictionary<string, int> headerIndexes)
    {
        var index = Array.IndexOf(headers, header);
        if (index != -1)
            headerIndexes.Add(header, index);
        else
            throw new Exception($"Unable to find column for \"{header}\"");
    }
}
