namespace ER.Melksaz.PrimitiveResults;

[Flags]
public enum BindAllIterationStrategy
{
    BreakOnFirstError = 1,
    GoToLast = 2,
    GoToLastAndIgnoreErrors = 4,
    ThrowExceptionOnCancellationRequested = 256
}