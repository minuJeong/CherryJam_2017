using System.Collections.Generic;

namespace Underwater.Think
{
    public class InfoKnowledge
    {
        private readonly Dictionary<Need, List<Location>> NeedMap = new Dictionary<Need, List<Location>>();

        public List<Location> GetLocationToSatisfyNeed(Need need)
        {
            var e = NeedMap.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Key == need)
                {
                    return e.Current.Value;
                }
            }
            return new List<Location>();
        }

        public void LearnNeedLocation(Location location, Need need)
        {
            if (!NeedMap.ContainsKey(need))
            {
                NeedMap.Add(need, new List<Location>());
            }

            if (!NeedMap[need].Contains(location))
            {
                NeedMap[need].Add(location);
            }
        }

        public void ForgetNeedLocation(Location location, Need need)
        {
            if (NeedMap.ContainsKey(need))
            {
                if (NeedMap[need].Contains(location))
                {
                    NeedMap[need].Remove(location);
                }
            }
        }
    }
}
