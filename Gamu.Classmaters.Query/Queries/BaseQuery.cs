using System;
using MediatR;

namespace Gamu.Classmaters.Query.Queries
{
    public class BaseQuery<TResult> : IRequest<TResult> where TResult : class
    {
        
    }
}
