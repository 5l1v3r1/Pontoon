﻿//-----------------------------------------------------------------------
// <copyright file="BluetoothAdapter.cs" company="In The Hand Ltd">
//   Copyright (c) 2017 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License - see License.txt
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace InTheHand.Devices.Bluetooth
{
    /// <summary>
    /// Represents a local Bluetooth adapter.
    /// </summary>
    /// <remarks>
    /// <para/><list type="table">
    /// <listheader><term>Platform</term><description>Version supported</description></listheader>
    /// <item><term>Windows (Desktop Apps)</term><description>Windows 7 or later</description></item></list>
    /// </remarks>
    public sealed partial class BluetoothAdapter
    {
        /// <summary>
        /// Gets the default BluetoothAdapter.
        /// </summary>
        /// <returns>An asynchronous operation that completes with a BluetoothAdapter.</returns>
        public static Task<BluetoothAdapter> GetDefaultAsync()
        {
#if WIN32
            return GetDefaultAsyncImpl();

#else
            return Task.FromResult<BluetoothAdapter>(null);
#endif
        }

        /// <summary>
        /// Gets the device address.
        /// </summary>
        public ulong BluetoothAddress
        {
            get
            {
#if WIN32
                return GetBluetoothAddress();
#else
                return 0;
#endif

            }
        }

        /*
        /// <summary>
        /// Gets a boolean indicating if the adapter supports the Bluetooth Classic transport type.
        /// </summary>
        public bool IsClassicSupported
        {
            get
            {
                return true;
            }
        }*/

        /// <summary>
        /// Gets the Name of the adapter.
        /// </summary>
        /// <value>The name of the adapter.</value>
        public string Name
        {
            get
            {
#if WIN32
                return GetName();
#else
                return string.Empty;
#endif
            }
        }

    }
}