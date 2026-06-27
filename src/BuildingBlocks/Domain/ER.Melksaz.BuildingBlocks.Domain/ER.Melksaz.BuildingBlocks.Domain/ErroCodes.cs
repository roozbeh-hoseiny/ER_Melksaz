using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.BuildingBlocks.Domain;

public static class DomainErrorCodes
{
    public const string UniqueConstraintErrorCode = "UniqueConstraint.Error";
    public const string DomainError_ErrorCode = "Error.Dmain";
    public const string AccessDenied_ErrorCode = "Error.AccessDenied";
    public const string UnhandledException_ErrorCode = "Error";
    public const string BadRequest_ErrorCode = "Error.BadRequest";
    public const string Validation_ErrorCode = "Error.Validation";
    public const string RpcException_ErrorCode = "Error.Rpc";
    public const string InvalidCaptcha_ErrorCode = "Error.InvalidCaptcha";
    public const string Concurrency_ErrorCode = "Error.Concurrency";

    public static PrimitiveError CreateUniqueConstraintError(string message) => PrimitiveError.Create(UniqueConstraintErrorCode, message);
}
