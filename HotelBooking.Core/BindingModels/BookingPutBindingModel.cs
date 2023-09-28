using System.ComponentModel.DataAnnotations;
namespace HotelBooking.Core.BindingModels {
    public class BookingPutBindingModel {
        [Required]
        public int Id { get; set; }
        public bool? IsActive { get; set; }
        public int? CustomerId { get; set; }
    }
}
