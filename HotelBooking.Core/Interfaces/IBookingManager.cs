﻿using System;
using System.Collections.Generic;
using HotelBooking.Core.BindingModels;
using HotelBooking.Core.Entities;
namespace HotelBooking.Core.Interfaces
{
    public interface IBookingManager
    {
        bool CreateBooking(BookingPostBindingModel model);
        int FindAvailableRoom(DateTime startDate, DateTime endDate);
        List<DateTime> GetFullyOccupiedDates(DateTime startDate, DateTime endDate);
    }
}
