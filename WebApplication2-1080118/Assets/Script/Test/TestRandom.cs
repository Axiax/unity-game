using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Assets.Script.Test
{
    static class TestRandom
    {
        private static Random _rnd = new Random();
        public static T WeightedRandom<T>(this IEnumerable<T> source, Func<T, int> selector)
        {
            var totalWeight = source.Sum(x => selector(x));
            var baseWeight = _rnd.Next(0, totalWeight);
            var currentWeight = 0;
            return source.FirstOrDefault(x =>
            {
                currentWeight += selector(x);
                return currentWeight > baseWeight;
            });
        }
    }
}
