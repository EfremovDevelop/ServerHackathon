using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels
{
    public class BookingAvaliableSlotsDto
    {
        public Place place { get; set; }
        public ICollection<BookingSlotDto> bookingSlot{ get; set; }
    }
}