using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class ListExtensions
    {
        public static List<T> Reversed<T>(this IEnumerable<T> list)
        {
            var copy = list.ToList();
            copy.Reverse();
            return copy;
        } 

        public static void Shuffle<T>(this List<T> list)
        {
            // fisher-yates shuffle
            
            var n = list.Count;
            var rng = new System.Random();
            
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;  
            }
        }
        
        public static List<T> Shuffled<T>(this IEnumerable<T> list)
        {
            var rng = new System.Random();

            var shuffled = list.OrderBy(a => rng.Next()).ToList();
            
            return shuffled;
        }
        
        public static List<T> Randomized<T>(this List<T> list)
        {
            list = new List<T>(list);
            var randomizedList = new List<T>();
            
            while (list.Count > 0)
            {
                int index = Random.Range(0, list.Count);
                randomizedList.Add(list[index]);
                list.RemoveAt(index);
            }
            
            return randomizedList;
        }

        public static List<T> GetRandomRange<T>(this List<T> list, int n)
        {
            var shuffled = list.Randomized();

            var selected = shuffled.GetRange(0, n);

            return selected;
        }

        public static T GetRandom<T>(this List<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }
            
            var randomIndex = Random.Range(0, list.Count);

            var randomItem = list[randomIndex];

            return randomItem;
        }
        
        public static T GetRandomExcept<T>(this List<T> list, T except)
        {
            if (list.Count == 0)
            {
                return default;
            }

            var listExcept = list.Except(except).ToList();

            if (listExcept.Count == 0)
            {
                return default;
            }
            
            var randomIndex = Random.Range(0, listExcept.Count);

            var randomItem = listExcept[randomIndex];

            return randomItem;
        }

        public static IEnumerable<TItem> Except<TItem>(this IEnumerable<TItem> list, params TItem[] exceptItems)
        {
            var filtered = list.Where(item => !exceptItems.Contains(item));

            return filtered;
        }
    }
}