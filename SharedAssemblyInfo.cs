using System.Reflection;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif RELEASE
[assembly: AssemblyConfiguration("Release")]
#else
[assembly: AssemblyConfiguration("Unknown")]
#endif

[assembly: AssemblyCompany("Aaron Dandy")]
[assembly: AssemblyProduct("DoTheMath")]
[assembly: AssemblyCopyright("Copyright © 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]