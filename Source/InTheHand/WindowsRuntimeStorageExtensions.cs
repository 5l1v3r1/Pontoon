// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsRuntimeStorageExtensions.cs" company="In The Hand Ltd">
//   Copyright (c) 2016 In The Hand Ltd, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using InTheHand.Storage;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// Contains extension methods for the IStorageFile and IStorageFolder interfaces for .NET interop.
    /// </summary>
    public static class InTheHandRuntimeStorageExtensions
    {
        /// <summary>
        /// Retrieves a stream for reading from a specified file.
        /// </summary>
        /// <param name="windowsRuntimeFile"></param>
        /// <returns></returns>
        public static Task<Stream> OpenStreamForReadAsync(this IStorageFile windowsRuntimeFile)
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            return WindowsRuntimeStorageExtensions.OpenStreamForReadAsync((global::Windows.Storage.StorageFile)((StorageFile)windowsRuntimeFile));
#elif __ANDROID__ || __UNIFIED__ || WIN32
            if (windowsRuntimeFile == null)
            {
                throw new ArgumentNullException("windowsRuntimeFile");
            }

#if __ANDROID__
            if(windowsRuntimeFile.Path.StartsWith("content:"))
            {
                return Task.FromResult<Stream>(Android.App.Application.Context.ContentResolver.OpenOutputStream(Android.Net.Uri.Parse(windowsRuntimeFile.Path)));
            }
#endif

            return Task.FromResult<Stream>(global::System.IO.File.OpenRead(windowsRuntimeFile.Path));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Retrieves a stream for reading from a file in the specified parent folder.
        /// </summary>
        /// <param name="rootDirectory">The Windows Runtime IStorageFolder object that contains the file to read from.</param>
        /// <param name="relativePath">The path, relative to the root folder, to the file to read from.</param>
        /// <returns></returns>
        public static Task<Stream> OpenStreamForReadAsync(this IStorageFolder rootDirectory, string relativePath)
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            return WindowsRuntimeStorageExtensions.OpenStreamForReadAsync((global::Windows.Storage.StorageFolder)((StorageFolder)rootDirectory), relativePath);
#elif __ANDROID__ || __UNIFIED__ || WIN32
            string newPath = Path.Combine(rootDirectory.Path, relativePath);
            return Task.FromResult<Stream>(global::System.IO.File.OpenRead(newPath));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Retrieves a stream for writing to a specified file.
        /// </summary>
        /// <param name="windowsRuntimeFile">The Windows Runtime IStorageFile object to write to.</param>
        /// <returns></returns>
        public static Task<Stream> OpenStreamForWriteAsync(this IStorageFile windowsRuntimeFile)
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            return WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync((global::Windows.Storage.StorageFile)((StorageFile)windowsRuntimeFile));
#elif __ANDROID__ || __UNIFIED__ || WIN32
            if (windowsRuntimeFile == null)
            {
                throw new ArgumentNullException("windowsRuntimeFile");
            }

#if __ANDROID__
            if (windowsRuntimeFile.Path.StartsWith("content:"))
            {
                return Task.FromResult<Stream>(Android.App.Application.Context.ContentResolver.OpenInputStream(Android.Net.Uri.Parse(windowsRuntimeFile.Path)));
            }
#endif

            return Task.FromResult<Stream>(global::System.IO.File.OpenWrite(windowsRuntimeFile.Path));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Retrieves a stream for writing from a file in the specified parent folder.
        /// </summary>
        /// <param name="rootDirectory">The Windows Runtime IStorageFolder object that contains the file to write to.</param>
        /// <param name="relativePath">The path, relative to the root folder, to the file to write to.</param>
        /// <param name="creationCollisionOption"></param>
        /// <returns></returns>
        public static Task<Stream> OpenStreamForWriteAsync(this IStorageFolder rootDirectory, string relativePath, CreationCollisionOption creationCollisionOption)
        {
#if WINDOWS_UWP || WINDOWS_APP || WINDOWS_PHONE_APP || WINDOWS_PHONE
            return WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync((global::Windows.Storage.StorageFolder)((StorageFolder)rootDirectory), relativePath, (global::Windows.Storage.CreationCollisionOption)((int)creationCollisionOption));
#elif __ANDROID__ || __UNIFIED__ || WIN32
            string newPath = Path.Combine(rootDirectory.Path, relativePath);
            return Task.FromResult<Stream>(global::System.IO.File.OpenWrite(newPath));
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}