using System;
namespace Gamu.Classmaters.Data.Interfaces
{
    public interface IMapper<in TIn, out KOut>
    {
        KOut Map(TIn input);
    }
}
