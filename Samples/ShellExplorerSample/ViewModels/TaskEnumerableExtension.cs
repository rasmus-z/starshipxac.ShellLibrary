using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShellExplorerSample.ViewModels
{
    public static class TaskEnumerableExtension
    {
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAll(tasks);
        }
    }
}
