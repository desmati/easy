using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace System.Threading.Tasks
{
    public static class ThreadingExtensions
    {
        /// <summary>
        /// ورژن معمولی
        /// RunWithMaxDegreeOfConcurrency(10, ids, async i =>
        /// {
        ///     ICustomerRepo repo = new CustomerRepo();
        ///     var cust = await repo.GetCustomer(i);
        ///     customers.Add(cust);
        /// });
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maxDegreeOfConcurrency"></param>
        /// <param name="collection"></param>
        /// <param name="taskFactory"></param>
        /// <returns></returns>
        public static async Task RunWithMaxDegreeOfConcurrency<T>(
         int maxDegreeOfConcurrency, IEnumerable<T> collection, Func<T, Task> taskFactory)
        {
            var activeTasks = new List<Task>(maxDegreeOfConcurrency);
            foreach (var task in collection.Select(taskFactory))
            {
                activeTasks.Add(task);
                if (activeTasks.Count == maxDegreeOfConcurrency)
                {
                    await Task.WhenAny(activeTasks.ToArray());
                    //TODO: observe exceptions here
                    activeTasks.RemoveAll(t => t.IsCompleted);
                }
            }
            await Task.WhenAll(activeTasks.ToArray()).ContinueWith(t =>
            {
                //TODO: observe exceptions in a manner consistent with the above   
            });
        }

        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body) =>
            source.ForEachAsync<T>(10, body);

        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(async delegate
                {
                    using (partition)
                        while (partition.MoveNext())
                            await body(partition.Current).ContinueWith(t =>
                            {
                                //observe exceptions
                            });

                }));
        }
    }
}