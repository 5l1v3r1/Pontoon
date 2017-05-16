﻿//-----------------------------------------------------------------------
// <copyright file="BluetoothDevice.Win32.cs" company="In The Hand Ltd">
//   Copyright (c) 2017 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License - see License.txt
// </copyright>
//-----------------------------------------------------------------------

using InTheHand.Devices.Enumeration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace InTheHand.Devices.Bluetooth
{
    public sealed partial class BluetoothDevice
    {
        private static Task<BluetoothDevice> FromBluetoothAddressAsyncImpl(ulong address)
        {
            BLUETOOTH_DEVICE_INFO info = new BLUETOOTH_DEVICE_INFO();
            info.dwSize = global::System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.Address = address;
            int result = NativeMethods.BluetoothGetDeviceInfo(IntPtr.Zero, ref info);
            if (result == 0)
            {
                return Task.FromResult<BluetoothDevice>(new BluetoothDevice(info));
            }

            return Task.FromResult<BluetoothDevice>(null);
        }

        private static async Task<BluetoothDevice> FromIdAsyncImpl(string deviceId)
        {
            if (deviceId.StartsWith("BLUETOOTH#"))
            {
                var parts = deviceId.Split('#');

                string addrString = parts[1];
                ulong addr = 0;
                if (ulong.TryParse(addrString, NumberStyles.HexNumber, null, out addr))
                {
                    return await FromBluetoothAddressAsync(addr);
                }
            }

            return null;
        }

        private BLUETOOTH_DEVICE_INFO _info;

        internal BluetoothDevice(BLUETOOTH_DEVICE_INFO info)
        {
            _info = info;
        }

        private ulong GetBluetoothAddress()
        {
            return _info.Address;
        }
        
        private BluetoothClassOfDevice GetClassOfDevice()
        {
            return new BluetoothClassOfDevice(_info.ulClassofDevice);
        }

        private BluetoothConnectionStatus GetConnectionStatus()
        {
            NativeMethods.BluetoothGetDeviceInfo(IntPtr.Zero, ref _info);
            return _info.fConnected ? BluetoothConnectionStatus.Connected : BluetoothConnectionStatus.Disconnected;
        }

        private void ConnectionStatusChangedAdd()
        {
            var t = BluetoothAdapter.GetDefaultAsync();
            t.Wait();
            t.Result.ConnectionChanged += Result_ConnectionChanged;
        }

        private void ConnectionStatusChangedRemove()
        {
            var t = BluetoothAdapter.GetDefaultAsync();
            t.Wait();
            t.Result.ConnectionChanged -= Result_ConnectionChanged;
        }

        private void Result_ConnectionChanged(object sender, ulong e)
        {
            if (e == this.BluetoothAddress)
            {
                _connectionStatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private string GetDeviceId()
        {
            return "BLUETOOTH#" + BluetoothAddress.ToString("X12");
        }

        private string GetName()
        {
            return _info.szName;
        }

        internal static IReadOnlyCollection<Guid> GetRfcommServices(ref BLUETOOTH_DEVICE_INFO info)
        {
            List<Guid> services = new List<Guid>();

            int num = 0;
            int result = NativeMethods.BluetoothEnumerateInstalledServices(IntPtr.Zero, ref info, ref num, null);
            if (num > 0)
            {
                byte[] buffer = new byte[16 * num];

                result = NativeMethods.BluetoothEnumerateInstalledServices(IntPtr.Zero, ref info, ref num, buffer);
                if (result == 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        byte[] gb = new byte[16];
                        Buffer.BlockCopy(buffer, i * 16, gb, 0, 16);

                        services.Add(new Guid(gb));
                    }
                }
            }

            return services.AsReadOnly();
        }

        private void GetRfcommServices(List<Rfcomm.RfcommDeviceService> services)
        {
            foreach(Guid g in GetRfcommServices(ref _info))
            {
                services.Add(new Rfcomm.RfcommDeviceService(this, Rfcomm.RfcommServiceId.FromUuid(g)));
            }
        }
    }
}