using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHackathon.API.Contracts
{
    public record BookingRequest(DateTime CheckIn, DateTime CheckOut, int placeId);
}