using DimensionalityReduction.DataParser;
using DimensionalityReduction.Models;

IParser<CarBooking> parser = new CsvParser();

using var stream = new StreamReader("./Data/ncr_ride_bookings.csv");

var bookings = parser.Parse(stream).ToList();
