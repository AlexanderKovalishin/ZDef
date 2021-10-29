using System;
using System.Collections.Generic;

namespace ZDef.Core
{
    public static class ListExtension
    {
        private static readonly Random Random = new Random();
        
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count; i > 0; i--)
            {
                int index = i - 1;
                int newIndex = Random.Next(index + 1);
                (list[newIndex], list[index]) = (list[index], list[newIndex]);
            }
        }    
    }
}