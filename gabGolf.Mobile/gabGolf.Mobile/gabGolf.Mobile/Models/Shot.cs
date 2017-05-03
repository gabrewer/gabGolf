using GeoCoordinatePortable;
using Plugin.Geolocator.Abstractions;
using System;

namespace gabGolf.Mobile.Models
{
    public class Shot
    {
        public string Club;
        public Position StartPosition;
        public Position EndPosition;
        public double Distance
        {
            get
            {
                return new GeoCoordinate(StartPosition.Latitude, StartPosition.Longitude).GetDistanceTo(new GeoCoordinate(EndPosition.Latitude, EndPosition.Longitude));
            }
        }
    }
}
