using System;
using HotelBooking.Core;
using HotelBooking.Core.Entities;
using HotelBooking.Core.Interfaces;
using HotelBooking.Core.Services;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            // Arrange
            DateTime date = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(date, date);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_StartDateInThePast()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(-5);
            DateTime finishDate = startDate.AddDays(3);
            // Act
            Action act = () => bookingManager.FindAvailableRoom(startDate, finishDate);
            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_OverlapsBookingButNotAvailableDays()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(6);
            DateTime finishDate = startDate.AddDays(10);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, finishDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_OverlapsBookingPartiallyInclAvailableDays()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime finishDate = startDate.AddDays(10);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, finishDate);
            // Assert
            Assert.Equal(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_OverlapsExistingBookingFullyInclAvailableDays()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime finishDate = startDate.AddDays(35);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, finishDate);
            // Assert
            Assert.Equal(-1, roomId);
        }


        [Fact]
        public void FindAvailableRoom_AfterBooking()
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(30);
            DateTime finishDate = startDate.AddDays(5);
            // Act
            int roomId = bookingManager.FindAvailableRoom(startDate, finishDate);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

    }
}
