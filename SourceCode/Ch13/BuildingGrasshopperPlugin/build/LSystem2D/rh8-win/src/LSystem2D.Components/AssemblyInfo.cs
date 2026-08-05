using System;
using System.Reflection;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;

using Rhino.PlugIns;

[assembly: PlugInDescription(DescriptionType.Email, "isabel.lupiani@gmail.com")]
[assembly: PlugInDescription(DescriptionType.Phone, "")]
[assembly: PlugInDescription(DescriptionType.Organization, @"")]
[assembly: PlugInDescription(DescriptionType.Address, @"")]
[assembly: PlugInDescription(DescriptionType.Country, @"")]
[assembly: PlugInDescription(DescriptionType.WebSite, "")]
[assembly: PlugInDescription(DescriptionType.UpdateUrl, "")]
[assembly: PlugInDescription(DescriptionType.Icon, "LSystem2D.Components.Resources.projectIcon.ico")]

[assembly: AssemblyTitle(@"LSystem2D.Components")]
[assembly: AssemblyDescription(@"2D L-System plugin that contains two components - one for rewriting strings, and the other for creating 2D non-branching polylines based on the rewritten strings.")]
[assembly: AssemblyCompany(@"")]
[assembly: AssemblyProduct(@"LSystem2D")]
[assembly: AssemblyCopyright(@"Copyright © 2026 Isabel Lupiani")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("53ce3dd7-979e-41f8-bbbc-64ad11a1b168")]
[assembly: AssemblyVersion("0.1.0")]
[assembly: AssemblyFileVersion("0.1.0")]
[assembly: AssemblyInformationalVersion("0.1.0")]

[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
