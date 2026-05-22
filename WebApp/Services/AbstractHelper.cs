using WebApp.Models;

namespace WebApp.Services;

public abstract class AbstractHelper
{
    public static IEnumerable<T>[] Split<T>(IEnumerable<T> list)
    {
        int n = list.Count() / 2;
        return new IEnumerable<T>[]{
            list.Take(n),
            list.Skip(n).Take(n)
        };
    }
    public static IEnumerable<Parent<Key>> ToTree<Key, T, C>(IEnumerable<Parent<Key>> parents, IEnumerable<Child<Key>> children) where Key : struct
    {
        var dict = children.GroupBy(p => p.ParentId).ToDictionary(p => p.Key, q => q.ToList());
        return parents.Select(p =>
        {
            if (dict.ContainsKey(p.Id))
            {
                p.Children = dict[p.Id];
            }
            return p;
        });
    }
}