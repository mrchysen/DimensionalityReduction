using DimensionalityReduction.Models;

namespace DimensionalityReduction.DataParser;

public interface IParser<T>
{
    public IEnumerable<T> Parse(StreamReader data);
}

public sealed class CsvParser(
    IMapper<CarBooking>? mapper = null) : IParser<CarBooking>
{
    private readonly IMapper<CarBooking> _mapper = mapper ?? new CarBookingMapper();

    public IEnumerable<CarBooking> Parse(StreamReader data)
    {
        SkipHeaderLine(data);

        while (!data.EndOfStream)
        {
            var line = data.ReadLine();

            if (!string.IsNullOrEmpty(line))
            {
                yield return _mapper.Map(line);
            }
        }
    }

    private void SkipHeaderLine(StreamReader data) => data.ReadLine();
}

