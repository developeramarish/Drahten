﻿
namespace PublicHistoryService.Application.Queries
{
    //Marker interface for generic constraint purposes.
    public interface IQuery
    {
    }

    //Marker interface for generic constraint purposes.
    public interface IQuery<TResult> : IQuery
    {
    }
}
