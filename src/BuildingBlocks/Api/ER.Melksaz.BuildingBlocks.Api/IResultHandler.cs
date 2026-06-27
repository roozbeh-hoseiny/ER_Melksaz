using ER.Melksaz.PrimitiveResults;
using Microsoft.AspNetCore.Http;

namespace ER.Melksaz.BuildingBlocks.Api;

public interface IResultHandler
{
    IResult Handle<T>(PrimitiveResult<T> result);
    IResult Handle<T>(PrimitiveResult<T> result, Func<T, IResult> func);
}
