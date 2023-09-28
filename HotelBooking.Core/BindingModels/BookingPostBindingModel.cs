using System;
using System.ComponentModel.DataAnnotations;
namespace HotelBooking.Core.BindingModels {
    public class BookingPostBindingModel {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
