﻿//-----------------------------------------------------------------------
// <copyright file="RfcommServiceId.cs" company="In The Hand Ltd">
//   Copyright (c) 2017 In The Hand Ltd, All rights reserved.
//   This source code is licensed under the MIT License - see License.txt
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace InTheHand.Devices.Bluetooth.Rfcomm
{
    /// <summary>
    /// Represents an RFCOMM service ID.
    /// </summary>
    public sealed class RfcommServiceId
    {
        private static readonly Guid BluetoothBase = new Guid(0x00000000, 0x0000, 0x1000, 0x80, 0x00, 0x00, 0x80, 0x5F, 0x9B, 0x34, 0xFB);
       
        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized Serial Port service (with short id 0x1101).
        /// </summary>
        public static RfcommServiceId SerialPort
        {
            get
            {
                return FromShortId(0x1101);
            }
        }

        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized OBEX Object Push service (with short id 0x1105).
        /// </summary>
        public static RfcommServiceId ObexObjectPush
        {
            get
            {
                return FromShortId(0x1105);
            }
        }

        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized OBEX File Transfer service (with short id 0x1106).
        /// </summary>
        public static RfcommServiceId ObexFileTransfer
        {
            get
            {
                return FromShortId(0x1106);
            }
        }

        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized Basic Printing service (with short id 0x1122).
        /// </summary>
        public static RfcommServiceId BasicPrinting
        {
            get
            {
                return FromShortId(0x1122);
            }
        }

        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized Phone Book Access (PCE) service (with short id 0x112E).
        /// </summary>
        public static RfcommServiceId PhoneBookAccessPce
        {
            get
            {
                return FromShortId(0x112E);
            }
        }

        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized Phone Book Access (PSE) service (with short id 0x112F).
        /// </summary>
        public static RfcommServiceId PhoneBookAccessPse
        {
            get
            {
                return FromShortId(0x112F);
            }
        }

        /// <summary>
        /// Creates a RfcommServiceId object corresponding to the service id for the standardized Generic File Transfer service (with short id 0x1202).
        /// </summary>
        public static RfcommServiceId GenericFileTransfer
        {
            get
            {
                return FromShortId(0x1202);
            }
        }

        
        

        /// <summary>
        /// Creates a RfcommServiceId object from a 32-bit service id.
        /// </summary>
        /// <param name="shortId">The 32-bit service id.</param>
        /// <returns>The RfcommServiceId object.</returns>
        public static RfcommServiceId FromShortId(UInt32 shortId)
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
            return Windows.Devices.Bluetooth.Rfcomm.RfcommServiceId.FromShortId(shortId);

#else
            byte[] guidBytes = BluetoothBase.ToByteArray();
            BitConverter.GetBytes(shortId).CopyTo(guidBytes, 0);
            return new RfcommServiceId(new Guid(guidBytes));  
#endif 
        }

        /// <summary>
        /// Creates a RfcommServiceId object from a 128-bit service id.
        /// </summary>
        /// <param name="uuid">The 128-bit service id.</param>
        /// <returns>The RfcommServiceId object.</returns>
        public static RfcommServiceId FromUuid(Guid uuid)
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
            return Windows.Devices.Bluetooth.Rfcomm.RfcommServiceId.FromUuid(uuid);

#else
            return new RfcommServiceId(uuid);
#endif
        }

#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
        private Windows.Devices.Bluetooth.Rfcomm.RfcommServiceId _id;

        private RfcommServiceId(Windows.Devices.Bluetooth.Rfcomm.RfcommServiceId id)
        {
            _id = id;
        }

        public static implicit operator Windows.Devices.Bluetooth.Rfcomm.RfcommServiceId(RfcommServiceId id)
        {
            return id._id;
        }

        public static implicit operator RfcommServiceId(Windows.Devices.Bluetooth.Rfcomm.RfcommServiceId id)
        {
            return new RfcommServiceId(id);
        }

#else 
        private Guid _uuid;

        private RfcommServiceId(Guid uuid)
        {
            _uuid = uuid;
        }
#endif





       
        /// <summary>
        /// Retrieves the 128-bit service id.
        /// </summary>
        public Guid Uuid
        {
            get
            {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
                return _id.Uuid;
#else
                return _uuid;
#endif
            }
        }

        /// <summary>
        /// Converts the RfcommServiceId to a 32-bit service id if possible.
        /// </summary>
        /// <returns>Returns the 32-bit service id if the RfcommServiceId represents a standardized service.</returns>
        public uint AsShortId()
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
            return _id.AsShortId();

#else
            var bytes = _uuid.ToByteArray();
            var baseBytes = BluetoothBase.ToByteArray();
            bool match = true;
            for(int i = 4; i < 16; i++)
            {
                if(bytes[i] != baseBytes[i])
                {
                    match = false;
                    break;
                }
            }

            return match ? BitConverter.ToUInt32(bytes, 0) : 0;
#endif
        }

        /// <summary>
        /// Converts the RfcommServiceId to a string.
        /// </summary>
        /// <returns>Returns the string representation of the 128-bit service id.</returns>
        public string AsString()
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE_81
            return _id.AsString();

#else
            return _uuid.ToString();
#endif
        }
    }
}