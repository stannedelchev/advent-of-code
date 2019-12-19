using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal class Memory<T> : IEnumerable<KeyValuePair<long,T>>
    {
        private readonly Dictionary<long, T> store = new Dictionary<long, T>();

        public T this[long index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            get
            {
                if (!store.ContainsKey(index))
                {
                    store[index] = default;
                }

                return store[index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            set => store[index] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void Clear()
        {
            store.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public IEnumerator<KeyValuePair<long, T>> GetEnumerator()
        {
            return new ReadOnlyCollection<KeyValuePair<long, T>>(store.OrderBy(kvp => kvp.Key).ToList()).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}