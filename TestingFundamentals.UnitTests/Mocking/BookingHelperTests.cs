using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingFundamentals.Mocking;

namespace TestingFundamentals.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExisitingBooking_ReturnEmptyString()
        {
            var mockRepository = new Mock<IBookingRepository>();

            mockRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                new Booking
                {
                    Id = 2,
                    ArrivalDate = new DateTime(2020, 12, 15, 14, 0, 0),
                    DepartureDate = new DateTime(2020, 12, 20, 10, 0, 0),
                    Reference = "a"
                }
            }.AsQueryable());

            var res = BookingHelper.OverlappingBookingsExist( new Booking { },mockRepository.Object);


        }
    }
}
