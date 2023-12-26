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
        private Booking _exisitingBooking;
        private Mock<IBookingRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _exisitingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = CreateDate(2020, 12, 15), //Before(_exitingBooking.ArrivalDate)
                DepartureDate = CreateDate(2020, 12, 20),
                Reference = "a"
            };
            _mockRepository = new Mock<IBookingRepository>();

            _mockRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _exisitingBooking
            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExisitingBooking_ReturnEmptyString()
        {
            //var mockRepository = new Mock<IBookingRepository>();

            //mockRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            //{
            //    new Booking
            //    {
            //        Id = 2,
            //        ArrivalDate = CreateDate(2020, 12, 15), //Before(_exitingBooking.ArrivalDate)
            //        DepartureDate = CreateDate(2020, 12, 20),
            //        Reference = "a"
            //    }
            //}.AsQueryable());

            var res = BookingHelper.OverlappingBookingsExist( new Booking 
            {
                Id = 1,
                ArrivalDate = Before(_exisitingBooking.ArrivalDate, days: 2),//CreateDate(2020, 12, 10),
                DepartureDate = Before(_exisitingBooking.ArrivalDate),
            },_mockRepository.Object);

            Assert.That(res, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfAnExisitingBooking_ReturnExistingBookingReference()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_exisitingBooking.ArrivalDate),
                DepartureDate = After(_exisitingBooking.ArrivalDate),
            }, _mockRepository.Object);

            Assert.That(res, Is.EqualTo(_exisitingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterAnExisitingBookingDepartureDate_ReturnExistingBookingReference()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_exisitingBooking.ArrivalDate),
                DepartureDate = After(_exisitingBooking.DepartureDate),
            }, _mockRepository.Object);

            Assert.That(res, Is.EqualTo(_exisitingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfAnExisitingBooking_ReturnExistingBookingReference()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_exisitingBooking.ArrivalDate),
                DepartureDate = Before(_exisitingBooking.DepartureDate),
            }, _mockRepository.Object);

            Assert.That(res, Is.EqualTo(_exisitingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsInTheMiddleAndFinishesAfterAnExisitingBooking_ReturnExistingBookingReference()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_exisitingBooking.ArrivalDate),
                DepartureDate = After(_exisitingBooking.DepartureDate),
            }, _mockRepository.Object);

            Assert.That(res, Is.EqualTo(_exisitingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterAnExisitingBooking_ReturnEmptyString()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_exisitingBooking.DepartureDate),
                DepartureDate = After(_exisitingBooking.DepartureDate, days: 3),
            }, _mockRepository.Object);

            Assert.That(res, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var res = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_exisitingBooking.ArrivalDate),
                DepartureDate = After(_exisitingBooking.DepartureDate),
                Status = "Cancelled"
            }, _mockRepository.Object);

            Assert.That(res, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime CreateDate(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
    }
}
