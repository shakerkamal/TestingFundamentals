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
                booking.ArrivalDate < b.DepartureDate &&
                b.ArrivalDate < booking.DepartureDate);

        return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
    }
}

public interface IUnitOfWork
{
    IQueryable<T> Query<T>();
}

public class UnitOfWork : IUnitOfWork
{
    public IQueryable<T> Query<T>()
    {
        return new List<T>().AsQueryable();
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

public class HouseKeeperStatementReport
{
    public int HouseKeeperId { get; set; }
    public DateTime StatementDate { get; set; }
    public bool HasData { get; set; }
    public HouseKeeperStatementReport(int houseKeeperId, DateTime statementDate)
    {
        HouseKeeperId = houseKeeperId;
        StatementDate = statementDate;
    }

    public void CreateDocument()
    {
        //creates a document on the provided paramenters
    }

    public void ExportToPdf(string fileName)
    {
        throw new NotImplementedException();
    }
}
