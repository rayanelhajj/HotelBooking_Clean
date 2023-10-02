﻿using System;
using HotelBooking.Core.BindingModels;
namespace HotelBooking.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }

        public Booking() {
            
        }

        public Booking(BookingPostBindingModel model) {
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            CustomerId = model.CustomerId;
        }
    }
}
