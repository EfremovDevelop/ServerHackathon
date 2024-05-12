using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerHackathon.API.Contracts
{
    public record PlaceRequest(int placeId, DateTime data);
}