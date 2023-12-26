using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public static class BookingHelper
{
    public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
    {
        if (booking.Status == "Cancelled")
            return string.Empty;

        //var unitOfWork = new UnitOfWork();

        var bookings = bookingRepository.GetActiveBookings(booking.Id);

        var overlappingBooking =
            bookings.FirstOrDefault(
                b =>
                booking.ArrivalDate >= b.ArrivalDate &&
                booking.ArrivalDate < b.DepartureDate ||
                booking.DepartureDate > b.ArrivalDate &&
                booking.DepartureDate <= b.DepartureDate);

        return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
    }
}

public interface IUnitOfWork
{
    IQueryable<Booking> Query<T>();
}

public class UnitOfWork : IUnitOfWork
{
    public IQueryable<Booking> Query<T>()
    {
        return new List<Booking>().AsQueryable();
    }
}

public class Booking
{
    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public string Reference { get; set; }
}
