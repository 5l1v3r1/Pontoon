// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Geoposition.cs" company="In The Hand Ltd">
//   Copyright (c) 2015-16 In The Hand Ltd, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
//#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
//using System.Runtime.CompilerServices;
//[assembly: TypeForwardedTo(typeof(Windows.Devices.Geolocation.Geoposition))]
//#else

#if __IOS__
using CoreLocation;
#elif WIN32
using System.Device.Location;
#endif

namespace InTheHand.Devices.Geolocation
{
    /// <summary>
    /// Represents a location that may contain latitude and longitude data or venue data.
    /// </summary>
    public sealed class Geoposition
    {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE
        private Windows.Devices.Geolocation.Geoposition _position;

        private Geoposition(Windows.Devices.Geolocation.Geoposition position)
        {
            _position = position;
        }

        public static implicit operator Windows.Devices.Geolocation.Geoposition(Geoposition gp)
        {
            return gp._position;
        }

        public static implicit operator Geoposition(Windows.Devices.Geolocation.Geoposition gp)
        {
            return new Geoposition(gp);
        }
#elif __IOS__
        // constructor from CoreLocation location
        internal Geoposition(CLLocation location)
        {
            Coordinate = new Geocoordinate();
            if (location != null)
            {
                Coordinate.Point = new Geopoint(new BasicGeoposition() { Latitude = location.Coordinate.Latitude, Longitude = location.Coordinate.Longitude, Altitude = location.Altitude });

                Coordinate.Accuracy = location.HorizontalAccuracy;

                if (!double.IsNaN(location.VerticalAccuracy))
                {
                    Coordinate.AltitudeAccuracy = location.VerticalAccuracy;
                }

                if (!double.IsNaN(location.Course) && location.Course != -1)
                {
                    Coordinate.Heading = location.Course;
                }

                if (!double.IsNaN(location.Speed) && location.Speed != -1)
                {
                    Coordinate.Speed = location.Speed;
                }

                Coordinate.Timestamp = InTheHand.DateTimeOffsetHelper.FromNSDate(location.Timestamp);
            }
        }
#elif WIN32
        internal Geoposition(GeoPosition<GeoCoordinate> position)
        {
            Coordinate = new Geocoordinate();
            Coordinate.Point = new Geopoint(new BasicGeoposition() { Latitude = position.Location.Latitude, Longitude = position.Location.Longitude, Altitude = position.Location.Altitude });
            Coordinate.Accuracy = position.Location.HorizontalAccuracy;
            Coordinate.Timestamp = position.Timestamp;
            Coordinate.AltitudeAccuracy = position.Location.VerticalAccuracy;
            Coordinate.Heading = position.Location.Course;
            Coordinate.Speed = position.Location.Speed;
            Coordinate.PositionSource = PositionSource.Unknown;
        }
#endif

        /// <summary>
        /// The latitude and longitude associated with a geographic location.
        /// </summary>
        public Geocoordinate Coordinate
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            get
            {
                return _position.Coordinate;
            }
#else
            get;
            internal set;
#endif
        }
    }
}
//#endif