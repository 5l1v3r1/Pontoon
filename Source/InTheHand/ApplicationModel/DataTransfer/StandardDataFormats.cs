﻿//-----------------------------------------------------------------------
// <copyright file="StandardDataFormats.cs" company="In The Hand Ltd">
//     Copyright © 2013-17 In The Hand Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace InTheHand.ApplicationModel.DataTransfer
{
    /// <summary>
    /// Contains static properties that return string values.
    /// Each string corresponds to a known format ID.
    /// Use this class to avoid errors in using string constants to specify data formats.
    /// </summary>
    public static class StandardDataFormats
    {
        /*
        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the Bitmap format.
        /// </summary>
        public static string Bitmap
        {
            get
            {
                return "Bitmap";
            }
        }
        
        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the HTML format.
        /// </summary>
        public static string Html
        {
            get
            {
                return "HTML Format";
            }
        }

        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the Rich Text Format (RTF).
        /// </summary>
        public static string Rtf
        {
            get
            {
                return "Rich Text Format";
            }
        }
        
        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the StorageItem format.
        /// </summary>
        public static string StorageItems
        {
            get
            {
                return "Shell IDList Array";
            }
        }*/

        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the Text format.
        /// </summary>
        public static string Text
        {
            get
            {
                return "Text";
            }
        }

        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the Uniform Resource Identifier (URI) format.
        /// </summary>
        public static string WebLink
        {
            get
            {
                return "UniformResourceLocatorW";
            }
        }

        /// <summary>
        /// A read-only property that returns the format ID string value corresponding to the Uniform Resource Identifier (URI) format.
        /// </summary>
        public static string ApplicationLink
        {
            get
            {
                return "ApplicationLink";
            }
        }
    }
}