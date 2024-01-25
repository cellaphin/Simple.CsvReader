using CsvReader.Contracts;
using Microsoft.VisualBasic.FileIO;

namespace CsvReader;

public class CsvReader<TOutput>
{

    public string FilePath { get; }

    private string[] _columnsToRead;
    private IMapper<TOutput> _mapper;
    private readonly string[] _delimiters;

    public CsvReader(string fullPath, string[] columnsToRead, IMapper<TOutput> mapper, params string[] delimiters)
    {
        FilePath = fullPath;
        _columnsToRead = columnsToRead;
        _mapper = mapper;
        _delimiters = delimiters.Any() ? delimiters : new string[] { "," };
    }

    public IEnumerable<TOutput> ReadAll()
    {
        List<TOutput?> data = new();

        using (TextFieldParser parser = new TextFieldParser(FilePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(_delimiters);

            // Read the header row
            string[] columnNames = parser.ReadFields();

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                data.Add(_mapper.Map(fields, columnNames, _columnsToRead));
            }
        }

        return data;
    }
}