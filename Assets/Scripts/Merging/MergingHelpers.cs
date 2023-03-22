using System.Collections.Generic;

namespace Merging
{
    public static class MergingHelpers
    {
        public static bool CheckFullEmpty(ICollection<MergingItemArea> areas)
        {
            bool isFullEmpty = true;
            foreach (var area in areas)
            {
                if (area.IsEmpty() == false)
                {
                    isFullEmpty = false;
                    break;  
                }
            }

            return isFullEmpty;
        }
    }
}