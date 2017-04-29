﻿//-----------------------------------------------------------------------
// <copyright file="Bluetooth.Win32.cs" company="In The Hand Ltd">
//   Copyright (c) 2017 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License - see License.txt
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace InTheHand.Devices.Bluetooth
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct BLUETOOTH_DEVICE_INFO
    {
        internal int dwSize;
        internal ulong Address;
        internal uint ulClassofDevice;
        [MarshalAs(UnmanagedType.Bool)]
        internal bool fConnected;
        [MarshalAs(UnmanagedType.Bool)]
        internal bool fRemembered;
        [MarshalAs(UnmanagedType.Bool)]
        internal bool fAuthenticated;
        internal SYSTEMTIME stLastSeen;
        internal SYSTEMTIME stLastUsed;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 248)]
        internal string szName;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SYSTEMTIME
    {
        private ushort year;
        private short month;
        private short dayOfWeek;
        private short day;
        private short hour;
        private short minute;
        private short second;
        private short millisecond;
    }

    internal static class NativeMethods
    {
        private const string bthDll = "bthprops.cpl";

        private const int BLUETOOTH_MAX_NAME_SIZE = 248;
        
        // Pairing
        [DllImport(bthDll)]
        internal static extern int BluetoothAuthenticateDeviceEx(IntPtr hwndParentIn,
                IntPtr hRadioIn,
                ref BLUETOOTH_DEVICE_INFO pbtdiInout,
                IntPtr pbtOobData,
                AUTHENTICATION_REQUIREMENTS authenticationRequirement);

        [DllImport(bthDll)]
        internal static extern int BluetoothRemoveDevice(ref ulong pAddress);

        internal enum AUTHENTICATION_REQUIREMENTS
        {
            MITMProtectionNotRequired = 0x00,
            MITMProtectionRequired = 0x01,
            MITMProtectionNotRequiredBonding = 0x02,
            MITMProtectionRequiredBonding = 0x03,
            MITMProtectionNotRequiredGeneralBonding = 0x04,
            MITMProtectionRequiredGeneralBonding = 0x05,
            MITMProtectionNotDefined = 0xff,
        }

        internal enum BLUETOOTH_AUTHENTICATION_METHOD
        {
            LEGACY = 1,
            OOB,
            NUMERIC_COMPARISON,
            PASSKEY_NOTIFICATION,
            PASSKEY,
        }

        internal enum BLUETOOTH_IO_CAPABILITY
        {
            DISPLAYONLY = 0x00,
            DISPLAYYESNO = 0x01,
            KEYBOARDONLY = 0x02,
            NOINPUTNOOUTPUT = 0x03,
            UNDEFINED = 0xff
        }

        // Radio
        [DllImport(bthDll, SetLastError = true)]
        internal static extern IntPtr BluetoothFindFirstRadio(ref BLUETOOTH_FIND_RADIO_PARAMS pbtfrp, out IntPtr phRadio);

        [StructLayout(LayoutKind.Sequential)]
        internal struct BLUETOOTH_FIND_RADIO_PARAMS
        {
            public int dwSize;
        }

        [DllImport(bthDll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BluetoothFindNextRadio(IntPtr hFind, out IntPtr phRadio);

        [DllImport(bthDll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BluetoothFindRadioClose(IntPtr hFind);

        
        [DllImport(bthDll, SetLastError = true)]
        internal static extern int BluetoothGetRadioInfo(IntPtr hRadio, ref BLUETOOTH_RADIO_INFO pRadioInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct BLUETOOTH_RADIO_INFO
        {
            internal int dwSize;
            internal ulong address;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BLUETOOTH_MAX_NAME_SIZE)]
            internal string szName;
            internal uint ulClassofDevice;
            internal ushort lmpSubversion;
            [MarshalAs(UnmanagedType.U2)]
            internal ushort manufacturer;
        }

        [StructLayout(LayoutKind.Sequential, Size = 8)]
        internal class BLUETOOTH_COD_PAIRS
        {
            internal uint ulCODMask;
            [MarshalAs(UnmanagedType.LPWStr)]
            internal string pcszDescription;
        }

        [DllImport(bthDll, SetLastError = true)]
        internal static extern IntPtr BluetoothFindFirstDevice(
                ref BLUETOOTH_DEVICE_SEARCH_PARAMS pbtsp,
                ref BLUETOOTH_DEVICE_INFO pbtdi);

        [DllImport(bthDll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BluetoothFindNextDevice(
            IntPtr hFind,
            ref BLUETOOTH_DEVICE_INFO pbtdi);

        [DllImport(bthDll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool BluetoothFindDeviceClose(IntPtr hFind);

        [StructLayout(LayoutKind.Sequential)]
        internal struct BLUETOOTH_DEVICE_SEARCH_PARAMS
        {
            internal int dwSize;
            internal bool fReturnAuthenticated;
            internal bool fReturnRemembered;
            internal bool fReturnUnknown;
            internal bool fReturnConnected;
            internal bool fIssueInquiry;
            internal ushort cTimeoutMultiplier;
            internal IntPtr hRadio;
        }


        [DllImport(bthDll, SetLastError = true)]
        internal static extern int BluetoothGetDeviceInfo(IntPtr hRadio,  ref BLUETOOTH_DEVICE_INFO pbtdi);
    }
}