// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Environment.cs" company="In The Hand Ltd">
//   Copyright (c) 2016 In The Hand Ltd, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Windows.System.Profile;
using System;

namespace InTheHand
{
    /// <summary>
    /// Provides information about the Operating System.
    /// </summary>
    /// <seealso cref="System.Environment"/>
    public static class Environment
    {
        private static OperatingSystem _operatingSystem = new OperatingSystem();
        /// <summary>
        /// Gets an OperatingSystem object that contains the current platform identifier and version number.
        /// </summary>
        public static OperatingSystem OSVersion
        {
            get
            {
                return _operatingSystem;
            }
        }
    }

    /// <summary>
    /// Represents information about an operating system, such as the version and platform identifier.
    /// </summary>
    public sealed class OperatingSystem
    {
        private Version _version;
        /// <summary>
        /// Gets a System.Version object that identifies the operating system.
        /// </summary>
        public Version Version
        {
            get
            {

                if (_version == null)
                {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE || WIN32
                    _version = global::System.Environment.OSVersion.Version;
#else
                    string rawString = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
                    if (string.IsNullOrEmpty(rawString))
                    {
                        //default value
                        _version = new Version(8, 1, 0, 0);
                    }
                    else
                    {
                        ulong raw = ulong.Parse(rawString);
                        int major = (int)(raw & 0xFFFF000000000000L) >> 48;
                        int minor = (int)(raw & 0x0000FFFF00000000L) >> 32;
                        int build = (int)(raw & 0x00000000FFFF0000L) >> 16;
                        int revision = (int)(raw & 0x000000000000FFFFL);
                        _version = new Version(10 + major, minor, build, revision);
                    }
#endif
                }

                return _version;
            }
        }

        public string VersionString
        {
            get
            {
                return Version.ToString();
            }
        }
    }
}