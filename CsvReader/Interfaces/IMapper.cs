namespace Simple.CsvReader.Interfaces;

public interface IMapper<TOutput>
{
    TOutput? Map(string[] fields, string[] allColumnNames, string[] usedColumnNames);
}
