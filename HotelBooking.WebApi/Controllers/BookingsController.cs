using System.ComponentModel.DataAnnotations;
using System.Net;
using HotelBooking.Core;
using HotelBooking.Core.Entities;
using HotelBooking.Core.Exceptions;
using HotelBooking.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HotelBooking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : Controller
    {
        private readonly IRepository<Booking> bookingRepository;
        private IRepository<Customer> customerRepository;
        private IRepository<Room> roomRepository;
        private readonly IBookingManager bookingManager;

        public BookingsController(IRepository<Booking> bookingRepos, IRepository<Room> roomRepos,
            IRepository<Customer> customerRepos, IBookingManager manager)
        {
            bookingRepository = bookingRepos;
            roomRepository = roomRepos;
            customerRepository = customerRepos;
            bookingManager = manager;
        }

        // GET: bookings
        [HttpGet(Name = "GetBookings")]
        public IEnumerable<Booking> Get()
        {
            return bookingRepository.GetAll();
        }

        // GET bookings/5
        [HttpGet("{id}", Name = "GetBooking")]
        public IActionResult Get(int id)
        {
            var item = bookingRepository.Get(id);
            if (item is null) {
                throw new RestException(HttpStatusCode.NotFound, $"Booking with ID: {id} not found");
            }
            return new ObjectResult(item);
        }

        // POST bookings
        [HttpPost]
        public IActionResult Post([FromBody] [Required] Booking booking)
        {
            bool created = bookingManager.CreateBooking(booking);

            if (created)
            {
                return CreatedAtRoute("GetBookings", null);
            }
            throw new RestException(HttpStatusCode.Conflict, "The booking could not be created. All rooms are occupied. Please try another period.");
        }

        // PUT bookings/5
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] [Required] Booking booking)
        {
            if (booking.Id != id)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Request Body ID does not match the request URL ID");
            }

            var modifiedBooking = bookingRepository.Get(id);

            if (modifiedBooking == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Booking not found");
            }

            // This implementation will only modify the booking's state and customer.
            // It is not safe to directly modify StartDate, EndDate and Room, because
            // it could conflict with other active bookings.
            modifiedBooking.IsActive = booking.IsActive;
            modifiedBooking.CustomerId = booking.CustomerId;

            bookingRepository.Edit(modifiedBooking);
            return NoContent();
        }

        // DELETE bookings/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (bookingRepository.Get(id) is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Booking not found");
            }

            bookingRepository.Remove(id);
            return NoContent();
        }

    }
}
