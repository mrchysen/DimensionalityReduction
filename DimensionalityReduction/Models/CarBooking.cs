namespace DimensionalityReduction.Models;

public class CarBooking
{
    // Дата заказа
    public DateOnly Date { get; set; }
    // Время заказа
    public TimeOnly Time { get; set; }
    // ID
    public string? BookingId { get; set; }
    // Статус заказа
    public BookingStatus? Status { get; set; }
    // ID заказчика
    public string? CustomerId { get; set; }
    // Тип машины
    public VehicleType? VehicleType { get; set; }
    // Начальная позиция
    public string? PickupLocation { get; set; }
    // Конечная позиция
    public string? DropLocation { get; set; }
    // Среднее время приезда водителя к заказчику (in minutes)
    public double? AvgVTAT { get; set; }
    // Среднее время поездки до места высадки (in minutes)
    public double? AvgCTAT { get; set; }
    // Отмененно ли заказчиком
    public bool? CancelledByCustomer { get; set; }
    // Причина отмены
    public string? CustomerCancellationReason { get; set; }
    // Отмена от водителя
    public bool? CancelledByDriver { get; set; }
    // Причина отмены
    public string? DriverCancellationReason { get; set; }
    // Флаг незакоченной поездки
    public bool? IncompleteRide { get; set; }
    // Причина незакоченной поездки
    public string? IncompleteRideReason { get; set; }
    // Стоимость поездки
    public decimal? BookingValue { get; set; }
    // Протяженность поездки (in km)
    public double? RideDistance { get; set; } 
    // Оценка водителя
    public double? DriverRating { get; set; }
    // Оценка заказчика
    public double? CustomerRating { get; set; }
    // Способ оплаты
    public PaymentMethod? PaymentMethod { get; set; }
}
