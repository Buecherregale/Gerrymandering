using System.Collections.Generic;
using JetBrains.Annotations;

namespace Util
{
    public abstract class GerrymanderingUtil
    {
        [Pure]
        public static int MaxIndex(IReadOnlyList<int> list)
        {
            var maxIndex = 0;

            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] > list[maxIndex])
                    maxIndex = i;
            }

            return maxIndex;
        }
    }
}