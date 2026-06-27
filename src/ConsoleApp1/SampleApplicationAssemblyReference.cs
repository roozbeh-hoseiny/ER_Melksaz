using System.Reflection;

namespace ConsoleApp1;

/// <summary>
/// Serves as an assembly marker and provides access to the current assembly.
/// This type is intended for scenarios such as dependency registration,
/// reflection-based type discovery, assembly scanning, and architecture
/// validation without requiring references to implementation types.
/// </summary>
public static class SampleApplicationAssemblyReference
{
    /// <summary>
    /// Gets the assembly that contains this marker type.
    /// </summary>
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}