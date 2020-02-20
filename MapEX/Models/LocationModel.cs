using System;
using Xamarin.Forms.Maps;
//using Xamarin.Forms.Maps;

namespace MapEX.Models
{
    public class LocationModel : ILocationModel
    {
        public Pin ClosestPin { get; private set; }
        public double DistanceToClosestPin { get; private set; }

        public void SetDistanceToClosestPin(double distance)
        {
            DistanceToClosestPin = distance;
        }

        public void SetClosestPin(Pin pin)
        {
            ClosestPin = pin;
        }
    }
}
