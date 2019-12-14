using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2019.Intcode
{
    internal class Memory<T>
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
    }
}