using System;
using Xamarin.Forms.Maps;

namespace MapEX.Models
{
    public interface ILocationModel
    {
        Pin ClosestPin { get; }
        double DistanceToClosestPin { get; }
        void SetDistanceToClosestPin(double distance);
        void SetClosestPin(Pin pin);
    }
}
