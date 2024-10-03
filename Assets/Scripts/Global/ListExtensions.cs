using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static Dictionary<T1, T2> ToDictionary<T1, T2>(this List<SerializedPair<T1, T2>> list)
    {
        return list.ToDictionary(
            item => item.Key,
            item => item.Value);
    }
}