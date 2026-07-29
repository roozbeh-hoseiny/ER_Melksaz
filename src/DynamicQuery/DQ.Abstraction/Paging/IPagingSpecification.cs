namespace DQ.Abstraction.Paging;

public interface IPagingSpecification
{
    int? Skip { get; }
    int? Take { get; }
}