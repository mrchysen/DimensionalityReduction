using DimensionalityReduction.Models;
using System.Globalization;
using System.Runtime.Serialization;

namespace DimensionalityReduction.DataParser;

public interface IMapper<T>
{
    public T Map(string line);
}

public class CarBookingMapper : IMapper<CarBooking>
{
    public CarBooking Map(string line)
    {
        if (string.IsNullOrEmpty(line))
            throw new ArgumentException("CSV line cannot be null or empty");

        var values = line.Split(',');

        if (values.Length != 21)
            throw new ArgumentException($"Invalid CSV format. Expected 21 columns, got {values.Length}");

        try
        {
            return new CarBooking
            {
                Date = DateOnly.Parse(values[0]),
                Time = TimeOnly.Parse(values[1]),
                BookingId = values[2],
                Status = ParseBookingStatus(values[3]),
                CustomerId = values[4].EnsureNull(),
                VehicleType = ParseVehicleType(values[5].EnsureNull()),
                PickupLocation = values[6].EnsureNull(),
                DropLocation = values[7].EnsureNull(),
                AvgVTAT = Convert.ToDouble(values[8].EnsureNull(), CultureInfo.InvariantCulture),
                AvgCTAT = Convert.ToDouble(values[9].EnsureNull(), CultureInfo.InvariantCulture),
                CancelledByCustomer = ToBoolean(values[10].EnsureNull()),
                CustomerCancellationReason = values[11].EnsureNull(),
                CancelledByDriver = ToBoolean(values[12].EnsureNull()),
                DriverCancellationReason = values[13].EnsureNull(),
                IncompleteRide = ToBoolean(values[14].EnsureNull()), 
                IncompleteRideReason = values[15].EnsureNull(),
                BookingValue = Convert.ToDecimal(values[16].EnsureNull(), CultureInfo.InvariantCulture),
                RideDistance = Convert.ToDouble(values[17].EnsureNull(), CultureInfo.InvariantCulture),
                DriverRating = Convert.ToDouble(values[18].EnsureNull(), CultureInfo.InvariantCulture),
                CustomerRating = Convert.ToDouble(values[19].EnsureNull(), CultureInfo.InvariantCulture),
                PaymentMethod = ParsePaymentMethod(values[20].EnsureNull())
            };
        }
        catch (Exception ex)
        {
            throw new FormatException($"Error parsing CSV line: {line}", ex);
        }
    }

    private bool ToBoolean(string? value)
    {
        if (value is null || value == "0") return false;
        return true;
    }

    private BookingStatus ParseBookingStatus(string status)
    {
        if (string.IsNullOrEmpty(status)) return BookingStatus.Completed;

        return status.Replace(" ", "") switch
        {
            "Completed" => BookingStatus.Completed,
            "CancelledByCustomer" => BookingStatus.CancelledByCustomer,
            "CancelledbyCustomer" => BookingStatus.CancelledByCustomer,
            "CancelledByDriver" => BookingStatus.CancelledByDriver,
            "CancelledbyDriver" => BookingStatus.CancelledByDriver,
            "InProgress" => BookingStatus.InProgress,
            "Scheduled" => BookingStatus.Scheduled,
            "NoShow" => BookingStatus.NoShow,
            "Incomplete" => BookingStatus.Incomplete,
            "NoDriverFound" => BookingStatus.NoDriverFound,
            _ => throw new FormatException($"Unknown booking status: {status}")
        };
    }

    private VehicleType ParseVehicleType(string? type)
    {
        if (string.IsNullOrEmpty(type)) return VehicleType.GoSedan;

        return type.Replace(" ", "").Replace("/", "") switch
        {
            "GoMini" => VehicleType.GoMini,
            "GoSedan" => VehicleType.GoSedan,
            "Auto" => VehicleType.Auto,
            "eBike" => VehicleType.EBike,
            "Bike" => VehicleType.EBike,
            "UberXL" => VehicleType.UberXL,
            "PremierSedan" => VehicleType.PremierSedan,
            _ => throw new FormatException($"Unknown vehicle type: {type}")
        };
    }

    private PaymentMethod ParsePaymentMethod(string? method)
    {
        if (string.IsNullOrEmpty(method)) return PaymentMethod.UPI;

        return method.Replace(" ", "") switch
        {
            "UPI" => PaymentMethod.UPI,
            "Cash" => PaymentMethod.Cash,
            "CreditCard" => PaymentMethod.CreditCard,
            "Credit Card" => PaymentMethod.CreditCard,
            "UberWallet" => PaymentMethod.UberWallet,
            "Uber Wallet" => PaymentMethod.UberWallet,
            "DebitCard" => PaymentMethod.DebitCard,
            "Debit Card" => PaymentMethod.DebitCard,
            _ => throw new FormatException($"Unknown payment method: {method}")
        };
    }
}

file static class StringExtension
{
    public static string? EnsureNull(this string? value)
    {
        if (value == "null") return null;
        return value;
    }
}
