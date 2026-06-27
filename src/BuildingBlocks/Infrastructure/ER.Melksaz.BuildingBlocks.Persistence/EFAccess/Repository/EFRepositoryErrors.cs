using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.BuildingBlocks.Persistence.EFAccess.Repository;

public static class EFRepositoryErrors
{
    public static readonly Lazy<PrimitiveError> Given_Id_Is_Null_Error =
        PrimitiveResults.Helpers.LazyErrorCreator.Create("Persistence.Given_Id_Is_Null.Error", "The given id is null. can not find entity with null primarykey.");

    public static readonly Lazy<PrimitiveError> Entity_With_Id_Not_Found_Error =
        PrimitiveResults.Helpers.LazyErrorCreator.Create("Persistence.Entity_With_Id_Not_Found.Error", "Entity with given id is not found.");

    public static readonly Lazy<PrimitiveError> Null_Reader_Error =
        PrimitiveResults.Helpers.LazyErrorCreator.Create("Persistence.Null_Reader.Error", "Reader is null.");

    public static readonly Lazy<PrimitiveError> Null_DbResult_Error =
        PrimitiveResults.Helpers.LazyErrorCreator.Create("Persistence.Null_DbResult.Error", "The query has null result.");

    public static readonly Lazy<PrimitiveError> Can_Not_Add_Null_Entity_Error =
        PrimitiveResults.Helpers.LazyErrorCreator.Create("Persistence.Can_Not_Add_Null_Entity.Error", "Can not add null entity.");

    public static readonly Lazy<PrimitiveError> Can_Not_Update_Null_Entity_Error =
        PrimitiveResults.Helpers.LazyErrorCreator.Create("Persistence.Can_Not_Update_Null_Entity.Error", "Can not update null entity.");

    public static PrimitiveError Generate_Entity_PrimaryKey_Not_Found_Error<TEntity>() =>
        PrimitiveError.Create("Repository.Error", $"Can not find PrimaryKey of '{typeof(TEntity)}'.");

    public static PrimitiveError Generate_Entity_HasComplexPrimaryKey_Error<TEntity>() =>
        PrimitiveError.Create("Repository.Error", $"The provided EntityType '{typeof(TEntity)}' has complex primary key.");
}

