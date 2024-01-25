namespace CsvReader.Contracts;

public interface IMapper<TOutput>
{
    TOutput? Map(string[] fields, string[] allColumnNames, string[] usedColumnNames);
}
