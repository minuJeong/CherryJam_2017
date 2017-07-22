using System.Collections.Generic;

namespace Underwater.Think
{
    public class InfoKnowledge
    {
        private readonly Dictionary<Location, Need> NeedMap = new Dictionary<Location, Need>();

        public Location GetLocationToSatisfyNeed(Need need)
        {
            var e = NeedMap.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value == need)
                {
                    return e.Current.Key;
                }
            }
            return null;
        }

        public void LearnNeedLocation(Location location, Need need)
        {
            NeedMap.Add(location, need);
        }
    }
}
