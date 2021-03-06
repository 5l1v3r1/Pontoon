// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeofenceState.cs" company="In The Hand Ltd">
//   Copyright (c) 2015-16 In The Hand Ltd, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
//#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
//using System.Runtime.CompilerServices;
//[assembly: TypeForwardedTo(typeof(Windows.Devices.Geolocation.Geofencing.GeofenceState))]
//#else

namespace InTheHand.Devices.Geolocation.Geofencing
{
    /// <summary>
    /// Indicates the current state of a Geofence. 
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <listheader><term>Platform</term><description>Version supported</description></listheader>
    /// <item><term>iOS</term><description>iOS 9.0 and later</description></item>
    /// <item><term>macOS</term><description>OS X 10.7 and later</description></item>
    /// <item><term>Windows UWP</term><description>Windows 10</description></item>
    /// <item><term>Windows Store</term><description>Windows 8.1 or later</description></item>
    /// <item><term>Windows Phone Store</term><description>Windows Phone 8.1 or later</description></item>
    /// <item><term>Windows Phone Silverlight</term><description>Windows Phone 8.0 or later</description></item></list></remarks>
    public enum GeofenceState
    {
        /// <summary>
        /// No flag is set.
        /// </summary>
        None = 0,

        /// <summary>
        /// The device has entered the geofence area.
        /// </summary>
        Entered = 1,

        /// <summary>
        /// The device has left the geofence area.
        /// </summary>
        Exited = 2,

        /// <summary>
        /// The geofence was removed.
        /// <para>Not supported on iOS.</para>
        /// </summary>
        Removed = 4,
    }
}
//#endif