using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingFundamentals.Mocking;

public interface IBookingRepository
{
    IQueryable<Booking> GetActiveBookings(int? excludeBookingId);
}

public class BookingRepository : IBookingRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Booking> GetActiveBookings(int? excludeBookingId = null)
    {
        var bookings = _unitOfWork.Query<Booking>()
            .Where(b => b.Status != "Cancelled");

        if(excludeBookingId.HasValue) 
        {
            bookings = bookings.Where(b => b.Id!.Equals(excludeBookingId.Value));
        }
        return bookings;
    }
}
