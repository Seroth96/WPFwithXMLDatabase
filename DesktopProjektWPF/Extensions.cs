using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopProjektWPF
{
    public static class Extensions
    {
        public static ICollection<T> RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            List<T> toRemove = collection.Where(item => predicate(item)).ToList();
            toRemove.ForEach(item => collection.Remove(item));
            return collection;
        }
    }
}
