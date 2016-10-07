﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if WINDOWS_PHONE
[assembly: AssemblyConfiguration("Windows Phone 8.0")]
#elif WINDOWS_PHONE_81
[assembly: AssemblyConfiguration("Windows Phone Silverlight 8.1")]
#elif WINDOWS_PHONE_APP
[assembly: AssemblyConfiguration("Windows Phone 8.1")]
#elif __IOS__
[assembly: AssemblyConfiguration("iOS")]
#elif __ANDROID__
[assembly: AssemblyConfiguration("Android")]
#elif __MAC__
[assembly: AssemblyConfiguration("macOS")]
#elif WINDOWS_APP
[assembly: AssemblyConfiguration("Windows 8.1")]
#elif WINDOWS_UWP
[assembly: AssemblyConfiguration("Windows Universal")]
#elif WIN32
[assembly: AssemblyConfiguration("Windows")]
#else
[assembly: AssemblyConfiguration("Portable")]
#endif
[assembly: AssemblyProduct("Pontoon")]
[assembly: AssemblyCompany("In The Hand Ltd")]
[assembly: AssemblyCopyright("Copyright © In The Hand Ltd 2013-16. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("10.0.0.0")]
[assembly: AssemblyFileVersion("10.2016.10.7")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]
#if SIGNED
[assembly: AssemblyKeyFile("C:\\InTheHand.snk")]
#endif