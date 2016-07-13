﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhoneLine.cs" company="In The Hand Ltd">
//   Copyright (c) 2016 In The Hand Ltd, All rights reserved.
// </copyright>
// <summary>
//   Provides methods for launching the built-in phone call UI.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading.Tasks;

#if __ANDROID__
using Android.App;
using Android.Content;
#elif WINDOWS_APP
using Windows.UI.Popups;
using Windows.Foundation;
#elif WINDOWS_PHONE_APP
using Windows.ApplicationModel.Calls;
using Windows.Foundation;
#elif WINDOWS_PHONE
using Microsoft.Phone.Tasks;
#endif

namespace InTheHand.ApplicationModel.Calls
{
    /// <summary>
    /// Provides methods for launching the built-in phone call UI.
    /// </summary>
    public sealed class PhoneLine
    {
#if WINDOWS_UWP
        private Windows.ApplicationModel.Calls.PhoneLine _line;

        internal PhoneLine(Windows.ApplicationModel.Calls.PhoneLine line)
        {
            _line = line;
        }
#elif WINDOWS_APP || WINDOWS_PHONE_APP
        
        internal static Type _type10;

        static PhoneLine()
        {
            _type10 = Type.GetType("Windows.ApplicationModel.Calls.PhoneLine, Windows, ContentType=WindowsRuntime");
        }

        private object _line;
        private Type _type;

        internal PhoneLine(object line)
        {
            _line = line;
            _type = _line.GetType();
        }
#else
        internal PhoneLine()
        {
        }
#endif
        public static async Task<PhoneLine> FromIdAsync(Guid lineId)
        {
#if __ANDROID__ || __IOS__
            return new PhoneLine();
#elif WINDOWS_UWP
            return new PhoneLine(await Windows.ApplicationModel.Calls.PhoneLine.FromIdAsync(lineId));
#elif WINDOWS_APP || WINDOWS_PHONE_APP
            if(_type10 != null)
            {
                return new PhoneLine(await ((IAsyncOperation<object>)_type10.GetRuntimeMethod("FromIdAsync", new Type[] { typeof(Guid) }).Invoke(null, new object[] { lineId })));
            }

            return null;
#else
            return null;
#endif
        }

        /// <summary>
        /// Place a phone call on the phone line.
        /// The caller must be in the foreground.
        /// </summary>
        /// <param name="number">The number to dial.</param>
        /// <param name="displayName">The display name of the party receiving the phone call.
        /// This parameter is optional.</param>
        public void Dial(string number, string displayName)
        {
#if __ANDROID__
            string action = Intent.ActionCall;
            Intent callIntent = new Intent(action, Android.Net.Uri.FromParts("tel", PhoneCallManager.CleanPhoneNumber(number), null));
            callIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivity(callIntent);
#elif __IOS__
            global::Foundation.NSUrl url = new global::Foundation.NSUrl("tel:" + PhoneCallManager.CleanPhoneNumber(number));
            UIKit.UIApplication.SharedApplication.OpenUrl(url);
#elif WINDOWS_UWP
            _line.Dial(number, displayName);
#endif
        }
    }
}